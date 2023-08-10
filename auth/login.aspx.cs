using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.DynamicData;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EPBM.Models;
using WebApplication1;

namespace EPBM.auth
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/default.aspx", false);
                }
            }

        }
        protected void Login_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {

                string connString = ConfigurationManager.ConnectionStrings["NRE_ProfileConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand sqlcmd = new SqlCommand("Select UC.UserName, UC.LoginType, UserEmail, PhoneNo, FullName, Designation, ProfileImage, O.Name as Department, OG.Name, UP.ICNO as Organization from UserProfile UP, UserCredential UC, Organization O, OrganizationGroup OG WHERE UP.UserId=UC.UserId and O.OrganizationId=UP.OrganizationId and O.GroupId=OG.GroupId And UP.Blocked='False' And UP.Deleted='False' And UP.ICNO=@nokp", conn);
                    sqlcmd.Parameters.AddWithValue("@nokp", txtUsername.Text);
                    conn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    conn.Close();

                    if (dt.Rows.Count != 0)
                    {

                        var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                        var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                        manager.LoginType = dt.Rows[0][1].ToString().Trim();
                        manager.ProfileUsername = dt.Rows[0][0].ToString().Trim();

                        // This doen't count login failures towards account lockout
                        // To enable password failures to trigger lockout, change to shouldLockout: true
                        var result = signinManager.PasswordSignIn(txtUsername.Text, txtPassword.Text, chkPersist.Checked, shouldLockout: false);


                        switch (result)
                        {
                            case SignInStatus.Success:
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    Session["Profile." + dc.ColumnName] = dt.Rows[0][dc.ColumnName].ToString();
                                    //Response.Cookies["Profile." + dc.ColumnName].Value = dt.Rows[0][dc.ColumnName].ToString();
                                }

                                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                                break;
                            case SignInStatus.LockedOut:
                                errorLabel.Text = "Akaun anda telah dikunci. Sila cuba lagi selepas 5 minit.";
                                break;
                            case SignInStatus.Failure:
                            default:
                                errorLabel.Text = "No Kad Pengenalan dan/atau Kata laluan tidak sah.";
                                break;
                        }
                    }
                }
            }
        }
    }
}