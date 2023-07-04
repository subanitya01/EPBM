using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace EPBM
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Bind_UserData();
        }


        protected void Bind_UserData()
        {
            string nokp = Session["nokp"].ToString();

            string connString = ConfigurationManager.ConnectionStrings["NRE_ProfileConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand sqlcmd = new SqlCommand("SELECT FullName, ProfileImage FROM UserProfile WHERE ICNO='" + nokp + "'", conn);
                conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conn.Close();

                if (dt.Rows.Count != 0)
                {
                    string fullName = dt.Rows[0][0].ToString().Trim();
                    string userImg = dt.Rows[0][1].ToString().Trim();

                    lblUsername.Text = fullName;

                    if (userImg != "")
                    {
                        imgUserImage.ImageUrl = userImg;
                    }

                }
            }
        }

    }
}