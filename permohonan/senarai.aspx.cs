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
using System.Globalization;


namespace EPBM.Permohonan
{
    public partial class senarai : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private ReportFunction reportFunction = new ReportFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

            {
                Load_GridData();
            }

        }
        

        private void Load_GridData()
        {
            string searchTerm = txtSearch.Text;
            string searchCol = listSearchCol.SelectedItem.Text;

            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>();
            string selectText = "Select * from  Papar_Permohonan ";
            string commandText = "where IdStatusPermohonan IN ('1','2','3')";

            //string filter = Request.QueryString["filter"];

            //if ((filter == "2minggu" && !IsPostBack) || cbFilter2Minggu.Checked == true)
            //{
            //    commandText += " and TarikhSahlaku >= DATEADD(day,-14, CAST( GETDATE() AS Date ) ) and  IdStatusPermohonan != 4";

            //    cbFilter2Minggu.Checked = true;

            //}

            if (!string.IsNullOrEmpty(searchTerm))
            {
                queryParams.Add("@searchTerm", searchTerm);
                if (string.IsNullOrEmpty(searchCol) || searchCol == "SEMUA KOLUM")
                {
                    commandText += " AND (NamaJabatan LIKE '%' + @searchTerm + '%' OR Tajuk LIKE '%' + @searchTerm + '%' OR Status_Permohonan LIKE '%' + @searchTerm + '%' OR Harga LIKE '%' + @searchTerm + '%')";
                }
                else if (searchCol == "JABATAN")
                {
                    commandText += " AND NamaJabatan LIKE '%' + @searchTerm + '%' ";
                }
                else if (searchCol == "TAJUK")
                {
                    commandText += " AND Tajuk LIKE '%' + @searchTerm + '%'";
                }
                else if (searchCol == "STATUS")
                {
                    commandText += " AND Status_Permohonan LIKE '%' + @searchTerm + '%'";
                }

            }

            commandText += GetOrder();

            DataTable dtProfile = Utils.GetDataTable(selectText + commandText, queryParams);
            dtProfile.TableName = "Papar_Permohonan";

            DataSet ds = new DataSet();
            ds.Tables.Add(dtProfile);

            Senarai.DataSource = ds;
            Senarai.DataBind();


            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT ROW_NUMBER() OVER (Order by Id DESC) AS 'Bil.', Tajuk, Convert(varchar,[Harga], 20) AS 'Harga', Convert(varchar,[TarikhSahlaku],6) AS 'TarikhSahlaku' , JenisPertimbangan, KaedahPerolehan, SumberPeruntukan , Nama_JPerolehan AS 'Jenis Perolehan', PBM ,  Convert(varchar,[TarikhTerima],6) AS 'TarikhTerima', CatatanPendaftar, ShortName AS 'Jabatan' FROM Papar_Permohonan " + commandText, strConnString))
            {
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", searchTerm);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                gvData.DataSource = dataTable;
                gvData.DataBind();
            }



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
            string tajuk = ((Label)gr.FindControl("lblTajukUtama")).Text.ToString();
            lblTajukUtama.Text = tajuk;
            ID.Text = sid;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "opendeleteModal();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "#deleteModal", "$('#deleteModal').modal('show')", true);

        }

        protected void btnSahHapus_Click(object sender, System.EventArgs e)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Permohonan WHERE ID ='" + ID.Text + "'", conn);

            conn.Open();
            string insertQuery = "DELETE From Permohonan where ID ='" + ID.Text + "'";
            SqlCommand com = new SqlCommand(insertQuery, conn);
            com.ExecuteNonQuery();

            Response.Redirect("/Permohonan/Senarai.aspx");
            conn.Close();

        }


        protected void Senarai_DataBound(object sender, EventArgs e)
        {

            for (int i = 0; i <= Senarai.Rows.Count - 1; i++)
            {
                ImageButton btnhapus = (ImageButton)Senarai.Rows[i].FindControl("btnhapus");
                HyperLink HyperLinkEdit = (HyperLink)Senarai.Rows[i].FindControl("HyperLinkEdit");
                //HyperLink HyperLinkMaju = (HyperLink)Senarai.Rows[i].FindControl("HyperLinkMaju");

                Label lblStatus = (Label)Senarai.Rows[i].FindControl("lblStatus");
                Label lblIDStatus = (Label)Senarai.Rows[i].FindControl("lblIDStatus");

                if (lblIDStatus.Text == "1")
                {
                    lblStatus.CssClass = "badge text-bg-primary";
                    HyperLinkEdit.Visible = true;
                    btnhapus.Visible = true;
                }

                if (lblIDStatus.Text == "2")
                {
                    lblStatus.CssClass = "badge text-bg-warning";
                    HyperLinkEdit.Visible = true;
                    btnhapus.Visible = true;
                }

                if (lblIDStatus.Text == "3")
                {
                    lblStatus.CssClass = "badge text-bg-success";
                    HyperLinkEdit.Visible = true;
                    //HyperLinkMaju.Visible = true;

                }

            }

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Permohonan/Senarai.aspx");

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Senarai.PageIndex = 0;
            //Panel2.Visible = true;
            Load_GridData();
        }

        private string GetOrder()
        {

            string orderBy = String.Empty;

            if (String.IsNullOrEmpty(lblSortColumn.Text))
                lblSortColumn.Text = "ID";

            if (String.IsNullOrEmpty(lblSortDirection.Text))

                lblSortDirection.Text = Constants.DESC;

            lblIcon.Text = lblSortDirection.Text == Constants.ASC ? "↑" : "↓";

            lblSortRecord.Text = String.Format(" {0} {1}", GetSortingHeader(lblSortColumn.Text), lblIcon.Text);


            return String.Format(" order by {0} {1}", lblSortColumn.Text, lblSortDirection.Text);
        }

        private string GetSortingHeader(string columnName)
        {
            foreach (DataControlField column in Senarai.Columns)
            {
                if (column.SortExpression.Equals(columnName))
                    return column.HeaderText;
            }

            return "ID ";
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfStatus = (e.Row.FindControl("hfStatus") as HiddenField);

                //if (hfStatus.Value == "NRECC")
                //{
                //    e.Row.ToolTip = (e.Row.DataItem as DataRowView)["NamaBahagian"].ToString();
                //}
            }

        }

        protected void Senarai_Sorting(object sender, GridViewSortEventArgs e)
        {

            if (lblSortColumn.Text.Equals(e.SortExpression))
                lblSortDirection.Text = lblSortDirection.Text.Equals(Constants.ASC) ? Constants.DESC : Constants.ASC;
            else
                lblSortColumn.Text = e.SortExpression;

            Load_GridData();


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

                reportFunction.GenerateLaporanSenaraiPerolehanExcel(GetLaporanTitle(), dataTable, String.Empty, ref fileName, ref filePath);

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

                reportFunction.GenerateLaporanSenaraiPerolehanPdf(GetPdfLaporanTitle(), dataTable, String.Empty, ref fileName, ref filePath);

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
           

            return String.Format("Senarai Perolehan\n");
            

        }

        private string GetPdfLaporanTitle()
        {
            return String.Format("Senarai Perolehan\n");
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



        protected void Filter2Minggu_Change(object sender, EventArgs args)
        {
            Load_GridData();
        }
    }
}