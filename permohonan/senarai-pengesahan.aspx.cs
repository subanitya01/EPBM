using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Security;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;

namespace EPBM.permohonan
{
    public partial class pengesahan : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                Load_GridData();
            }
        }

        private void Load_GridData()
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            conn.Open();

            SqlDataAdapter Sqa = new SqlDataAdapter("select * FROM Papar_Permohonan where IdStatusPermohonan = '1' ORDER BY Id DESC ", conn);
            DataSet ds = new DataSet();
            Sqa.Fill(ds);
            Senarai.DataSource = ds;
            Senarai.DataBind();
            conn.Close();

        }

        protected void Senarai_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Senarai.PageIndex = e.NewPageIndex;
            Load_GridData();
        }


        protected void Senarai_DataBound(object sender, EventArgs e)
        {

            for (int i = 0; i <= Senarai.Rows.Count - 1; i++)
            {
                ImageButton btnhapus = (ImageButton)Senarai.Rows[i].FindControl("btnhapus");
                Label lblStatus = (Label)Senarai.Rows[i].FindControl("lblStatus");
                Label lblIDStatus = (Label)Senarai.Rows[i].FindControl("lblIDStatus");

                if (lblIDStatus.Text == "1")
                {
                    lblStatus.CssClass = "badge text-bg-primary";

                }

                if (lblIDStatus.Text == "2")
                {
                    lblStatus.CssClass = "badge text-bg-warning";

                }

                if (lblIDStatus.Text == "3")
                {
                    lblStatus.CssClass = "badge text-bg-success";

                }

                if (lblIDStatus.Text == "4")
                {
                    lblStatus.CssClass = "badge text-bg-info";

                }

            }

        }



    }
}