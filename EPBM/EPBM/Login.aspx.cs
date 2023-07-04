using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;
using FormsAuth;
using System.Web.Security;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Collections;

namespace EPBM
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnShowPopup.Attributes.Add("onmouseover", "ShowPopup();");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "$(function() { popup(); });", true);
        }



        protected void Login_Click(object sender, EventArgs e)
        {

            //string adPath = "LDAP://wsa.gov:3268/dc=wsa,dc=gov"; //Path to your LDAP directory server
            //LdapAuthentication adAuth = new LdapAuthentication(adPath);
            try
            {
                if (txtUsername.Text.Length != 0 && txtPassword.Text.Length != 0) // check if textbox is not empty
                {
                    string connString = ConfigurationManager.ConnectionStrings["NRE_ProfileConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        SqlCommand sqlcmd = new SqlCommand("Select UP.UserId, UC.UserName, UC.LoginType from UserProfile UP, UserCredential UC WHERE UP.UserId=UC.UserId And UP.Blocked='False' And UP.Deleted='False' And UP.ICNO='" + txtUsername.Text + "'", conn);
                        conn.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        conn.Close();

                        if (dt.Rows.Count != 0)
                        {
                            string userid = dt.Rows[0][0].ToString().Trim();
                            string username = dt.Rows[0][1].ToString().Trim();
                            string logintype = dt.Rows[0][2].ToString().Trim();

                            if (logintype == "1") // login AD
                            {
                                LoginAD(username);
                            }
                            else    // login profile
                            {
                                LoginProfile(txtUsername.Text, txtPassword.Text);
                            }
                        }
                    }
                }

                else if (txtUsername.Text.Length == 0) //check if textbox username is empty
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "$(function() { hide(); });", true);
                    errorLabel.Text = "No Kad Pengenalan dan/atau Kata laluan tidak sah."; //"+ ex ;
                    // ScriptManager.RegisterStartupScript(this, this.GetType(),
                    //"alert",
                    //"swal('Sila isi medan No Kad Pengenalan !', '', 'warning').then((value) => { window.location ='Login.aspx'; });",
                    //true);

                }
                else if (txtPassword.Text.Length == 0) //check if textbox password is empty
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "$(function() { hide(); });", true);
                    errorLabel.Text = "No Kad Pengenalan dan/atau Kata laluan tidak sah."; //"+ ex ;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(),
                    //"alert",
                    //"swal('Sila isi medan Kata Laluan !', '', 'warning').then((value) => { window.location ='Login.aspx'; });",
                    //true);
                }
            }
            catch (Exception ex)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "$(function() { hide(); });", true);
                errorLabel.Text = "No Kad Pengenalan dan/atau Kata laluan tidak sah."; //"+ ex ;
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //       "alert",
                //       "swal('No Kad Pengenalan atau Kata laluan tidak sah !', '', 'warning').then((value) => { window.location ='Login.aspx'; });",
                //       true);


            }
        }

        protected void LoginAD(string username)
        {
            string adPath = "LDAP://wsa.gov:3268/dc=wsa,dc=gov"; //Path to your LDAP directory server
            LdapAuthentication adAuth = new LdapAuthentication(adPath);

            if (true == adAuth.IsAuthenticated(username, txtPassword.Text)) //check if user exist
            {
                string groups = adAuth.GetGroups();
                Session["nokp"] = txtUsername.Text; // set session user

                //check kod_sub_Pengguna
                check_Kod_SubPengguna(txtUsername.Text);

                //Create the ticket, and add the groups.
                bool isCookiePersistent = chkPersist.Checked;
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
                          txtUsername.Text, DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);

                //Encrypt the ticket.
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                //Create a cookie, and then add the encrypted ticket to the cookie as data.
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                if (true == isCookiePersistent)
                    authCookie.Expires = authTicket.Expiration;

                //Add the cookie to the outgoing cookies collection.
                Response.Cookies.Add(authCookie);

                //You can redirect now.
                //Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text, false));
                Response.Redirect("~/DaftarPerolehan.aspx", false);
            }
        }

        protected void LoginProfile(string nokp, string password)
        {
            string hashPassword = GenerateSHA256String(password);
            string connString = ConfigurationManager.ConnectionStrings["NRE_ProfileConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand sqlcmd = new SqlCommand("Select UP.ICNO, UC.UserPassword from UserProfile UP, UserCredential UC WHERE UP.UserId=UC.UserId And UP.Blocked='False' And UP.Deleted='False' And UP.ICNO='" + nokp + "' AND UC.UserPassword='" + hashPassword + "'", conn);
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conn.Close();

                if (dt.Rows.Count != 0)
                {
                    Session["nokp"] = nokp;
                    //check kod_sub_Pengguna
                    check_Kod_SubPengguna(nokp);
                    Response.Redirect("~/DaftarPerolehan.aspx", false);
                }
                else
                {
                    errorLabel.Text = "No Kad Pengenalan dan/atau Kata laluan tidak berpadanan. Sila cuba sekali lagi";
                }
            }
        }

        protected void check_Kod_SubPengguna(string nokp)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRS_Conn"].ConnectionString))
                {
                    conn.Open();

                    //Session["nokp"] = nokp;

                    //SqlCommand cmd = new SqlCommand("SELECT Kod_Pengguna FROM Pengguna WHERE NoIC= '" + nokp + "' ", conn);

                    SqlCommand cmd = new SqlCommand("SELECT User_Roles_ID FROM CR_User WHERE NoIC=@noic ", conn);

                    cmd.Parameters.AddWithValue("@noic", nokp);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ArrayList levelList = new ArrayList();

                    if (dt.Rows.Count > 0)
                    {
                        //Session["level"] = dt.Rows[0][0].ToString();
                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            levelList.Add(dt.Rows[x][0].ToString());
                        }

                    }
                    else
                    {
                        //Session["level"] = "3";
                        levelList.Add("3");
                    }

                    Session["level"] = levelList;
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void AuthenticateProfile(string nokp, string password)
        {
            string connString = ConfigurationManager.ConnectionStrings["NRE_ProfileConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand sqlcmd = new SqlCommand("Select UP.ICNO, UC.UserPassword from UserProfile UP, UserCredential UC WHERE UP.UserId=UC.UserId And UP.Blocked='False' And UP.Deleted='False' And UP.ICNO='" + nokp + "' AND UC.UserPassword='" + password + "'", conn);
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conn.Close();

                if (dt.Rows.Count != 0)
                {
                    //berjaya
                }
                else
                {
                    //gagal
                }
            }
        }

        protected string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }


        protected string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}