
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
    public partial class CarianSyarikat : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private ReportFunction reportFunction = new ReportFunction();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                PanelGrid.Visible = false;
                Panel1.Visible = false;
                ddlSykt();
            }
        }

        private void ddlSykt()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT SyarikatBerjaya FROM Permohonan Where SyarikatBerjaya NOT IN ('')", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddlSyarikat.DataSource = ds;
            ddlSyarikat.DataTextField = "SyarikatBerjaya";
            ddlSyarikat.DataValueField = "SyarikatBerjaya";
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
            try
            {

                {

                   if (ddlSyarikat.SelectedIndex == 0)
                   {
                       PanelGrid.Visible = false;
                       Panel1.Visible = false;
                       
                   }

                    else
                    { 
                    Load_GridData();

                    }
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

            SqlCommand cmd = new SqlCommand("select* FROM Papar_Laporan where SyarikatBerjaya = '" + ddlSyarikat.SelectedValue + "' AND Id_StatusKeputusan IN ('1','2')", conn);

            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Senarai.DataSource = ds;
            Senarai.DataBind();

            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT ROW_NUMBER() OVER (Order by Id DESC) AS 'Bil.', Tajuk, Nama_JPerolehan AS 'Jenis Perolehan' , NamaJabatan AS 'Kemen /Jabatan', Tempoh, Harga ,SyarikatBerjaya ,StatusKeputusan , TarikhSuratSetujuTerima AS 'Tarikh Surat Setuju Terima' FROM Papar_Laporan WHERE (SyarikatBerjaya='" + ddlSyarikat.SelectedValue + "') AND Id_StatusKeputusan IN ('1','2')", conn))
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
                SqlCommand sqlcmd = new SqlCommand("SELECT count (Id) FROM Papar_Laporan WHERE SyarikatBerjaya ='" + ddlSyarikat.SelectedValue + "' AND Id_StatusKeputusan IN ('1','2')", conn);

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

                reportFunction.GenerateLaporanSyarikatExcel(GetPdfLaporanTitle(), dataTable, String.Empty, ref fileName, ref filePath);

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

                reportFunction.GenerateLaporanSyarikatPdf(GetPdfLaporanTitle(), dataTable, String.Empty, ref fileName, ref filePath);

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

        private string GetPdfLaporanTitle()
        {
            return String.Format("CARIAN BERDASARKAN KEPADA SYARIKAT BERJAYA\n" + ddlSyarikat.SelectedItem.Text);
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

                if (lblIDStatus.Text == "2")
                {
                    lblStatus.CssClass = "badge text-bg-success";

                }

            }

        }

        protected void Senarai_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Senarai.PageIndex = e.NewPageIndex;
            Load_GridData();
        }

        


    }
}