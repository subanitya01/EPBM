using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.DirectoryServices.AccountManagement;
using EPBM.auth;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace EPBM
{
    public class IdentityConfig<TUser> : UserManager<TUser> where TUser : IdentityUser
    {
        public IdentityConfig(IUserStore<TUser> store) : base(store)
        {
        }
        /*public override async Task<TUser> FindAsync(string userName, string password)
        {
            //ThrowIfDisposed();
            TUser user = await FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }

            return (await CheckPasswordAsync(user, password)) ? user : null;
        }*/

        public override Task<bool> CheckPasswordAsync(TUser user, string password)
        {
            /*PrincipalContext dc = new PrincipalContext(ContextType.Domain, "domain", "DC=wsa,DC=gov", [user_name], [password]);
            bool authenticated = dc.ValidateCredentials(user.UserName, password);
            return authenticated;*/
            string connString = ConfigurationManager.ConnectionStrings["NRE_ProfileConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //SqlCommand sqlcmd = new SqlCommand("Select UP.UserId, UC.UserName, UC.LoginType from UserProfile UP, UserCredential UC WHERE UP.UserId=UC.UserId And UP.Blocked='False' And UP.Deleted='False' And UP.ICNO='" + txtUsername.Text + "'", conn);
                //SqlCommand sqlcmd = new SqlCommand("Select UserName, LoginType, Email, FullName, Organization, Department, Position from UserProfileView WHERE Inactive=0 And ICNO=@nokp", conn);
                SqlCommand sqlcmd = new SqlCommand("Select UC.UserName, UC.LoginType, UserEmail, PhoneNo, FullName, Designation, ProfileImage, O.Name as Department, OG.Name as Organization from UserProfile UP, UserCredential UC, Organization O, OrganizationGroup OG WHERE UP.UserId=UC.UserId and O.OrganizationId=UP.OrganizationId and O.GroupId=OG.GroupId And UP.Blocked='False' And UP.Deleted='False' And UP.ICNO=@nokp", conn);
                sqlcmd.Parameters.AddWithValue("@nokp", user.UserName);
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conn.Close();

                if (dt.Rows.Count != 0)
                {
                    string username = dt.Rows[0][0].ToString().Trim();
                    string logintype = dt.Rows[0][1].ToString().Trim();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        HttpContext.Current.Session["User." + dc.ColumnName] = dt.Rows[0][dc.ColumnName].ToString();
                    }

                    if (logintype == "1") // login AD
                    {
                        return Task.FromResult(LoginAD(username, password));
                    }
                    else    // login profile
                    {
                        return Task.FromResult(LoginProfile(user.UserName, password));
                    }
                }
            }

            return Task.FromResult(false); //DB connection problem
        }

        protected bool LoginAD(string username, string password)
        {
            string adPath = "LDAP://wsa.gov:3268/dc=wsa,dc=gov"; //Path to your LDAP directory server
            LdapAuthentication adAuth = new LdapAuthentication(adPath);

            if (true == adAuth.IsAuthenticated(username, password)) //check if user exist
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