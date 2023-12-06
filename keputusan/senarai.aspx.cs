using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.keputusan
{
    public partial class senarai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initDropdownList();
                ViewState["extendSearch"] = false;
                BindData();
            }
        }

        protected void initDropdownList()
        {
            string CommandText = "Select * from PaparMesyuarat WHERE IdStatusPengesahan=4 ORDER BY Id";
            DataTable dtMesyuarat = Utils.GetDataTable(CommandText);
            ViewState["dtMesyuarat"] = dtMesyuarat;
            //bool selected = false;
            ListItem item = new ListItem("SILA PILIH", "");
            listMesyuarat.Items.Add(item);

            foreach (DataRow row in dtMesyuarat.Rows)
            {
                item = new ListItem(row["MESYUARAT"].ToString(), row["Id"].ToString());
                //item.Value = row["Id"].ToString();
                /*if (!selected)
                {
                    item.Selected = true;
                    //selected = true;
                }*/
                listMesyuarat.Items.Add(item);
            }

            string CommandText2 = "Select * from Jabatan WHERE aktif=1 ORDER BY Organisasi_Grp_ID";
            DataTable dtJabatan = Utils.GetDataTable(CommandText2);
            
            ListItem item2 = new ListItem("SILA PILIH", "");
            listJabatan.Items.Add(item2);

            foreach (DataRow row in dtJabatan.Rows)
            {
                item2 = new ListItem(row["Nama"].ToString(), row["Organisasi_Grp_ID"].ToString());
                listJabatan.Items.Add(item2);
            }

            string CommandText3 = "Select * from Bahagian where Organisasi_Grp_ID=1 ORDER BY Id";
            DataTable dtBahagian = Utils.GetDataTable(CommandText3);
            
            ListItem item3 = new ListItem("SILA PILIH BAHAGIAN", "");
            listBahagian.Items.Add(item3);

            foreach (DataRow row in dtBahagian.Rows)
            {
                item3 = new ListItem(row["Nama"].ToString(), row["Id"].ToString());
                listBahagian.Items.Add(item3);
            }

            string CommandText4 = "Select * from StatusKeputusan ORDER BY Id";
            DataTable dtStatus = Utils.GetDataTable(CommandText4);
            
            ListItem item4 = new ListItem("SILA PILIH", "");
            listStatus.Items.Add(item4);

            foreach (DataRow row in dtStatus.Rows)
            {
                item4 = new ListItem(row["Nama"].ToString(), row["Id"].ToString());
                listStatus.Items.Add(item4);
            }
        }
        protected void BindData()
        {
            /*try
            {*/
            string searchTerm = Convert.ToString(ViewState["txtSearch"]) ?? null;
            string searchCol = Convert.ToString(ViewState["listSearchCol"]) ?? null;
            bool extendSearch = Convert.ToBoolean(ViewState["extendSearch"]);
            string sortDir = ViewState["SortDirection"] as string;
            string sortBy = ViewState["SortExpression"] as string;
            string selectData = "Select Id, Tajuk, CASE WHEN IdJabatan = 1 THEN NamaPendekBahagian ELSE ShortName END as Jabatan, IdStatusKeputusan, PBM as MUKTAMAD, " +
                                "StatusKeputusan as STATUS, SyarikatBerjaya, NilaiTawaran, Tempoh, MOFSyarikatDiperaku, MOFNilaiTawaran, MOFTempoh, IdPBMMuktamad, AlasanKeputusan as KETERANGAN, MESYUARAT ";
            string CommandText = "from Papar_Permohonan WHERE TarikhHapus IS NULL AND IdStatusPengesahan = 4 ";
            string limit = " OFFSET  " + (GridView1.PageIndex * GridView1.PageSize) + " ROWS FETCH NEXT " + GridView1.PageSize + " ROWS ONLY";

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
                    if(CondSyarikat == "SAMA DENGAN")
                        CommandText += " AND SyarikatBerjaya = @Syarikat";
                    else if(CondSyarikat == "BERMULA DENGAN")
                        CommandText += " AND SyarikatBerjaya LIKE @Syarikat + '%'";
                    else if(CondSyarikat == "BERAKHIR DENGAN")
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
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            lblSortRecord.Text = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            BindData();
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                Label lblStatus = e.Row.FindControl("lblStatus") as Label;
                Label LblKeterangan = e.Row.FindControl("LblKeterangan") as Label;
                ListView detailsList = e.Row.FindControl("DetailsList") as ListView;
                Literal numbering = e.Row.FindControl("Numbering") as Literal;
                
                if (drv.Row["IdStatusKeputusan"].ToString() == "1")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-info";
                else if (drv.Row["IdStatusKeputusan"].ToString() == "2")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-success";
                else if (drv.Row["IdStatusKeputusan"].ToString() == "5")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-warning";
                else
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-danger";

                if (drv.Row["IdStatusKeputusan"].ToString() == "3" || drv.Row["IdStatusKeputusan"].ToString() == "5")
                {
                    detailsList.Visible = false;
                }
                else if (!string.IsNullOrEmpty(drv.Row["IdStatusKeputusan"].ToString()))
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Label"), new DataColumn("Text") });
                    if (drv.Row["IdPBMMuktamad"].ToString() == "1")
                    {
                        dt.Rows.Add("SYARIKAT BERJAYA", drv.Row["SyarikatBerjaya"].ToString());
                        dt.Rows.Add("NILAI TAWARAN", "RM " + string.Format("{0:#,0.00}", drv.Row["NilaiTawaran"]));
                        dt.Rows.Add("TEMPOH", drv.Row["Tempoh"].ToString() + " BULAN");
                    }
                    else
                    {
                        dt.Rows.Add("SYARIKAT DIPERAKU", drv.Row["MOFSyarikatDiperaku"].ToString());
                        dt.Rows.Add("NILAI TAWARAN", "RM " + string.Format("{0:#,0.00}", drv.Row["MOFNilaiTawaran"]));
                        dt.Rows.Add("TEMPOH", drv.Row["MOFTempoh"].ToString() + " BULAN");
                    }
                    detailsList.DataSource = dt;
                    detailsList.DataBind();
                    LblKeterangan.Visible = false;
                }
                numbering.Text = (GridView1.PageIndex * GridView1.PageSize + e.Row.RowIndex + 1).ToString();
            }
        }
        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection == "ASC" ? "&uarr;" : "&darr;";
        }

        protected void Search(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;

            ViewState["txtSearch"] = txtSearch.Text.Trim();
            ViewState["listSearchCol"] = listSearchCol.Text.Trim();
            ViewState["extendSearch"] = false;
            btnCetak.CommandArgument = "basic";
            BindData();
        }

        protected void SearchExtend(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;

            ViewState["txtSearch"] = txtSearch.Text.Trim();
            ViewState["listSearchCol"] = listSearchCol.Text.Trim();
            ViewState["listMesyuarat"] = listMesyuarat.Text.Trim();
            ViewState["txtTajuk"] = txtTajuk.Text.Trim();
            ViewState["listJabatan"] = listJabatan.Text.Trim();
            ViewState["listBahagian"] = listBahagian.Text.Trim();
            ViewState["listStatus"] = listStatus.Text.Trim();
            ViewState["listCondSyarikat"] = listCondSyarikat.Text.Trim();
            ViewState["txtSyarikat"] = txtSyarikat.Text.Trim();
            ViewState["extendSearch"] = true;
            btnCetak.CommandArgument = "extend";
            BindData();
        }
    }
}