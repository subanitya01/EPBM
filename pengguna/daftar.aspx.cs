using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.pengguna
{
    public partial class daftar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initRoleList();
            }
        }

        protected void initRoleList()
        {
            DataTable dtRoles = Utils.GetDataTable("Select * from AspNetRoles");

            foreach (DataRow row in dtRoles.Rows)
            {
                ListItem item = new ListItem();
                item.Text = row["Name"].ToString();
                //item.Value = row["Id"].ToString();
                //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                CheckBoxList1.Items.Add(item);
            }
        }
        protected void BindData(string searchTerm = null, string searchCol = null, bool count=false)
        {
            //Get users
            DataTable EpbmDT = Utils.GetDataTable("Select U.Id, MAX(UserName) as UserName, STRING_AGG(R.Name, ',') AS RoleName from AspNetUsers U left join AspNetUserRoles UR on UR.UserId=U.Id left join ASpNetRoles R on UR.RoleId=R.Id group by U.Id");

            List<String> userValues = new List<String>();

            foreach (DataRow row in EpbmDT.Rows)
            {
                userValues.Add("(" + row["Id"] + ",'" + row["UserName"] + "','" + row["RoleName"] + "')");
            }
            //get user info from eProfile
            string selectData = "Select UP.ICNO as 'NO K/P', UserEmail as 'E-MEL', FullName as 'NAMA', O.Name as 'PENEMPATAN', OG.Name as 'JABATAN/KEMENTERIAN', EU.Id as 'Exist' ";
            string CommandText = "from UserCredential UC, Organization O, OrganizationGroup OG, UserProfile UP "
                            + "left join (select * from (values" + string.Join(",", userValues.ToArray()) + ") as EpbmUsers (Id, IcNo, RoleName)) as EU on EU.IcNo=UP.ICNO "
                            + "WHERE UP.UserId=UC.UserId and O.OrganizationId=UP.OrganizationId and O.GroupId=OG.GroupId And UP.Blocked='False' And UP.Deleted='False'";
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (string.IsNullOrEmpty(searchCol))
                {
                    CommandText += " AND (UP.ICNO LIKE '%' + @searchTerm + '%' OR UserEmail LIKE '%' + @searchTerm + '%' OR FullName LIKE '%' + @searchTerm + '%')";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "NO K/P")
                {
                    CommandText += " AND UP.ICNO LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "E-MEL")
                {
                    CommandText += " AND UserEmail LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "NAMA")
                {
                    CommandText += " AND FullName LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
            }
            string limit = " ORDER BY FullName OFFSET  " + (GridView1.PageIndex * GridView1.PageSize) + " ROWS FETCH NEXT " + GridView1.PageSize + " ROWS ONLY";
            
            if (count)
            {
                string selectCount = "Select count(*) ";
                DataTable dtCountProfile = Utils.GetDataTable(selectCount + CommandText, queryParams, "NRE_ProfileConnectionString");
                GridView1.VirtualItemCount = Convert.ToInt16(dtCountProfile.Rows[0][0].ToString());
            }

            DataTable dtProfile = Utils.GetDataTable(selectData + CommandText + limit, queryParams, "NRE_ProfileConnectionString");

            GridView1.DataSource = dtProfile;
            GridView1.DataBind();
            ViewState["dtProfile"] = dtProfile;
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData(txtSearch.Text.Trim(), listSearchCol.Text.Trim());
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton addBtn = e.Row.FindControl("BtnAddUser") as LinkButton;
                Label existLbl = e.Row.FindControl("lblExist") as Label;
                DataRowView drv = e.Row.DataItem as DataRowView;

                if (!String.IsNullOrEmpty(drv["Exist"].ToString()))
                {
                    addBtn.Visible = false;
                    existLbl.Visible = true;
                }
                else
                {
                    addBtn.Visible = true;
                    existLbl.Visible = false;
                }
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0; 
            Panel2.Visible = true;
            BindData(txtSearch.Text.Trim(), listSearchCol.Text.Trim(), true);
        }

        protected void BtnAddUser_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string IcNo = btn.CommandArgument.ToString();
            string Email = btn.CommandName;
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { Id= IcNo, UserName = IcNo, Email = Email };
            IdentityResult result = manager.Create(user);
            
            if (result.Succeeded)
            {
                var currentUser = manager.FindByName(user.UserName);

                foreach (ListItem item in CheckBoxList1.Items)
                {
                    if (item.Selected)
                        manager.AddToRole(currentUser.Id, item.Value);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "window.notyf.success('Pengguna berjaya ditambah!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "window.notyf.error('" + result.Errors.FirstOrDefault() + "');", true);
            }
        }
    }
}