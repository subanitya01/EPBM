using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace EPBM
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["Profile.UserName"] as string))
                logout();
            if (!IsPostBack)
            {

                /*if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/auth/login.aspx", false);
                }*/

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
    }
}