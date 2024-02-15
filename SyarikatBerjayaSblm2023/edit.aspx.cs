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
    public partial class edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {   
                
                Papar();
                Bind_ddlJabatan();               
 
                ddlJenis_Perolehan();
               
                Pemohon();

            }
        }

        protected void Papar()
        {

            string Id = Request.QueryString["Id"];
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            string strQuery = "SELECT * FROM PaparSyarikatBerjayaSebelum2023 where Id ='" + Id + "'";

            using (SqlCommand cmd = new SqlCommand(strQuery))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                cmd.Connection = conn; 
                conn.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                conn.Close();

             
             
                String IdJenisPerolehan = String.Empty;
                String Organisasi_Grp_ID = String.Empty;
               

                //pnlCatatanPengesah.Visible = false;

                int i;

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                  
                    IdJenisPerolehan = dt.Rows[i]["IdJenisPerolehan"].ToString();
                    Organisasi_Grp_ID = dt.Rows[i]["Organisasi_Grp_ID"].ToString();

                    Bind_ddlBahagian(dt.Rows[0]["Organisasi_Grp_ID"].ToString());
                    txt_tajuk.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Tajuk"].ToString()) ? String.Empty : (dt.Rows[0]["Tajuk"]).ToString();                    
                    ddlJenisPerolehan.SelectedValue = String.IsNullOrWhiteSpace(dt.Rows[0]["IdJenisPerolehan"].ToString()) ? String.Empty : (dt.Rows[0]["IdJenisPerolehan"]).ToString();
                    txtJenisPerolehan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["LainJenisPerolehan"].ToString()) ? String.Empty : (dt.Rows[0]["LainJenisPerolehan"]).ToString();
                    ddlJabatan.SelectedValue = String.IsNullOrWhiteSpace(dt.Rows[0]["Organisasi_Grp_ID"].ToString()) ? String.Empty : (dt.Rows[0]["Organisasi_Grp_ID"]).ToString();
                    ddlBahagian.SelectedValue = String.IsNullOrWhiteSpace(dt.Rows[0]["IdBahagian"].ToString()) ? String.Empty : (dt.Rows[0]["IdBahagian"]).ToString();
                    //txtharga.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Harga"].ToString()) ? String.Empty : ((Decimal)dt.Rows[0]["Harga"]).ToString();
                    txtharga.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Harga"].ToString()) ? String.Empty : ((Decimal)dt.Rows[0]["Harga"]).ToString("##.##");
                    txtahunlantikan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["TahunLantikan"].ToString()) ? String.Empty : ((DateTime)dt.Rows[0]["TahunLantikan"]).ToString("yyyy-MM-dd");
                    //txttkhterima.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["TarikhTerima"].ToString()) ? String.Empty : ((DateTime)dt.Rows[0]["TarikhTerima"]).ToString("yyyy-MM-dd");
                    txtcatatan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["CatatanPendaftar"].ToString()) ? String.Empty : (dt.Rows[0]["CatatanPendaftar"]).ToString();
                    txttempoh.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Tempoh"].ToString()) ? String.Empty : (dt.Rows[0]["Tempoh"]).ToString();
                    txtNamaSyarikat.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["NamaSyarikat"].ToString()) ? String.Empty : (dt.Rows[0]["NamaSyarikat"]).ToString();


                    if (IdJenisPerolehan == "99")
                    {
                        PnlJenisPerolehan.Visible = true;
                    }

                    if (Organisasi_Grp_ID == "1")

                    {
                        pnlBahagian.Visible = true;

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
                this.ddlJabatan.Items.Insert(0, new ListItem("-- Sila Pilih Kementerian/ Jabatan --", ""));
                this.ddlBahagian.Items.Insert(0, new ListItem("-- Sila Pilih Bahagian/ Unit --", ""));
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

                if ((ddlBahagian.SelectedValue == "" && ddlJabatan.SelectedValue == "1")|| (ddlJabatan.SelectedValue == ""))
                {

                    MessageAlertbhg.Visible = true;
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
       
            DateTime tkhtahunlantikan = SystemHelper.GetDateTime(txtahunlantikan.Text);


            try

            {

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT * FROM SyarikatBerjayaSebelum2023 WHERE ID ='" + Request.QueryString["Id"] + "'", conn);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    string insertQuery = "UPDATE SyarikatBerjayaSebelum2023 SET Tajuk = '" + txt_tajuk.Text.ToString() + "',IdJenisPerolehan = '" + ddlJenisPerolehan.SelectedValue + "',LainJenisPerolehan = '" + txtJenisPerolehan.Text.ToString() + "',IdJabatan = '" + ddlJabatan.SelectedValue + "',IdBahagian = " + (string.IsNullOrEmpty(ddlBahagian.SelectedValue) ? "NULL" : ddlBahagian.SelectedValue) + ",Harga = '" + txtharga.Text.ToString() + "',CatatanPendaftar = '" + txtcatatan.Text.ToString() + "',TarikhDicipta = '" + DateTime.Now.ToString("yyyy-MM-dd") + "',DiciptaOleh = '" + txticno.Text.ToString() + "',NamaBahagian = '" + ddlBahagian.SelectedItem.ToString() + "',  TahunLantikan = '" + tkhtahunlantikan.ToString("yyyy-MM-dd") + "', Tempoh='" + txttempoh.Text.ToString() + "', NamaSyarikat='" + txtNamaSyarikat.Text.ToString() + "' where ID ='" + Request.QueryString["Id"] + "'";
                    SqlCommand com = new SqlCommand(insertQuery, conn);

                    com.ExecuteNonQuery();
                    
                    Response.Redirect("/SyarikatBerjayaSblm2023/CarianSyarikat.aspx");

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