using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlTypes;
using System.Drawing;
using Microsoft.AspNet.Identity;
using System.Xml.Linq;
using System.Web.DynamicData;
using System.Reflection;
using System.Security.Cryptography;
using System.Collections;
using System.Runtime.Remoting.Contexts;
using EPBM.Models;

namespace EPBM.pengguna
{
    public partial class senarai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        protected void BindData(string searchTerm=null, string searchCol=null)
        {
            //Get users
            DataTable EpbmDT = Utils.GetDataTable("Select U.Id, MAX(UserName) as UserName, STRING_AGG(R.Name, ',') AS RoleName from AspNetUsers U left join AspNetUserRoles UR on UR.UserId=U.Id left join ASpNetRoles R on UR.RoleId=R.Id group by U.Id");

            List<String> userValues = new List<String>();
            List<String> ics = new List<String>();

            foreach (DataRow row in EpbmDT.Rows)
            {
                userValues.Add("('" + row["Id"] + "','" + row["UserName"] + "','" + row["RoleName"] + "')");
                ics.Add("'" + row["Id"] + "'");
            }

            //get user info from eProfile
            /*string CommandText = "Select EU.Id, RoleName, UP.ICNO as 'NO K/P', UC.UserName as 'NAMA PENGGUNA', UC.LoginType, UserEmail as 'E-MEL', PhoneNo as 'NO. TEL', FullName as 'NAMA', Designation as 'JAWATAN', ProfileImage, O.Name as 'PENEMPATAN', OG.Name as 'JABATAN/KEMENTERIAN',  IIF(UP.Blocked='True' Or UP.Deleted='True', 1, 0) as Inactive "
                            + "from UserCredential UC, Organization O, OrganizationGroup OG, (select top " + ics.Count() + " from UserProfile where IcNo in (" + string.Join(",", ics.ToArray()) + ") group by IcNo order by Blocked asc, Deleted asc) UP "
                            + "inner join (select * from (values" + string.Join(",", userValues.ToArray()) + ") as EpbmUsers (Id, IcNo, RoleName)) as EU on EU.IcNo=UP.ICNO "
                            + "WHERE UP.UserId=UC.UserId and O.OrganizationId=UP.OrganizationId and O.GroupId=OG.GroupId";*/


            //get user info from eProfile
            string CommandText = "Select EU.Id, RoleName, UP.ICNO as 'NO K/P', UC.UserName as 'NAMA PENGGUNA', UC.LoginType, UserEmail as 'E-MEL', PhoneNo as 'NO. TEL', FullName as 'NAMA', Designation as 'JAWATAN', ProfileImage, O.Name as 'PENEMPATAN', OG.Name as 'JABATAN/KEMENTERIAN',  IIF(UP.Blocked='True' Or UP.Deleted='True', 1, 0) as Inactive "
                            + "from UserCredential UC, Organization O, OrganizationGroup OG, UserProfile UP "
                            + "inner join (select * from (values" + string.Join(",", userValues.ToArray()) + ") as EpbmUsers (Id, IcNo, RoleName)) as EU on EU.IcNo=UP.ICNO "
                            + "WHERE UP.UserId=UC.UserId and O.OrganizationId=UP.OrganizationId and O.GroupId=OG.GroupId";

            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (string.IsNullOrEmpty(searchCol))
                {
                    CommandText += " AND (UP.ICNO LIKE '%' + @searchTerm + '%' OR UserEmail LIKE '%' + @searchTerm + '%' OR FullName LIKE '%' + @searchTerm + '%' OR O.Name LIKE '%' + @searchTerm + '%' OR RoleName LIKE '%' + @searchTerm + '%')";
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
                else if (searchCol == "PENEMPATAN")
                {
                    CommandText += " AND O.Name LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "PERANAN")
                {
                    CommandText += " AND RoleName LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
            }
            DataTable dtProfile = Utils.GetDataTable(CommandText, queryParams, "NRE_ProfileConnectionString");
            dtProfile.TableName = "User";

            //get users roles
            DataTable dtRoles = Utils.GetDataTable("Select UR.UserId, R.Name from AspNetUserRoles UR left join ASpNetRoles R on UR.RoleId=R.Id");
            dtRoles.TableName = "Role";

            DataSet ds = new DataSet();
            ds.Tables.Add(dtProfile);
            ds.Tables.Add(dtRoles);
            DataRelation rel = new DataRelation("userRoles", ds.Tables["User"].Columns["Id"], ds.Tables["Role"].Columns["UserId"], false);
            ds.Relations.Add(rel);

            GridView1.DataSource = ds;
            GridView1.DataBind();
            ViewState["dtProfile"] = dtProfile;
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

            return sortDirection=="ASC" ? "&uarr;" : "&darr;";
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }
        private DataTable UserRoles
        {
            get { return ViewState["UserRoles"] != null ? (DataTable)ViewState["UserRoles"] : null; }
            set { ViewState["UserRoles"] = value; }
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ListView inner = e.Row.FindControl("RoleList") as ListView;
                Label activeLbl = e.Row.FindControl("lblStatusActive") as Label;
                Label inactiveLbl = e.Row.FindControl("lblStatusInactive") as Label;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("UserId"), new DataColumn("Role") });
                DataRowView drv = e.Row.DataItem as DataRowView;
                DataRow[] rows = drv.Row.GetChildRows("userRoles");

                foreach (DataRow row in rows)
                {
                    dt.Rows.Add(row["UserId"].ToString(), row["Name"].ToString());
                }

                inner.DataSource = dt;
                inner.DataBind();

                if (drv["Inactive"].ToString()=="1")
                {
                    activeLbl.Visible = false;
                    inactiveLbl.Visible = true;
                }
                else
                {
                    activeLbl.Visible = true;
                    inactiveLbl.Visible = false;
                }
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dtProfile"];
            if (dtrslt.Rows.Count > 0)
            {
                lblSortRecord.Text = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                dtrslt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"];
                GridView1.DataSource = dtrslt;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                Dictionary<string, string> queryParams = new Dictionary<string, string>()
                {
                    {"@Id",  lnkView.CommandArgument }
                };
                Utils.ExcuteQuery("DELETE FROM AspNetUsers WHERE Id = @Id", queryParams);
                /*LinkButton lnkView = (LinkButton)e.CommandSource;
                string dealId = lnkView.CommandArgument;
                List<Details> data = (List<Details>)ViewState["Data"];
                data.RemoveAll(d => d.Id == Convert.ToInt32(dealId));
                ViewState["Data"] = data;
                gridbind();*/
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "BootstrapDialog.alert('Record Deleted Successfully.');", true);
                //data.    
            }
        }

        protected void Search(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            BindData(txtSearch.Text.Trim(), listSearchCol.Text.Trim());
        }
    }
}