using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace EPBM
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["Profile.UserName"])) logout();

            if (!IsPostBack)
            {
                string noKP = (string)Session["nokp"];

                if (!string.IsNullOrEmpty((string)Session["Profile.UserName"]))
                {
                    //string[] currentUserRoles = Roles.GetRolesForUser();
                    ApplicationUserManager UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    string[] currentUserRoles = UserManager.GetRoles(HttpContext.Current.User.Identity.GetUserId()).ToArray();
                    if (currentUserRoles.Length > 0)
                        currentUserRole.Text = "<br/>" + currentUserRoles[0];
                }
                if (!String.IsNullOrWhiteSpace(noKP))
                {
                    Enable_Panel();
                }

                /*if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/auth/login.aspx", false);
                }*/

            }
        }

        protected void Enable_Panel()
        {
            if (Session["level"] != null)
            {
                ArrayList levelList = (ArrayList)Session["level"];
                List<int> _levelList = new List<int>();

                for (int x = 0; x < levelList.Count; x++)
                    _levelList.Add(SystemHelper.GetInteger(levelList[x].ToString()));

                pnlDashboard.Visible = IsRoleIncluded(_levelList, 1, 2, 3);
                pnlUrusetia.Visible = IsRoleIncluded(_levelList, 1, 2);
                pnlSenaraiPerolehan.Visible = IsRoleIncluded(_levelList, 1, 2, 3);
                pnlPenyemak.Visible = IsRoleIncluded(_levelList, 1, 3);
                pnlDaftarMesyuarat.Visible = IsRoleIncluded(_levelList, 1, 2);
                pnlKeputusanMesyuarat.Visible = IsRoleIncluded(_levelList, 1, 2);
                pnlPerakuanMesyuarat.Visible = IsRoleIncluded(_levelList, 1, 3);
                pnlSenaraiKeputusan.Visible = IsRoleIncluded(_levelList, 1, 2, 3, 4);
                pnlSST.Visible = IsRoleIncluded(_levelList, 1, 2, 3);
                pnlLaporan.Visible = IsRoleIncluded(_levelList, 1, 2, 3, 4);
                pnlNamaPermohonan.Visible = IsRoleIncluded(_levelList, 1,2,3);
                pnlNamaMesyuarat.Visible = IsRoleIncluded(_levelList, 1,2,3);
                pnlAdmin.Visible = IsRoleIncluded(_levelList, 1);

            }
        }




        protected void LinkButton1_Command(object sender, EventArgs e)
        {
            logout();
        }

        protected void logout()
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            Session.Abandon();
            Response.Cookies.Clear();
            Response.Redirect("~/auth/login.aspx", false);
        }

        #region Added By Inteksoft

        private bool IsRoleIncluded(List<int> levelList, params int[] roleIds)
        {
            for (int i = 0; i < roleIds.Length; i++)
            {
                if (levelList.Contains(roleIds[i]))
                    return true;
            }
            return false;
        }

        #endregion
    }
}