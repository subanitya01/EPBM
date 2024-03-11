
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



namespace EPBM.SyarikatBerjayaSblm2023
{
    public partial class CarianSyarikat : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private ReportFunction reportFunction = new ReportFunction();


        protected void Page_Load(object sender, EventArgs e)
        {   
            
           

            if (!IsPostBack)
            {
                Load_GridData();
                
                PanelGrid.Visible = true;
                Panel1.Visible = false;
                ddlSykt();
            }
        }

        private void ddlSykt()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT NamaSyarikat FROM SyarikatBerjayaSebelum2023", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddlSyarikat.DataSource = ds;
            ddlSyarikat.DataTextField = "NamaSyarikat";
            ddlSyarikat.DataValueField = "NamaSyarikat";
            ddlSyarikat.DataBind();
            ddlSyarikat.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Sila Pilih--", ""));
        }

        protected void ddlSyarikat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSyarikat.SelectedIndex == 0)
            {
                
                PanelGrid.Visible = false;
                Panel1.Visible = false;
            }

        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            DateTime TarikhMula = SystemHelper.GetDateTime(txttkhmula.Text);
            DateTime TarikhAkhir = SystemHelper.GetDateTime(txttkhakhir.Text);

            try
            {

                {
                    Load_GridData();


                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        #region PaparData Syarikat

        private void Load_GridData()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlDataAdapter da;

            conn.Open();
            //string curUser = (string)Session["name"];

            SqlCommand cmd = new SqlCommand("Select * FROM PaparSyarikatBerjayaSebelum2023 " + GetCondition() + GetOrder(), conn);

            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Senarai.DataSource = ds;
            Senarai.DataBind();
            
            //using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT ROW_NUMBER() OVER (Order by Id DESC) AS 'Bil.', Tajuk, Nama AS 'Jenis Perolehan',Tempoh, Convert(varchar,[Harga],20) AS Harga ,NamaSyarikat, TahunLantikan AS 'TahunLantikan',NamaSyarikat FROM PaparSyarikatBerjayaSebelum2023 " + GetCondition(), conn))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT ROW_NUMBER() OVER(" + GetOrder() + ")  AS 'Bil.', Tajuk, Nama AS 'Jenis Perolehan',Tempoh, Convert(varchar,[Harga],20) AS Harga ,NamaSyarikat, TahunLantikan AS 'TahunLantikan',NamaSyarikat FROM PaparSyarikatBerjayaSebelum2023 " + GetCondition(), conn))
                {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                gvData.DataSource = dataTable;
                gvData.DataBind();
            }

          
            PanelGrid.Visible = true;
            Panel1.Visible = true;
            

            conn.Close();
            Count();

        }

        private void Count()
        {

            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;         
            using (SqlConnection conn = new SqlConnection(connString))
            {

                //string curUser = (string)Session["name"];
                SqlCommand sqlcmd = new SqlCommand("SELECT count (Id) FROM PaparSyarikatBerjayaSebelum2023 " + GetCondition(), conn);


                conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conn.Close();

                if (dt.Rows.Count != 0)
                {                  
                    total_RepTahun.Text = dt.Rows[0][0].ToString().Trim();
                }
                else
                {

                }
            }

        }

        #endregion

        #region Laporan

        protected void ExportToExcel(object sender, EventArgs e)
        {
            try
            {
                string fileName = String.Empty;
                string filePath = String.Empty;

                Load_GridData(); 
                DataTable dataTable = (DataTable)gvData.DataSource;

                reportFunction.GenerateLaporanSyarikatExcel(GetLaporanTitle1(), dataTable, String.Empty, ref fileName, ref filePath);

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

                reportFunction.GenerateLaporanSyarikatPdf(GetLaporanTitle1(), dataTable, String.Empty, ref fileName, ref filePath);

                DownloadFile(fileName, filePath);
            }
            catch (ThreadAbortException ex) { }
            catch (Exception exception)
            {
                Response.Write(exception.Message);
            }
           
        }

        private string GetLaporanTitle()
        {
            return String.Format("CARIAN BERDASARKAN KEPADA SYARIKAT BERJAYA\n" + ddlSyarikat.SelectedItem.Text);
        }


        private string GetLaporanTitle1()
        {
 

            string NamaSyarikat = String.Empty;


            //string TarikhMula = txttkhmula.Text;
            //string TarikhAkhir = txttkhmula.Text;
            DateTime TarikhMula = SystemHelper.GetDate(txttkhmula.Text);
            DateTime TarikhAkhir = SystemHelper.GetDate(txttkhakhir.Text);

            if (ddlSyarikat.SelectedIndex > 0)

            {
                NamaSyarikat = ddlSyarikat.SelectedItem.Text;
            }

            if (ddlSyarikat.SelectedIndex == 0)

            {
                NamaSyarikat = "SEMUA";
            }

            if (txttkhmula.Text == "" && txttkhakhir.Text == "")
            {

                return String.Format("DATA SEJARAH SYARIKAT BERJAYA SEBELUM 2024 \nNama Syarikat :{0} ", NamaSyarikat);
            }

            //if (TarikhMula==null )
            //{

            //    return String.Format("DATA SEJARAH SYARIKAT BERJAYA SEBELUM 2024 \nTarikh Mula: {0}\nNama Syarikat :{1} ", TarikhMula.ToString("dd-MMM-yyyy"), NamaSyarikat);
            //}

            //if (TarikhAkhir == null)
            //{

            //    return String.Format("DATA SEJARAH SYARIKAT BERJAYA SEBELUM 2024 \nTarikh Akhir: {0}\nNama Syarikat :{1} ", TarikhAkhir.ToString("dd-MMM-yyyy"), NamaSyarikat);
            //}
            
            return String.Format("DATA SEJARAH SYARIKAT BERJAYA SEBELUM 2024 \nTarikh Mula: {0}{1}{2}\nNama Syarikat :{3} ", TarikhMula.ToString("dd-MMM-yyyy"), "  Hingga  ", TarikhAkhir.ToString("dd-MMM-yyyy"), NamaSyarikat);


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

        }

        protected void Senarai_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Senarai.PageIndex = e.NewPageIndex;
            Load_GridData();
        }


        protected void btn_hapus_Click(object sender, System.EventArgs e)
        {

            GridViewRow gr = (GridViewRow)(((ImageButton)sender).Parent.Parent);

            string sid = ((Label)gr.FindControl("lblID")).Text.ToString();
            //string tajuk = ((Label)gr.FindControl("lblTajukUtama")).Text.ToString();
            //lblTajukUtama.Text = tajuk;
            ID.Text = sid;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "opendeleteModal();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "#deleteModal", "$('#deleteModal').modal('show')", true);

        }

        protected void btnSahHapus_Click(object sender, System.EventArgs e)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM SyarikatBerjayaSebelum2023 WHERE Id ='" + ID.Text + "'", conn);

            conn.Open();
            string insertQuery = "DELETE From SyarikatBerjayaSebelum2023 where Id ='" + ID.Text + "'";
            SqlCommand com = new SqlCommand(insertQuery, conn);
            com.ExecuteNonQuery();

            Response.Redirect("/SyarikatBerjayaSblm2023/CarianSyarikat.aspx");
            conn.Close();

        }


        private string GetCondition()

        {

            string condition = String.Format("Where Id NOT IN ('0')");

            if (ddlSyarikat.SelectedIndex > 0) condition = String.Format("{0} AND NamaSyarikat LIKE '%{1}%'", condition, ddlSyarikat.SelectedItem.Text);
            if (!String.IsNullOrWhiteSpace(txttkhmula.Text) && String.IsNullOrWhiteSpace(txttkhakhir.Text)) condition = String.Format("{0} AND TahunLantikan = '{1}'", condition, SystemHelper.GetDateTime(txttkhmula.Text).ToString("yyyy-MM-dd"));
            if (String.IsNullOrWhiteSpace(txttkhmula.Text) && !String.IsNullOrWhiteSpace(txttkhakhir.Text)) condition = String.Format("{0} AND TahunLantikan = '{1}'", condition, SystemHelper.GetDateTime(txttkhakhir.Text).ToString("yyyy-MM-dd"));
            if (!String.IsNullOrWhiteSpace(txttkhmula.Text) && !String.IsNullOrWhiteSpace(txttkhakhir.Text))
            {
                string startDate = SystemHelper.GetDateTime(txttkhmula.Text).ToString("yyyy-MM-dd");
                string endDate = SystemHelper.GetDateTime(txttkhakhir.Text).ToString("yyyy-MM-dd");

                condition = String.Format("{0} AND ((TahunLantikan <= '{1}' AND TahunLantikan >= '{1}' AND TahunLantikan <= '{2}' AND TahunLantikan >= '{2}')", condition, startDate, endDate);
                condition = String.Format("{0} OR (TahunLantikan >= '{1}' AND TahunLantikan >= '{1}' AND TahunLantikan <= '{2}' AND TahunLantikan >= '{2}')", condition, startDate, endDate);
                condition = String.Format("{0} OR (TahunLantikan <= '{1}' AND TahunLantikan >= '{1}' AND TahunLantikan <= '{2}' AND TahunLantikan <= '{2}')", condition, startDate, endDate);
                condition = String.Format("{0} OR (TahunLantikan >= '{1}' AND TahunLantikan >= '{1}' AND TahunLantikan <= '{2}' AND TahunLantikan <= '{2}'))", condition, startDate, endDate);
            }

         
            return condition;

        }


        private string GetOrder()
        {
            string orderBy = String.Empty;

            if (String.IsNullOrEmpty(lblSortColumn.Text))
                lblSortColumn.Text = "TahunLantikan";

            if (String.IsNullOrEmpty(lblSortDirection.Text))
                lblSortDirection.Text = Constants.DESC;

            lblSortRecord.Text = String.Format(" {0} {1}", GetSortingHeader(lblSortColumn.Text), lblIcon.Text);

            lblIcon.Text = lblSortDirection.Text == Constants.ASC ? "↑" : "↓";

            return String.Format(" order by {0} {1}", lblSortColumn.Text, lblSortDirection.Text);
        }

        private string GetSortingHeader(string columnName)
        {
            foreach (DataControlField column in Senarai.Columns)
            {
                if (column.SortExpression.Equals(columnName))
                    return column.HeaderText;
            }

            return "TahunLantikan";
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