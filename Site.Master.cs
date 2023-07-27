using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User.Id"] == null)
                {
                    Response.Redirect("~/auth/login.aspx", false);
                }
            }
        }

        protected void LinkButton1_Command(object sender, EventArgs e)
        {
            Session.Abandon();
            Request.Cookies.Clear();
            Response.Redirect("/auth/login.aspx");
        }
    }
}