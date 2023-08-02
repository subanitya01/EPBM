using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;



namespace EPBM.permohonan
{
    public partial class daftar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Jenis_Pertimbangan();
                Kaedah_Perolehan();
                Jenis_Perolehan();
                Sumber_Peruntukan();
                PBM_Muktamad();
                Jabatan();
                Pemohon();
            }
        }


        private void Pemohon()
        {
            string nokp = (string)Session["nokp"];

            string query = "SELECT FullName, ICNO, Designation, Grade, Organization.Name, UserProfile.OfficePhoneNo, UserProfile.UserEmail, PlacementStatus, Organization.GroupId,UserProfile.HomePhoneNo, UserProfile.UserId, UserCredential.UserName FROM UserProfile inner join UserCredential on UserProfile.UserId=UserCredential.UserId inner join Organization on Organization.OrganizationId=UserProfile.OrganizationId   WHERE UserProfile.ICNO='" + nokp + "' and blocked='0'";

            DataTable dataTable = GetDataTable(query, "NRE_ProfileConnectionString");

            if (dataTable.Rows.Count != 0)
            {
                txticno.Text = dataTable.Rows[0][1].ToString().Trim();

            }
        }



        private DataTable GetDataTable(string query, string connectionName = "ePBM_Conn")
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



        private void Jenis_Pertimbangan()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ePBM_Conn"].ConnectionString);
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

        private void Kaedah_Perolehan()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ePBM_Conn"].ConnectionString);
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

        private void Jenis_Perolehan()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ePBM_Conn"].ConnectionString);
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

        private void Sumber_Peruntukan()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ePBM_Conn"].ConnectionString);
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

        private void PBM_Muktamad()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ePBM_Conn"].ConnectionString);
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

        private void Jabatan()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ePBM_Conn"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Jabatan", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddlJabatan.DataSource = ds;
            ddlJabatan.DataTextField = "Nama";
            ddlJabatan.DataValueField = "Organisasi_Grp_ID";
            ddlJabatan.DataBind();
            ddlJabatan.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Sila Pilih--", ""));
        }

        protected void btnHantar_Click(object sender, EventArgs e)
        {

            DateTime tkhterima = SystemHelper.GetDateTime(txttkhterima.Text);
            DateTime tkhsahlaku = SystemHelper.GetDateTime(txttkhsahlaku.Text);



            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ePBM_Conn"].ConnectionString);
            sqlConnection.Open();

            txttkhcipta.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");

            string query = "INSERT INTO Permohonan (Tajuk,TarikhTerima,TarikhSahlaku,Harga,IdJabatan,IdJenisPerolehan,IdKaedahPerolehan,IdJenisPertimbangan,IdSumberPeruntukan,IdPBMMuktamad,LulusPelanPPT,CatatanPendaftar,TarikhDicipta,DiciptaOleh,IdStatusPermohonan) values (@Tajuk,@TarikhTerima,@TarikhSahlaku,@Harga,@IdJabatan,@IdJenisPerolehan,@IdKaedahPerolehan,@IdJenisPertimbangan,@IdSumberPeruntukan,@IdPBMMuktamad,@LulusPelanPPT,@CatatanPendaftar,@TarikhDicipta,@DiciptaOleh,'1')";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
             
            sqlCommand.Parameters.AddWithValue("@Tajuk", txt_tajuk.Text);          
            sqlCommand.Parameters.AddWithValue("@TarikhTerima", tkhterima);
            sqlCommand.Parameters.AddWithValue("@TarikhSahlaku", tkhsahlaku);     
            sqlCommand.Parameters.AddWithValue("@TarikhDicipta", txttkhcipta.Text);
            sqlCommand.Parameters.AddWithValue("@Harga", txtharga.Text);
            sqlCommand.Parameters.AddWithValue("@IdJabatan", ddlJabatan.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@IdJenisPerolehan", ddlJenisPerolehan.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@IdKaedahPerolehan", ddlKaedahPerolehan.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@IdJenisPertimbangan", ddlJenisPertimbangan.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@IdSumberPeruntukan", ddlSumberPeruntukan.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@IdPBMMuktamad", ddlPBMMuktamad.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@LulusPelanPPT", cbPerakuan1.Checked ? "True" : "False");
            sqlCommand.Parameters.AddWithValue("@CatatanPendaftar", txtcatatan.Text);
            sqlCommand.Parameters.AddWithValue("@DiciptaOleh", txticno.Text);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();


        }

        //private void txtharga_TextChanged(object sender, EventArgs e)
        //{

        //    double parsedValue;

        //    if (!double.TryParse(txtharga.Text, out parsedValue))
        //    {
        //        txtharga.Text = "";
        //    }
        //}

       


    }
}