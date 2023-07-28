using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;

using System.Globalization;

namespace EPBM.Permohonan
{
    public partial class senarai : System.Web.UI.Page
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

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ePBM_Conn"].ConnectionString);

            conn.Open();

            SqlDataAdapter Sqa = new SqlDataAdapter("select * FROM Papar_Permohonan", conn);
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


    }
}