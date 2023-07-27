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

namespace EPBM.auth
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User.Id"] != null)
                {
                    Response.Redirect("~/index.aspx", false);
                }
            }

        }
        protected void Login_Click(object sender, EventArgs e)
        {

            //string adPath = "LDAP://wsa.gov:3268/dc=wsa,dc=gov"; //Path to your LDAP directory server
            //LdapAuthentication adAuth = new LdapAuthentication(adPath);
            try
            {
                if (txtUsername.Text.Length != 0 && txtPassword.Text.Length != 0) // check if textbox is not empty
                {
                    //check kod_sub_Pengguna
                    if (!checkDb(txtUsername.Text)) throw new HttpException(404, "Pengguna tidak wujud di pangkalan data");

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
                                if(!LoginAD(username)) 
                                    throw new Exception("Pengguna tidak wujud di Active Directory");
                            }
                            else    // login profile
                            {
                                if(!LoginProfile(txtUsername.Text, txtPassword.Text)) 
                                    throw new Exception("Pengguna tidak wujud di Sistem Profil");
                            }

                            //Create the ticket.
                            bool isCookiePersistent = chkPersist.Checked;
                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
                                      txtUsername.Text, DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, null);

                            //Encrypt the ticket.
                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                            //Create a cookie, and then add the encrypted ticket to the cookie as data.
                            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                            if (true == isCookiePersistent)
                                authCookie.Expires = authTicket.Expiration;

                            //Add the cookie to the outgoing cookies collection.
                            Response.Cookies.Add(authCookie);

                            Response.Redirect("~/Index.aspx", false);
                        }
                    }
                }

                else if (txtUsername.Text.Length == 0) //check if textbox username is empty
                {
                    throw new Exception("Sila masukkan No Kad Pengenalan.");

                }
                else if (txtPassword.Text.Length == 0) //check if textbox password is empty
                {

                    throw new Exception("Sila masukkan Kata Laluan.");
                }
            }
            catch (Exception)
            {
                errorLabel.Text = "No Kad Pengenalan dan/atau Kata laluan tidak sah.";
            }
        }

        protected bool LoginAD(string username)
        {
            string adPath = "LDAP://wsa.gov:3268/dc=wsa,dc=gov"; //Path to your LDAP directory server
            LdapAuthentication adAuth = new LdapAuthentication(adPath);

            if (true == adAuth.IsAuthenticated(username, txtPassword.Text)) //check if user exist
            {
                return true;
            }
            else { return false; }
        }

        protected bool LoginProfile(string nokp, string password)
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
                    return true;
                }
            }
            return false;
        }

        protected bool checkDb(string nokp)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ePBM_Conn"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM Pengguna WHERE NoKP=@nokp and Aktif=1", conn);

                    //SqlCommand cmd = new SqlCommand("SELECT User_Roles_ID FROM CR_User WHERE NoIC=@noic ", conn);

                    cmd.Parameters.AddWithValue("@nokp", nokp);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ArrayList levelList = new ArrayList();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            Session["User."+dc.ColumnName] = dt.Rows[0][dc.ColumnName].ToString();
                        }
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }

            return false;
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
    }
}