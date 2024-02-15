using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;



namespace EPBM.SyarikatBerjayaSblm2023
{
    public partial class daftar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {          
                ddlJenis_Perolehan();             
                Bind_ddlJabatan();
                Pemohon();
            }
        }

        private void Pemohon()
        {
            string nokp = (string)Session["Profile.ICNO"];

            string query = "SELECT FullName, ICNO, Designation, Grade, Organization.Name, UserProfile.OfficePhoneNo, UserProfile.UserEmail, PlacementStatus, Organization.GroupId,UserProfile.HomePhoneNo, UserProfile.UserId, UserCredential.UserName FROM UserProfile inner join UserCredential on UserProfile.UserId=UserCredential.UserId inner join Organization on Organization.OrganizationId=UserProfile.OrganizationId   WHERE UserProfile.ICNO='" + nokp + "' and blocked='0'";

            DataTable dataTable = GetDataTable(query, "NRE_ProfileConnectionString");

            if (dataTable.Rows.Count != 0)
            {
                txticno.Text = dataTable.Rows[0][1].ToString().Trim();

            }
        }

        private DataTable GetDataTable(string query, string connectionName = "DefaultConnection")
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();

                return dataTable;
            }
        }

        private void ddlJenis_Perolehan()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM JenisPerolehan", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddlJenisPerolehan.DataSource = ds;
            ddlJenisPerolehan.DataTextField = "Nama";
            ddlJenisPerolehan.DataValueField = "Id";
            ddlJenisPerolehan.DataBind();
            ddlJenisPerolehan.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Sila Pilih--", ""));
        }

        protected void ddlJenisPerolehan_SelectedIndexChanged(object sender, EventArgs e)
        {
          txtJenisPerolehan.Text = string.Empty;

            if (ddlJenisPerolehan.SelectedValue == "99")
            {  
    
                PnlJenisPerolehan.Visible = true;
            }

            else
            {
                PnlJenisPerolehan.Visible = false;
            }

        }

        protected void Bind_ddlJabatan()
        {

            this.ddlBahagian.Items.Clear();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                SqlDataReader reader = new SqlCommand("SELECT * FROM Jabatan", connection).ExecuteReader();
                this.ddlJabatan.DataSource = reader;
                this.ddlJabatan.Items.Clear();
                this.ddlJabatan.DataTextField = "Nama";
                this.ddlJabatan.DataValueField = "Organisasi_Grp_ID";
                this.ddlJabatan.DataBind();
                this.ddlJabatan.Items.Insert(0, new ListItem("-- Sila Pilih Kementerian/ Jabatan --", ""));
                this.ddlBahagian.Items.Insert(0, new ListItem("-- Sila Pilih Bahagian/ Unit --", ""));
            }

        }

        protected void ddlJabatan_SelectedIndexChanged(object sender, EventArgs e)
        {    
            this.ddlBahagian.Items.Clear();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT* FROM Bahagian WHERE Organisasi_Grp_ID='" + ddlJabatan.SelectedValue + "'", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    this.ddlBahagian.DataSource = reader;
                    this.ddlBahagian.DataTextField = "Nama";
                    this.ddlBahagian.DataValueField = "Id";
                    this.ddlBahagian.DataBind();
                }

                this.ddlBahagian.Items.Insert(0, new ListItem("-- Sila Pilih Bahagian/ Unit --", ""));

                if (this.ddlJabatan.SelectedValue == "1")
                {
                    this.pnlBahagian.Visible = true;
                }
                else if (this.ddlJabatan.SelectedValue != "1")
                {
                    this.pnlBahagian.Visible = false;

                }

                else if (this.ddlBahagian.SelectedValue == "")
                {
                   
                }

            }
        }

        protected void ddlBahagian_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            conn.Open();           
            DataTable dt = new DataTable();

            if (ddlBahagian.SelectedIndex == 0 && ddlJabatan.SelectedIndex == 1)
            {

                //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#daftarform').modal('hide')", false);
            }

            else if (ddlJabatan.SelectedIndex == 1 && ddlBahagian.SelectedIndex == 0)
            {

                //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#daftarform').modal('hide')", false);
            }

        }

        protected void btnHantar_Click(object sender, EventArgs e)
        {

            DateTime tkhsahlaku = SystemHelper.GetDateTime(txttkhsahlaku.Text);

            MessageAlert.Visible = false;
            MessageAlertbhg.Visible = false;

            if ((ddlBahagian.SelectedValue == "" && ddlJabatan.SelectedValue == "1") || (ddlJabatan.SelectedValue == ""))
            {

                MessageAlertbhg.Visible = true;
            }

            else
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                sqlConnection.Open();

                txttkhcipta.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                //string query = "INSERT INTO SyarikatBerjayaSebelum2023 (Tajuk,IdJenisPerolehan,LainJenisPerolehan,IdJabatan,IdBahagian,Harga,TarikhSahlaku,CatatanPendaftar,TarikhDicipta,DiciptaOleh,NamaBahagian) values (@Tajuk,@IdJenisPerolehan,@LainJenisPerolehan,@IdJabatan,@IdBahagian,@Harga,@TarikhSahlaku,@CatatanPendaftar,@TarikhDicipta,@DiciptaOleh,@NamaBahagian)";
                string query = "INSERT INTO SyarikatBerjayaSebelum2023 (Tajuk,IdJenisPerolehan,LainJenisPerolehan,IdJabatan,IdBahagian,Harga,TahunLantikan,CatatanPendaftar,NamaBahagian,Tempoh,NamaSyarikat,TarikhDicipta,DiciptaOleh) values (@Tajuk,@IdJenisPerolehan,@LainJenisPerolehan,@IdJabatan,@IdBahagian,@Harga,@TahunLantikan,@CatatanPendaftar,@NamaBahagian,@Tempoh,@NamaSyarikat,@TarikhDicipta,@DiciptaOleh)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Tajuk", txt_tajuk.Text);
                sqlCommand.Parameters.AddWithValue("@IdJenisPerolehan", ddlJenisPerolehan.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@LainJenisPerolehan", txtJenisPerolehan.Text);
                sqlCommand.Parameters.AddWithValue("@IdJabatan", ddlJabatan.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@IdBahagian", (string.IsNullOrEmpty(ddlBahagian.SelectedValue) ? (object)DBNull.Value : ddlBahagian.SelectedValue));
                sqlCommand.Parameters.AddWithValue("@Harga", txtharga.Text);
                sqlCommand.Parameters.AddWithValue("@TahunLantikan", tkhsahlaku);
                sqlCommand.Parameters.AddWithValue("@CatatanPendaftar", txtcatatan.Text);
                sqlCommand.Parameters.AddWithValue("@TarikhDicipta", txttkhcipta.Text);
                sqlCommand.Parameters.AddWithValue("@DiciptaOleh", txticno.Text);
                sqlCommand.Parameters.AddWithValue("@NamaBahagian", ddlBahagian.SelectedItem.ToString());
                sqlCommand.Parameters.AddWithValue("@Tempoh", txttempoh.Text);
                sqlCommand.Parameters.AddWithValue("@NamaSyarikat", txtNamaSyarikat.Text);

                sqlCommand.ExecuteNonQuery();
                Response.Redirect("/SyarikatBerjayaSblm2023/CarianSyarikat.aspx");
                sqlConnection.Close();
                
            }

        }





        //protected void btnHantar_Click(object sender, EventArgs e)
        //{

        //        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        //        sqlConnection.Open();

        //        txttkhcipta.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        //        //string query = "INSERT INTO SyarikatBerjayaSebelum2023 (Tajuk,IdJenisPerolehan,LainJenisPerolehan,IdJabatan,IdBahagian,Harga,TarikhSahlaku,CatatanPendaftar,TarikhDicipta,DiciptaOleh,NamaBahagian) values (@Tajuk,@IdJenisPerolehan,@LainJenisPerolehan,@IdJabatan,@IdBahagian,@Harga,@TarikhSahlaku,@CatatanPendaftar,@TarikhDicipta,@DiciptaOleh,@NamaBahagian)";
        //        string query = "INSERT INTO SyarikatBerjayaSebelum2023 (Tajuk) values (@Tajuk)";
        //        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

        //        sqlCommand.Parameters.AddWithValue("@Tajuk", txt_tajuk.Text);


        //        sqlCommand.ExecuteNonQuery();
        //        Response.Redirect("/Permohonan/senarai.aspx");
        //        sqlConnection.Close();



        //}


    }
}