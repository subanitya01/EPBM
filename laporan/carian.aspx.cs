
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Threading;



namespace EPBM.laporan
{
    public partial class Carian : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private ReportFunction reportFunction = new ReportFunction();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                PanelGrid.Visible = false;
                Panel1.Visible = false;
             
                InitilaizeWebControl();
                ResetLaporanControl();
                ddlSumber_Peruntukan();
                ddlJenis_Perolehan();
            }
        }

        private void ddlJenis_Perolehan()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM JenisPerolehan", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddlJenisPerolehan.DataSource = ds;
            ddlJenisPerolehan.DataTextField = "Nama";
            ddlJenisPerolehan.DataValueField = "Id";
            ddlJenisPerolehan.DataBind();
            ddlJenisPerolehan.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Semua--", ""));
        }

        protected void ddlJenisPerolehan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ddlSumber_Peruntukan()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM SumberPeruntukan", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddlSumberPeruntukan.DataSource = ds;
            ddlSumberPeruntukan.DataTextField = "Nama";
            ddlSumberPeruntukan.DataValueField = "Id";
            ddlSumberPeruntukan.DataBind();
            ddlSumberPeruntukan.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Semua--", ""));
        }

        protected void ddlSumberPeruntukan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCari_Click(object sender, EventArgs e)
        {

            DateTime TarikhMula = SystemHelper.GetDateTime(txttkhmula.Text);
            DateTime TarikhAkhir = SystemHelper.GetDateTime(txttkhakhir.Text);

            try
            {

                {

                    if ((TarikhMula > TarikhAkhir))
                    {
                        MessageAlert.Visible = true;   
                    }

                    else
                    {
                        Load_GridData();
                        MessageAlert.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        #region PaparData Laporan

        #region SQL functions

        private DataTable GetDataTable(string query, string connectionName = "DefaultConnection")
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();

                return dataTable;
            }
        }

        #endregion

        private void Load_GridData()
        {

            Count2();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlDataAdapter da;

            conn.Open();
            //string curUser = (string)Session["name"];

            SqlCommand cmd = new SqlCommand("select * FROM Papar_Laporan " + GetCondition() + GetOrder(), conn);
            //SqlCommand cmd = new SqlCommand("select* FROM Papar_Laporan" + GetCondition(), conn);

            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Senarai.DataSource = ds;
            Senarai.DataBind();



            //using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT ROW_NUMBER() OVER (Order by Id DESC) AS 'Bil.', Tajuk, Nama_JPerolehan AS 'Jenis Perolehan' , NamaJabatan AS 'Kemen /Jabatan', Tempoh, Convert(varchar,[Harga],6)Harga ,SyarikatBerjaya ,StatusKeputusan ,Convert(varchar,[TarikhSuratSetujuTerima],20) AS 'Tarikh Surat Setuju Terima' FROM Papar_Laporan " + GetCondition(), conn))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT ROW_NUMBER() OVER (" + GetOrder() + ")  AS 'Bil.', Tajuk, Nama_JPerolehan AS 'Jenis Perolehan' , NamaJabatan AS 'Kemen /Jabatan', Tempoh, Convert(varchar,[Harga],20) AS 'Harga' ,SyarikatBerjaya ,JenisPertimbangan ,StatusKeputusan ,PBM AS 'MUKTAMAD' FROM Papar_Laporan " + GetCondition(), conn))
            {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                gvData.DataSource = dataTable;
                gvData.DataBind();

            }

            PanelGrid.Visible = true;
            Panel1.Visible = true;

            conn.Close();
            total_RepTahun.Text = ds.Tables[0].Rows.Count.ToString();

        }

        private void InitializeDDLJabatan(DropDownList dropDownList)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Jabatan where aktif = 'True' and  Organisasi_Grp_ID NOT IN ('1')", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();

            dropDownList.DataSource = ds;
            dropDownList.DataTextField = "ShortName";
            dropDownList.DataValueField = "Organisasi_Grp_ID";
            dropDownList.DataBind();
            dropDownList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Semua Jabatan", String.Empty));
        }

        protected void ddlFilterLaporan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ResetLaporanControl();

            pnlFilterJabatan.Visible = ddlFilterLaporan.SelectedValue == "2";

            //Load_GridData();
        }

        protected void ddlFilterJabatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void InitilaizeWebControl()
        {
           
            InitializeDDLJabatan(ddlFilterJabatan);
           
        }

        private void ResetLaporanControl()
        {
            pnlFilterJabatan.Visible = false;
        }
    
        #endregion

        #region Sum
        private void Count2()
        {
         
  
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string curUser = (string)Session["name"];

                SqlCommand cmd = new SqlCommand("SELECT SUM (Harga) FROM Papar_Laporan " + GetCondition(), conn);
                conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conn.Close();

                if (dt.Rows.Count != 0)
                {                 
                    lbltotal_RepTahun2.Text = SystemHelper.GetDouble(dt.Rows[0][0].ToString()).ToString("#,##0.00");
                }
                else
                {

                }
            }

        }

        #endregion

        #region Carian
        private string GetCondition()
        {
            //List<string> conditions = new List<string>();

            string condition = String.Format("Where Id_StatusKeputusan IN ('1','3','5') AND TarikhTerima > '{0}' AND TarikhTerima < '{1}'", SystemHelper.GetDate(txttkhmula.Text).ToString("yyyy-MM-dd"), SystemHelper.GetDate(txttkhakhir.Text).ToString("yyyy-MM-dd"));

            int jenisLaporan = SystemHelper.GetInteger(ddlFilterLaporan.SelectedValue);
            int jabatanId = SystemHelper.GetInteger(ddlFilterJabatan.SelectedValue);
            int jenisPerolehan = SystemHelper.GetInteger(ddlJenisPerolehan.SelectedValue);
            int sumberPeruntukan = SystemHelper.GetInteger(ddlSumberPeruntukan.SelectedValue);

            if (jenisLaporan == 1) condition = String.Format("{0} AND Organisasi_Grp_ID = 1", condition);
            if (jenisLaporan == 2 && jabatanId > 0) condition = String.Format("{0} AND Organisasi_Grp_ID = {1}", condition, jabatanId);
            if (jenisLaporan == 2 && jabatanId == 0) condition = String.Format("{0} AND Organisasi_Grp_ID != 1", condition);
            if (jenisPerolehan > 0) condition = String.Format("{0} AND IdJenisPerolehan = {1}", condition, jenisPerolehan);
            if (sumberPeruntukan > 0) condition = String.Format("{0} AND IdSumberPeruntukan = {1}", condition, sumberPeruntukan);

            return condition;
         
        }

        #endregion

        private string GetLaporanTitle1()
        {
            string jabatan = String.Empty;
            string peruntukan = String.Empty;
            string jperolehan = String.Empty;

            //string jperolehan = String.Empty;
            DateTime TarikhMula = SystemHelper.GetDateTime(txttkhmula.Text);
            DateTime TarikhAkhir = SystemHelper.GetDateTime(txttkhakhir.Text);

 

            if (ddlFilterLaporan.SelectedValue == "2")
            {
                jabatan = ddlFilterJabatan.SelectedItem.Text;
            }
            else if (ddlFilterLaporan.SelectedValue == "1")
            {
                jabatan = "Kementerian";
            }

            
            if (ddlSumberPeruntukan.SelectedIndex > 0)

            {
                peruntukan = ddlSumberPeruntukan.SelectedItem.Text;
            }

            if (ddlJenisPerolehan.SelectedIndex > 0)

            {
                jperolehan = ddlJenisPerolehan.SelectedItem.Text;
            }

            if (ddlSumberPeruntukan.SelectedIndex == 0)
            {
                peruntukan = "SEMUA";
            }

            if (ddlJenisPerolehan.SelectedIndex == 0)  

            {
                jperolehan = "SEMUA";
            } 
            

                jabatan = !String.IsNullOrWhiteSpace(jabatan) ? String.Format("({0})", jabatan) : String.Empty;
         
            return String.Format("Laporan EPBM {0}\nTarikh Mula: {1}{2}{3}\nSumber Peruntukan :{4}\nJenis Perolehan : {5}", jabatan, TarikhMula.ToString("dd-MMM-yyyy"), "  Hingga  ", TarikhAkhir.ToString("dd-MMM-yyyy"), peruntukan,jperolehan);

        }

        #region Laporan

        protected void ExportToExcel(object sender, EventArgs e)
        {
            try
            {
                string fileName = String.Empty;
                string filePath = String.Empty;

                Load_GridData(); 
                DataTable dataTable = (DataTable)gvData.DataSource;


                reportFunction.GenerateLaporanExcel(GetLaporanTitle1(), dataTable, "RM " + lbltotal_RepTahun2.Text, ref fileName, ref filePath);

                DownloadFile(fileName, filePath);
            }
            catch (ThreadAbortException ex) { }
            catch (Exception exception)
            {
                Response.Write(exception.Message);
            }
           
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ExportToPDF(object sender, EventArgs e)
        {
           
            try
            {
                string fileName = String.Empty;
                string filePath = String.Empty;

                Load_GridData();
                DataTable dataTable = (DataTable)gvData.DataSource;

                reportFunction.GenerateLaporanPdf(GetLaporanTitle1(), dataTable, "RM " + lbltotal_RepTahun2.Text, ref fileName, ref filePath);

                DownloadFile(fileName, filePath);
            }
            catch (ThreadAbortException ex) { }
            catch (Exception exception)
            {
                Response.Write(exception.Message);
            }
           
        }

        private void DownloadFile(string fileName, string location)
        {
            Response.AddHeader(Constants.CONTENT_DISPOSITION, String.Format(Constants.FORMAT_ATTACHMENT_FILE, System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8)));
            Response.ContentType = Constants.CONTENT_TYPE;
            Response.Flush();
            Response.BinaryWrite(SystemHelper.GetFileByteArray(location));
            Response.End();
        }

        #endregion

        protected void Senarai_DataBound(object sender, EventArgs e)
        {

            for (int i = 0; i <= Senarai.Rows.Count - 1; i++)
            {
                ImageButton btnhapus = (ImageButton)Senarai.Rows[i].FindControl("btnhapus");
                Label lblStatus = (Label)Senarai.Rows[i].FindControl("lblStatus");
                Label lblIDStatus = (Label)Senarai.Rows[i].FindControl("lblIDStatus");

                if (lblIDStatus.Text == "1")
                {
                    lblStatus.CssClass = "badge text-bg-info";

                }

                if (lblIDStatus.Text == "5")
                {
                    lblStatus.CssClass = "badge text-bg-warning";

                }

                if (lblIDStatus.Text == "3")
                {
                    lblStatus.CssClass = "badge text-bg-danger";

                }

            }

        }

        protected void Senarai_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Senarai.PageIndex = e.NewPageIndex;
            Load_GridData();
        }

        private string GetOrder()
        {
            string orderBy = String.Empty;

            if (String.IsNullOrEmpty(lblSortColumn.Text))
                lblSortColumn.Text = "TarikhSuratSetujuTerima";

            if (String.IsNullOrEmpty(lblSortDirection.Text))
                lblSortDirection.Text = Constants.DESC;

            lblSortRecord.Text = String.Format(" {0} {1}", GetSortingHeader(lblSortColumn.Text), lblIcon.Text);
           
            lblIcon.Text = lblSortDirection.Text == Constants.ASC ? "↑": "↓";

            return String.Format(" order by {0} {1}", lblSortColumn.Text, lblSortDirection.Text);
        }

        private string GetSortingHeader(string columnName)
        {
            foreach (DataControlField column in Senarai.Columns)
            {
                if (column.SortExpression.Equals(columnName))
                    return column.HeaderText;
            }

            return "TarikhSuratSetujuTerima";
        }

        protected void Senarai_Sorting(object sender, GridViewSortEventArgs e)
        {

            if (lblSortColumn.Text.Equals(e.SortExpression))
                lblSortDirection.Text = lblSortDirection.Text.Equals(Constants.ASC) ? Constants.DESC : Constants.ASC;
            else
                lblSortColumn.Text = e.SortExpression;
           
            Load_GridData();


        }

    }
}