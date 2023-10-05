using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.keputusan
{
    public partial class cetak_senarai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PreviousPage != null && PreviousPage.IsCrossPagePostBack)
            {
                //TextBox txtName = (TextBox)PreviousPage.FindControl("txtName");
                //LblName.Text = "Welcome " + txtName.Text;
                LinkButton btn = (LinkButton)Utils.FindControlRecursive(Page.PreviousPage, "btnCetak");
                if(btn != null)
                    BindData((String)btn.CommandArgument);
            }
            else
            {
                Response.Redirect("~/keputusan/senarai.aspx");
            }
        }
        protected void BindData(string method)
        {
            /*try
            {*/

            ViewState["txtSearch"] = ((TextBox)Utils.FindControlRecursive(Page.PreviousPage, "txtSearch")).Text.Trim();
            ViewState["listSearchCol"] = ((DropDownList)Utils.FindControlRecursive(Page.PreviousPage, "listSearchCol")).Text.Trim();
            ViewState["listMesyuarat"] = ((DropDownList)Utils.FindControlRecursive(Page.PreviousPage, "listMesyuarat")).Text.Trim();
            ViewState["txtTajuk"] = ((TextBox)Utils.FindControlRecursive(Page.PreviousPage, "txtTajuk")).Text.Trim();
            ViewState["listJabatan"] = ((DropDownList)Utils.FindControlRecursive(Page.PreviousPage, "listJabatan")).Text.Trim();
            ViewState["listBahagian"] = ((DropDownList)Utils.FindControlRecursive(Page.PreviousPage, "listBahagian")).Text.Trim();
            ViewState["listStatus"] = ((DropDownList)Utils.FindControlRecursive(Page.PreviousPage, "listStatus")).Text.Trim();
            ViewState["listCondSyarikat"] = ((DropDownList)Utils.FindControlRecursive(Page.PreviousPage, "listCondSyarikat")).Text.Trim();
            ViewState["txtSyarikat"] = ((TextBox)Utils.FindControlRecursive(Page.PreviousPage, "txtSyarikat")).Text.Trim();
            ViewState["extendSearch"] = method=="extend";
            string searchTerm = Convert.ToString(ViewState["txtSearch"]) ?? null;
            string searchCol = Convert.ToString(ViewState["listSearchCol"]) ?? null;
            bool extendSearch = Convert.ToBoolean(ViewState["extendSearch"]);
            string sortDir = ViewState["SortDirection"] as string;
            string sortBy = ViewState["SortExpression"] as string;
            string selectData = "Select Id, Tajuk, CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, IdStatusKeputusan, " +
                                "StatusKeputusan as STATUS, SyarikatBerjaya, Harga, Tempoh, AlasanKeputusan as KETERANGAN, MESYUARAT ";
            string CommandText = "from Papar_Permohonan WHERE TarikhHapus IS NULL AND IdStatusPengesahan = 4 ";
            string limit = "";// " OFFSET  " + (GridView1.PageIndex * GridView1.PageSize) + " ROWS FETCH NEXT " + GridView1.PageSize + " ROWS ONLY";

            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (string.IsNullOrEmpty(searchCol))
                {
                    CommandText += " AND (" +
                            "MESYUARAT LIKE '%' + @searchTerm + '%' " +
                            "OR TAJUK LIKE '%' + @searchTerm + '%' " +
                            "OR (NamaBahagian LIKE '%' + @searchTerm + '%' AND IdJabatan = 1) " +
                            "OR (NamaJabatan LIKE '%' + @searchTerm + '%' AND IdJabatan <> 1) " +
                            "OR StatusKeputusan LIKE '%' + @searchTerm + '%'" +
                            "OR SyarikatBerjaya LIKE '%' + @searchTerm + '%'" +
                            "OR AlasanKeputusan LIKE '%' + @searchTerm + '%'" +
                        ")";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "MESYUARAT")
                {
                    CommandText += " AND MESYUARAT LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "TAJUK")
                {
                    CommandText += " AND TAJUK LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "JABATAN")
                {
                    CommandText += " AND (NamaBahagian LIKE '%' + @searchTerm + '%' AND IdJabatan = 1 OR NamaJabatan LIKE '%' + @searchTerm + '%' AND IdJabatan <> 1)";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "STATUS")
                {
                    CommandText += " AND StatusKeputusan LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "SYARIKAT BERJAYA")
                {
                    CommandText += " AND SyarikatBerjaya LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "ALASAN DIBATALKAN")
                {
                    CommandText += " AND AlasanKeputusan LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
            }
            if (extendSearch)
            {

                int? IdMesyuarat = int.TryParse(ViewState["listMesyuarat"].ToString(), out int number) ? number : 0;
                string Tajuk = Convert.ToString(ViewState["txtTajuk"]) ?? null;
                int? IdJabatan = int.TryParse(ViewState["listJabatan"].ToString(), out int number2) ? number2 : 0;
                int? IdBahagian = int.TryParse(ViewState["listBahagian"].ToString(), out int number3) ? number3 : 0;
                int? IdStatus = int.TryParse(ViewState["listStatus"].ToString(), out int number4) ? number4 : 0;
                string CondSyarikat = Convert.ToString(ViewState["listCondSyarikat"]) ?? null;
                string Syarikat = Convert.ToString(ViewState["txtSyarikat"]) ?? null;

                if (IdMesyuarat > 0)
                {
                    CommandText += " AND IdMesyuarat = @IdMesyuarat";
                    queryParams.Add("@IdMesyuarat", IdMesyuarat);
                }
                if (!string.IsNullOrEmpty(Tajuk))
                {
                    CommandText += " AND Tajuk LIKE '%' + @Tajuk + '%'";
                    queryParams.Add("@Tajuk", Tajuk);
                }
                if (IdJabatan > 0)
                {
                    CommandText += " AND Organisasi_Grp_ID = @IdJabatan";
                    queryParams.Add("@IdJabatan", IdJabatan);
                }
                if (IdJabatan == 1 && IdBahagian > 0)
                {
                    CommandText += " AND IdBahagian = @IdBahagian";
                    queryParams.Add("@IdBahagian", IdBahagian);
                }
                if (IdStatus > 0)
                {
                    CommandText += " AND IdStatusKeputusan = @IdStatus";
                    queryParams.Add("@IdStatus", IdStatus);
                }
                if (!string.IsNullOrEmpty(Syarikat))
                {
                    if (CondSyarikat == "SAMA DENGAN")
                        CommandText += " AND SyarikatBerjaya = @Syarikat";
                    else if (CondSyarikat == "BERMULA DENGAN")
                        CommandText += " AND SyarikatBerjaya LIKE @Syarikat + '%'";
                    else if (CondSyarikat == "BERAKHIR DENGAN")
                        CommandText += " AND SyarikatBerjaya LIKE '%' + @Syarikat";
                    else
                        CommandText += " AND SyarikatBerjaya LIKE '%' + @Syarikat + '%'";

                    queryParams.Add("@Syarikat", Syarikat);
                }
            }
            if (!string.IsNullOrEmpty(sortBy))
                sortBy = " ORDER BY " + sortBy + " " + sortDir;
            else
                sortBy = " ORDER BY Id desc";

            string selectCount = "Select count(*) ";
            DataTable dtCountPermohonan = Utils.GetDataTable(selectCount + CommandText, queryParams);
            GridView1.VirtualItemCount = Convert.ToInt16(dtCountPermohonan.Rows[0][0].ToString());

            DataTable dtPermohonan = Utils.GetDataTable(selectData + CommandText + sortBy + limit, queryParams);

            GridView1.DataSource = dtPermohonan;
            GridView1.DataBind();
            ViewState["dtPermohonan"] = dtPermohonan;
            /*}
            catch (Exception) { Utils.HttpNotFound(); }*/
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                Label LblKeterangan = e.Row.FindControl("LblKeterangan") as Label;
                ListView detailsList = e.Row.FindControl("DetailsList") as ListView;
                Literal numbering = e.Row.FindControl("Numbering") as Literal;

                if (drv.Row["IdStatusKeputusan"].ToString() == "3")
                {
                    detailsList.Visible = false;
                }
                else if (!string.IsNullOrEmpty(drv.Row["IdStatusKeputusan"].ToString()))
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Label"), new DataColumn("Text") });
                    dt.Rows.Add("SYARIKAT BERJAYA", drv.Row["SyarikatBerjaya"].ToString());
                    dt.Rows.Add("NILAI", "RM " + string.Format("{0:#,0.00}", drv.Row["Harga"]));
                    dt.Rows.Add("TEMPOH", drv.Row["Tempoh"].ToString() + " BULAN");
                    detailsList.DataSource = dt;
                    detailsList.DataBind();
                    LblKeterangan.Visible = false;
                }
                numbering.Text = (e.Row.RowIndex + 1).ToString();
            }
        }
    }
}