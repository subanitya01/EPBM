using EPBM.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace EPBM.mesyuarat
{
    public partial class daftar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(User.IsInRole("Administrator") || User.IsInRole("Urusetia")))
            {
                Utils.HttpNotFound();
            }
            if (!IsPostBack)
            {
                initTypeList();
                BindData();
            }
        }

        protected void initTypeList()
        {
            DataTable dtRoles = Utils.GetDataTable("Select * from JenisMesyuarat");
            ddlJenis.Items.Add(new ListItem("SILA PILIH", ""));

            foreach (DataRow row in dtRoles.Rows)
            {
                ListItem item = new ListItem(row["Nama"].ToString(), row["Id"].ToString());
                //item.Value = row["Id"].ToString();
                //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                ddlJenis.Items.Add(item);
            }
        }

        protected void Save(object sender, EventArgs e)
        {
            /*Mesyuarat reg = new Mesyuarat();

            reg.Jenis = ddlJenis.SelectedValue.ToString();
            reg.Bil = txtBil.Text.ToString();
            reg.Tahun = txtTahun.Text.ToString();
            reg.Tarikh = txtTarikh.Text.ToString();

            var context = new ValidationContext(reg, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(reg, context, results, true);*/
            Page.Validate();
            if (Page.IsValid && !string.IsNullOrEmpty(Session["Profile.ICNO"] as string))
            {
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                {
                    {"@Jenis",  ddlJenis.SelectedValue },
                    {"@Bil",  txtBil.Text },
                    {"@Tahun",  txtTahun.Text },
                    {"@Tarikh",  txtTarikh.Text },
                    {"@TarikhCipta", DateTime.Now },
                    {"@DiciptaOleh", Session["Profile.ICNO"] },
                };
                DataTable dtMesyuarat = Utils.GetDataTable("Select count(*) as total from Mesyuarat where IdJenisMesyuarat=@Jenis AND Bilangan=@Bil AND Tahun=@Tahun AND TarikhHapus IS NULL", queryParams);
                if (dtMesyuarat.Rows.Count == 0 || Convert.ToInt32(dtMesyuarat.Rows[0]["total"]) == 0)
                {
                    Utils.ExcuteQuery("INSERT INTO Mesyuarat(IdJenisMesyuarat, Bilangan, Tahun, Tarikh, TarikhDicipta, DiciptaOleh) VALUES(@Jenis, @Bil, @Tahun, @Tarikh, @TarikhCipta, @DiciptaOleh)", queryParams);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "hideForm", "localStorage.removeItem('open_addMeeting');", true);
                    Session["flash.success"] = "Mesyuarat berjaya ditambah!";
                    Response.Redirect("/mesyuarat/daftar.aspx?r=success");
                }
                else
                {
                    ErrorList.Items.Clear();
                    ErrorList.Items.Add(new ListItem("Mesyuarat telah wujud. Sila semak senarai mesyuarat untuk mengelakkan pertindihan."));
                    errorMsg.Visible = true;
                }
            }
            /*else
            {
                ListItem msg = new ListItem();

                foreach (var validationResult in results)
                {
                    ErrorList.Items.Add(new ListItem(validationResult.ErrorMessage.ToString()));
                }
                errorMsg.Visible = true;
            }*/
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
                }

                if (new[] { 2, 4 }.Contains(Convert.ToInt32(drv.Row["IdStatusPengesahan"])))
                {
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
                Utils.ExcuteQuery("UPDATE Permohonan SET " +
                    "IdMesyuarat = NULL, " +
                    "IdStatusKeputusan = NULL, " +
                    "SyarikatBerjaya = NULL, " +
                    "Tempoh = NULL, " +
                    "LampiranKeputusan = NULL, " +
                    "TarikhSuratSetujuTerima = NULL, " +
                    "RujukanSuratSetujuTerima = NULL, " +
                    "AlasanKeputusan = NULL, " +
                    "IdStatusPermohonan=3 " +
                    "WHERE IdMesyuarat = @Id", queryParams);
                Session["flash.success"] = "Mesyuarat " + dtrslt[0]["JENIS"] + " Bil. " + dtrslt[0]["BILANGAN"] + " telah dihapuskan!";
                Response.Redirect("~/mesyuarat/daftar.aspx");
            }
        }
    }
}