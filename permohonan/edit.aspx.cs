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
    public partial class edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {   
                
                Papar();
                Bind_ddlJabatan();               
                ddl_JenisPertimbangan();
                ddlKaedah_Perolehan();
                ddlJenis_Perolehan();
                ddlSumber_Peruntukan();
                ddlPBM_Muktamad();              
                Pemohon();

            }
        }

        protected void Papar()
        {

            string Id = Request.QueryString["Id"];
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            string strQuery = "SELECT * FROM Papar_Permohonan where Id ='" + Id + "'";

            using (SqlCommand cmd = new SqlCommand(strQuery))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                cmd.Connection = conn; 
                conn.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                conn.Close();

             
                String IdJenisPertimbangan = String.Empty;
                String IdSumberPeruntukan = String.Empty;
                String IdJenisPerolehan = String.Empty;
                String Organisasi_Grp_ID = String.Empty;
                String LulusPelanPPT = String.Empty;
                String IdStatusPermohonan=String.Empty;

                //pnlCatatanPengesah.Visible = false;

                int i;

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    IdJenisPertimbangan = dt.Rows[i]["IdJenisPertimbangan"].ToString();
                    IdSumberPeruntukan = dt.Rows[i]["IdSumberPeruntukan"].ToString();
                    IdJenisPerolehan = dt.Rows[i]["IdJenisPerolehan"].ToString();
                    Organisasi_Grp_ID = dt.Rows[i]["Organisasi_Grp_ID"].ToString();
                    LulusPelanPPT = dt.Rows[i]["LulusPelanPPT"].ToString();
                    IdStatusPermohonan = dt.Rows[i]["IdStatusPermohonan"].ToString();

                    Bind_ddlBahagian(dt.Rows[0]["Organisasi_Grp_ID"].ToString());

                    txt_tajuk.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Tajuk"].ToString()) ? String.Empty : (dt.Rows[0]["Tajuk"]).ToString();                    
                   
                    ddlJenisPertimbangan.SelectedValue= String.IsNullOrWhiteSpace(dt.Rows[0]["IdJenisPertimbangan"].ToString()) ? String.Empty : (dt.Rows[0]["IdJenisPertimbangan"]).ToString();
                    txtJenisPertimbangan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["LainJenisPertimbangan"].ToString()) ? String.Empty : (dt.Rows[0]["LainJenisPertimbangan"]).ToString();

                    ddlSumberPeruntukan.SelectedValue = String.IsNullOrWhiteSpace(dt.Rows[0]["IdSumberPeruntukan"].ToString()) ? String.Empty : (dt.Rows[0]["IdSumberPeruntukan"]).ToString();
                    txtSumberPeruntukan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["LainSumberPeruntukan"].ToString()) ? String.Empty : (dt.Rows[0]["LainSumberPeruntukan"]).ToString();

                    ddlKaedahPerolehan.SelectedValue = String.IsNullOrWhiteSpace(dt.Rows[0]["IdKaedahPerolehan"].ToString()) ? String.Empty : (dt.Rows[0]["IdKaedahPerolehan"]).ToString();

                    ddlJenisPerolehan.SelectedValue = String.IsNullOrWhiteSpace(dt.Rows[0]["IdJenisPerolehan"].ToString()) ? String.Empty : (dt.Rows[0]["IdJenisPerolehan"]).ToString();
                    txtJenisPerolehan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["LainJenisPerolehan"].ToString()) ? String.Empty : (dt.Rows[0]["LainJenisPerolehan"]).ToString();

                    ddlJabatan.SelectedValue = String.IsNullOrWhiteSpace(dt.Rows[0]["Organisasi_Grp_ID"].ToString()) ? String.Empty : (dt.Rows[0]["Organisasi_Grp_ID"]).ToString();
                    ddlBahagian.SelectedValue = String.IsNullOrWhiteSpace(dt.Rows[0]["IdBahagian"].ToString()) ? String.Empty : (dt.Rows[0]["IdBahagian"]).ToString();

                    //txtharga.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Harga"].ToString()) ? String.Empty : ((Decimal)dt.Rows[0]["Harga"]).ToString();
                    txtharga.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Harga"].ToString()) ? String.Empty : ((Decimal)dt.Rows[0]["Harga"]).ToString("##.##");

                    ddlPBMMuktamad.SelectedValue = String.IsNullOrWhiteSpace(dt.Rows[0]["IdPBMMuktamad"].ToString()) ? String.Empty : (dt.Rows[0]["IdPBMMuktamad"]).ToString();
                    
                    txttkhsahlaku.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["TarikhSahlaku"].ToString()) ? String.Empty : ((DateTime)dt.Rows[0]["TarikhSahlaku"]).ToString("yyyy-MM-dd");
                    txttkhterima.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["TarikhTerima"].ToString()) ? String.Empty : ((DateTime)dt.Rows[0]["TarikhTerima"]).ToString("yyyy-MM-dd");
                    txtcatatan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["CatatanPendaftar"].ToString()) ? String.Empty : (dt.Rows[0]["CatatanPendaftar"]).ToString();
                    txtCatatanPengesah.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["CatatanPengesahan"].ToString()) ? String.Empty : (dt.Rows[0]["CatatanPengesahan"]).ToString();
                  

                    if (IdJenisPertimbangan == "99")
                    {
                        PnlJenisPertimbangan.Visible = true;
                    }

                    if (IdSumberPeruntukan == "99")
                    {
                        PnlSumberPeruntukan.Visible = true;
                    }

                    if (IdJenisPerolehan == "99")
                    {
                        PnlJenisPerolehan.Visible = true;
                    }

                    if (Organisasi_Grp_ID == "1")

                    {
                        pnlBahagian.Visible = true;

                    }

                    if(LulusPelanPPT=="YA")
                    {
                        
                        cbPerakuan1.Checked = true;

                    }

                    if (IdStatusPermohonan == "2")
                    {

                        pnlCatatanPengesah.Visible = true;

                    }

                }
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
                txtJenisPertimbangan.Text = string.Empty;
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
                txtJenisPerolehan.Text = string.Empty;
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
                txtSumberPeruntukan.Text = string.Empty;
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

            //this.ddlBahagian.Items.Clear();
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
            //this.ddlBahagian.Items.Clear();
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

        protected void Bind_ddlBahagian(string id)
        {
            //this.ddlBahagian.Items.Clear();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT* FROM Bahagian WHERE Organisasi_Grp_ID='" + id + "'", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    this.ddlBahagian.DataSource = reader;
                    this.ddlBahagian.DataTextField = "Nama";
                    this.ddlBahagian.DataValueField = "Id";
                    this.ddlBahagian.DataBind();
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
            try
            {

                MessageAlert.Visible = false;
                MessageAlertbhg.Visible = false;

                if ((ddlBahagian.SelectedValue == "0" && ddlJabatan.SelectedValue == "1")|| (ddlJabatan.SelectedValue == "0"))
                {

                    MessageAlertbhg.Visible = true;
                }

                else if (!cbPerakuan1.Checked)
                {
                    MessageAlert.Visible = true;
                }

                else { 

                InsertRecord();
                   
                }
            }

            catch (Exception ex)
            {
                //throw new Exception("Error: " + ex);
            }
        }

        protected void InsertRecord()
        {
            DateTime tkhterima = SystemHelper.GetDateTime(txttkhterima.Text);
            DateTime tkhsahlaku = SystemHelper.GetDateTime(txttkhsahlaku.Text);


            try

            {

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Permohonan WHERE ID ='" + Request.QueryString["Id"] + "'", conn);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    string insertQuery = "UPDATE Permohonan SET Tajuk = '" + txt_tajuk.Text.ToString() + "',IdJenisPertimbangan = '" + ddlJenisPertimbangan.SelectedValue + "',LainJenisPertimbangan = '" + txtJenisPertimbangan.Text.ToString() + "',IdKaedahPerolehan = '" + ddlKaedahPerolehan.SelectedValue + "',IdJenisPerolehan = '" + ddlJenisPerolehan.SelectedValue + "',LainJenisPerolehan = '" + txtJenisPerolehan.Text.ToString() + "',IdJabatan = '" + ddlJabatan.SelectedValue + "',IdBahagian = '" + ddlBahagian.SelectedValue + "',Harga = '" + txtharga.Text.ToString() + "',IdSumberPeruntukan = '" + ddlSumberPeruntukan.SelectedValue + "',LainSumberPeruntukan = '" + txtSumberPeruntukan.Text.ToString() + "',IdPBMMuktamad = '" + ddlPBMMuktamad.SelectedValue + "',IdStatusPermohonan = '1',CatatanPendaftar = '" + txtcatatan.Text.ToString() + "',TarikhDicipta = '" + DateTime.Now.ToString("yyyy-MM-dd") + "',DiciptaOleh = '" + txticno.Text.ToString() + "',NamaBahagian = '" + ddlBahagian.SelectedItem.ToString() + "',  TarikhSahlaku = '" + tkhsahlaku.ToString("yyyy-MM-dd") + "', TarikhTerima = '" + tkhterima.ToString("yyyy-MM-dd") + "' where ID ='" + Request.QueryString["Id"] + "'";
                    SqlCommand com = new SqlCommand(insertQuery, conn);

                    com.ExecuteNonQuery();
                    
                    Response.Redirect("/Permohonan/senarai.aspx");

                    conn.Close();
            
                }
        }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
    }
}

        
    }
}