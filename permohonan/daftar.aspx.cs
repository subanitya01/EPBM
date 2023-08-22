using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;



namespace EPBM.permohonan
{
    public partial class daftar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddl_JenisPertimbangan();
                ddlKaedah_Perolehan();
                ddlJenis_Perolehan();
                ddlSumber_Peruntukan();
                ddlPBM_Muktamad();
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



        protected void ddl_JenisPertimbangan()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM JenisPertimbangan", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddlJenisPertimbangan.DataSource = ds;
            ddlJenisPertimbangan.DataTextField = "Nama";
            ddlJenisPertimbangan.DataValueField = "Id";
            ddlJenisPertimbangan.DataBind();
            ddlJenisPertimbangan.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Sila Pilih--", ""));
        }

        protected void ddlJenisPertimbangann_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (ddlJenisPertimbangan.SelectedValue == "99")
            {
            
                PnlJenisPertimbangan.Visible = true;
            }

            else
            {
                PnlJenisPertimbangan.Visible = false;
            }
          
        }


        private void ddlKaedah_Perolehan()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KaedahPerolehan", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddlKaedahPerolehan.DataSource = ds;
            ddlKaedahPerolehan.DataTextField = "Nama";
            ddlKaedahPerolehan.DataValueField = "Id";
            ddlKaedahPerolehan.DataBind();
            ddlKaedahPerolehan.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Sila Pilih--", ""));
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


            if (ddlJenisPerolehan.SelectedValue == "99")
            {
    
                PnlJenisPerolehan.Visible = true;
            }

            else
            {
                PnlJenisPerolehan.Visible = false;
            }

        }

        private void ddlSumber_Peruntukan()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM SumberPeruntukan", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddlSumberPeruntukan.DataSource = ds;
            ddlSumberPeruntukan.DataTextField = "Nama";
            ddlSumberPeruntukan.DataValueField = "Id";
            ddlSumberPeruntukan.DataBind();
            ddlSumberPeruntukan.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Sila Pilih--", ""));
        }

        protected void ddlSumberPeruntukan_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ddlSumberPeruntukan.SelectedValue == "99")
            {
                
                PnlSumberPeruntukan.Visible = true;
            }

            else
            {
                PnlSumberPeruntukan.Visible = false;
            }

        }


        private void ddlPBM_Muktamad()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PBMMuktamad", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddlPBMMuktamad.DataSource = ds;
            ddlPBMMuktamad.DataTextField = "Nama";
            ddlPBMMuktamad.DataValueField = "Id";
            ddlPBMMuktamad.DataBind();
            ddlPBMMuktamad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Sila Pilih--", ""));
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
                this.ddlJabatan.Items.Insert(0, new ListItem("-- Sila Pilih Kementerian/ Jabatan --", "0"));
                this.ddlBahagian.Items.Insert(0, new ListItem("-- Sila Pilih Bahagian/ Unit --", "0"));
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

                this.ddlBahagian.Items.Insert(0, new ListItem("-- Sila Pilih Bahagian/ Unit --", "0"));

                if (this.ddlJabatan.SelectedValue == "1")
                {
                    this.pnlBahagian.Visible = true;
                }
                else if (this.ddlJabatan.SelectedValue != "1")
                {
                    this.pnlBahagian.Visible = false;

                }

                else if (this.ddlBahagian.SelectedValue == "0")
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

            DateTime tkhterima = SystemHelper.GetDateTime(txttkhterima.Text);
            DateTime tkhsahlaku = SystemHelper.GetDateTime(txttkhsahlaku.Text);

            MessageAlert.Visible = false;

           if (!cbPerakuan1.Checked)
            {
                MessageAlert.Visible = true;
            }
            else
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                sqlConnection.Open();

                txttkhcipta.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");

                string query = "INSERT INTO Permohonan (Tajuk,IdJenisPertimbangan,LainJenisPertimbangan,IdKaedahPerolehan,IdJenisPerolehan,LainJenisPerolehan,IdJabatan,IdBahagian,Harga,IdSumberPeruntukan,LainSumberPeruntukan,TarikhSahlaku,TarikhTerima,LulusPelanPPT,IdPBMMuktamad,IdStatusPermohonan,CatatanPendaftar,TarikhDicipta,DiciptaOleh,NamaBahagian) values (@Tajuk,@IdJenisPertimbangan,@LainJenisPertimbangan,@IdKaedahPerolehan,@IdJenisPerolehan,@LainJenisPerolehan,@IdJabatan,@IdBahagian,@Harga,@IdSumberPeruntukan,@LainSumberPeruntukan,@TarikhSahlaku,@TarikhTerima,@LulusPelanPPT,@IdPBMMuktamad,'1',@CatatanPendaftar,@TarikhDicipta,@DiciptaOleh,@NamaBahagian)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Tajuk", txt_tajuk.Text);
                sqlCommand.Parameters.AddWithValue("@IdJenisPertimbangan", ddlJenisPertimbangan.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@LainJenisPertimbangan", txtJenisPertimbangan.Text);
                sqlCommand.Parameters.AddWithValue("@IdKaedahPerolehan", ddlKaedahPerolehan.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@IdJenisPerolehan", ddlJenisPerolehan.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@LainJenisPerolehan", txtJenisPerolehan.Text);
                sqlCommand.Parameters.AddWithValue("@IdJabatan", ddlJabatan.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@IdBahagian", ddlBahagian.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@Harga", txtharga.Text);
                sqlCommand.Parameters.AddWithValue("@IdSumberPeruntukan", ddlSumberPeruntukan.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@LainSumberPeruntukan", txtSumberPeruntukan.Text);
                sqlCommand.Parameters.AddWithValue("@TarikhSahlaku", tkhsahlaku);
                sqlCommand.Parameters.AddWithValue("@TarikhTerima", tkhterima);
                sqlCommand.Parameters.AddWithValue("@LulusPelanPPT", cbPerakuan1.Checked ? "YA" : "TIDAK");
                sqlCommand.Parameters.AddWithValue("@IdPBMMuktamad", ddlPBMMuktamad.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@CatatanPendaftar", txtcatatan.Text);
                sqlCommand.Parameters.AddWithValue("@TarikhDicipta", txttkhcipta.Text);
                sqlCommand.Parameters.AddWithValue("@DiciptaOleh", txticno.Text);
                sqlCommand.Parameters.AddWithValue("@NamaBahagian", ddlBahagian.SelectedItem.ToString());

                sqlCommand.ExecuteNonQuery();
                Response.Redirect("/Permohonan/senarai.aspx");
                sqlConnection.Close();

            }

        }

       
       


    }
}