using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.auth
{
    public partial class profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationUserManager UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            string[] currentUserRoles = UserManager.GetRoles(HttpContext.Current.User.Identity.GetUserId()).ToArray();
            foreach (string role in currentUserRoles)
            {
                ListItem li = new ListItem();
                li.Attributes["class"] = "list-group-item";
                li.Text = role;
                ListPeranan.Items.Add(li);
            }
        }
    }
}