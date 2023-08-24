using System;
using System.Collections.Generic;
using System.Data;
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
                    CommandText += " AND (JENIS LIKE '%' + @searchTerm + '%' OR BILANGAN LIKE '%' + @searchTerm + '%' OR TARIKHMS LIKE '%' + @searchTerm + '%' OR PENGERUSI LIKE '%' + @searchTerm + '%')";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "JENIS")
                {
                    CommandText += " AND JENIS LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "BILANGAN")
                {
                    CommandText += " AND BILANGAN LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
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
            }
            CommandText += " order by Tarikh desc";
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
            Utils.ExcuteQuery("UPDATE Mesyuarat SET TarikhHapus = GETDATE() WHERE Id = @Id", queryParams);
            BindData();
        }
    }
}