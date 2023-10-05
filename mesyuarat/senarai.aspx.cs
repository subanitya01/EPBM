using EPBM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.mesyuarat
{
    public partial class senarai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        protected void BindData()
        {
            string searchTerm = Convert.ToString(ViewState["txtSearch"]) ?? null;
            string searchCol = Convert.ToString(ViewState["listSearchCol"]) ?? null;

            string CommandText = "Select * from PaparMesyuarat WHERE 1=1";

            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (string.IsNullOrEmpty(searchCol))
                {
                    //CommandText += " AND (JENIS LIKE '%' + @searchTerm + '%' OR BILANGAN LIKE '%' + @searchTerm + '%' OR TARIKHMS LIKE '%' + @searchTerm + '%' OR PENGERUSI LIKE '%' + @searchTerm + '%')";
                    CommandText += " AND (MESYUARAT LIKE '%' + @searchTerm + '%' OR TARIKHMS LIKE '%' + @searchTerm + '%' OR PENGERUSI LIKE '%' + @searchTerm + '%')";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "MESYUARAT")
                {
                    CommandText += " AND MESYUARAT LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                /*else if (searchCol == "JENIS")
                {
                    CommandText += " AND JENIS LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "BILANGAN")
                {
                    CommandText += " AND BILANGAN LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }*/
                else if (searchCol == "TARIKH")
                {
                    CommandText += " AND TARIKHMS LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "PENGERUSI")
                {
                    CommandText += " AND PENGERUSI LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "STATUS PENGESAHAN")
                {
                    CommandText += " AND StatusPengesahan LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
            }
            CommandText += " order by Tarikh desc, Id desc";
            DataTable dtMesyuarat = Utils.GetDataTable(CommandText, queryParams);

            GridView1.DataSource = dtMesyuarat;
            GridView1.DataBind();
            ViewState["dtMesyuarat"] = dtMesyuarat;
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
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                Label lblStatus = e.Row.FindControl("lblStatus") as Label;
                HyperLink viewButton = e.Row.FindControl("viewButton") as HyperLink;
                HyperLink editButton = e.Row.FindControl("editButton") as HyperLink;
                HyperLink decideButton = e.Row.FindControl("decideButton") as HyperLink;
                LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;

                viewButton.NavigateUrl = "/mesyuarat/papar.aspx?id=" + drv.Row["Id"];
                editButton.NavigateUrl = "/mesyuarat/edit.aspx?id=" + drv.Row["Id"];
                decideButton.NavigateUrl = "/mesyuarat/senarai-keputusan.aspx?id=" + drv.Row["Id"];

                if (Convert.ToInt32(drv.Row["IdStatusPengesahan"]) == 1)
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-info";
                else if (Convert.ToInt32(drv.Row["IdStatusPengesahan"]) == 2)
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-primary";
                else if (Convert.ToInt32(drv.Row["IdStatusPengesahan"]) == 3)
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-warning";
                else if (Convert.ToInt32(drv.Row["IdStatusPengesahan"]) == 4)
                {
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-success";
                    editButton.Visible = false;
                    lnkDelete.Visible = false;
                }

            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dtMesyuarat"];
            if (dtrslt.Rows.Count > 0)
            {
                lblSortRecord.Text = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                dtrslt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"];
                GridView1.DataSource = dtrslt;
                GridView1.DataBind();
            }
        }

        protected void Search(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;

            ViewState["txtSearch"] = txtSearch.Text.Trim();
            ViewState["listSearchCol"] = listSearchCol.Text.Trim();
            BindData();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                {
                    {"@Id",  btn.CommandArgument }
                };
            DataRow[] dtrslt = ((DataTable)ViewState["dtMesyuarat"]).Select("Id = " + btn.CommandArgument);
            
            if (Convert.ToInt32(dtrslt[0]["IdStatusPengesahan"]) != 4)
            {
                Utils.ExcuteQuery("UPDATE Mesyuarat SET TarikhHapus = GETDATE() WHERE Id = @Id", queryParams);
                Utils.ExcuteQuery("UPDATE Permohonan SET IdMesyuarat = NULL, IdStatusPermohonan=3 WHERE IdMesyuarat = @Id", queryParams);
                Session["flash.success"] = "Mesyuarat " + dtrslt[0]["JENIS"] + " Bil. " + dtrslt[0]["BILANGAN"] + " telah dihapuskan!";
                Response.Redirect("/mesyuarat/senarai.aspx");
            }
        }
    }
}