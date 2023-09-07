using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace EPBM.permohonan
{
    public partial class papar_pengesahan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Papar();
              
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



                int i;

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    IdJenisPertimbangan = dt.Rows[i]["IdJenisPertimbangan"].ToString();
                    IdSumberPeruntukan = dt.Rows[i]["IdSumberPeruntukan"].ToString();
                    IdJenisPerolehan = dt.Rows[i]["IdJenisPerolehan"].ToString();
                    Organisasi_Grp_ID = dt.Rows[i]["Organisasi_Grp_ID"].ToString();

                    lblTajukUtama.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Tajuk"].ToString()) ? String.Empty : (dt.Rows[0]["Tajuk"]).ToString();
                    lblTajuk.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Tajuk"].ToString()) ? String.Empty : (dt.Rows[0]["Tajuk"]).ToString();
                    lblJPertimbangan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["JenisPertimbangan"].ToString()) ? String.Empty : (dt.Rows[0]["JenisPertimbangan"]).ToString();
                    lblLainLainJPertimbangan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["LainJenisPertimbangan"].ToString()) ? String.Empty : (dt.Rows[0]["LainJenisPertimbangan"]).ToString();
                    lblLainLainJPerolehan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["LainJenisPerolehan"].ToString()) ? String.Empty : (dt.Rows[0]["LainJenisPerolehan"]).ToString();
                    lblLainLainSPeruntukan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["LainSumberPeruntukan"].ToString()) ? String.Empty : (dt.Rows[0]["LainSumberPeruntukan"]).ToString();
                    lblKaedahPerolehan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["KaedahPerolehan"].ToString()) ? String.Empty : (dt.Rows[0]["KaedahPerolehan"]).ToString();
                    lblJenisPerolehan_Kontrak.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Nama_JPerolehan"].ToString()) ? String.Empty : (dt.Rows[0]["Nama_JPerolehan"]).ToString();
                    lblJabatan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["NamaJabatan"].ToString()) ? String.Empty : (dt.Rows[0]["NamaJabatan"]).ToString();
                    lblHargaIndikatif.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Harga"].ToString()) ? String.Empty : ((Decimal)dt.Rows[0]["Harga"]).ToString("n2");
                    lblSumberPeruntukan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["SumberPeruntukan"].ToString()) ? String.Empty : (dt.Rows[0]["SumberPeruntukan"]).ToString();
                    lblPBM_Muktamad.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["PBM"].ToString()) ? String.Empty : (dt.Rows[0]["PBM"]).ToString();
                    TarikhSahlaku.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["TarikhSahlaku"].ToString()) ? String.Empty : ((DateTime)dt.Rows[0]["TarikhSahlaku"]).ToString("dd-MMM-yyyy");
                    lblTarikhTerima.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["TarikhTerima"].ToString()) ? String.Empty : ((DateTime)dt.Rows[0]["TarikhTerima"]).ToString("dd-MMM-yyyy");
                    lblLulus_Pelan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["LulusPelanPPT"].ToString()) ? String.Empty : (dt.Rows[0]["LulusPelanPPT"]).ToString();
                    lblCatatan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["CatatanPendaftar"].ToString()) ? String.Empty : (dt.Rows[0]["CatatanPendaftar"]).ToString();
                    lblBahagian.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["NamaBahagian"].ToString()) ? String.Empty : (dt.Rows[0]["NamaBahagian"]).ToString();
                    txtcatatanpengesah.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["CatatanPengesahan"].ToString()) ? String.Empty : (dt.Rows[0]["CatatanPengesahan"]).ToString();

                    //txtcatatan

                    if (IdJenisPertimbangan == "99")
                    {
                        lblLainLainJPertimbangan.Visible = true;
                    }

                    if (IdSumberPeruntukan == "99")
                    {
                        lblLainLainSPeruntukan.Visible = true;
                    }

                    if (IdJenisPerolehan == "99")
                    {
                        lblLainLainJPerolehan.Visible = true;
                    }

                    if (Organisasi_Grp_ID == "1")

                    {
                        PnlBahagian.Visible = true;

                    }

                }
            }
        }
        protected void btnSah_Click(object sender, EventArgs e)
        {
            try
            {
                              
                    UpdateSah();
                                 
            
            }

            catch (Exception ex)
            {
              
            }

            //finally
            //{

            //}

        }


        protected void UpdateSah()
        {
           

            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Permohonan WHERE ID ='" + Request.QueryString["Id"] + "'", conn);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    string insertQuery = "UPDATE Permohonan SET IdStatusPermohonan = '3' where ID ='" + Request.QueryString["Id"] + "'";
                    SqlCommand com = new SqlCommand(insertQuery, conn);

                    com.ExecuteNonQuery();

                    Response.Redirect("/Permohonan/senarai-pengesahan.aspx");

                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        }



        protected void btnKembali_Click(object sender, EventArgs e)
        {

            try
            {

                if (txtcatatanpengesah.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openrejectModal();", true);
                    MessageAlert.Visible = true;
                }

                else
                {

                UpdateKembali();

                }
              
            }

            catch (Exception ex)
            {

            }

            //finally
            //{

            //}


        }

        protected void UpdateKembali()
        {


                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Permohonan WHERE ID ='" + Request.QueryString["Id"] + "'", conn);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    string insertQuery = "UPDATE Permohonan SET IdStatusPermohonan = '2',CatatanPengesahan='" + txtcatatanpengesah.Text.ToString() + "' where ID ='" + Request.QueryString["Id"] + "'";
                    SqlCommand com = new SqlCommand(insertQuery, conn);

                    com.ExecuteNonQuery();

                    Response.Redirect("/Permohonan/senarai-pengesahan.aspx");

                    conn.Close();

                }
            
            
        }


    }
}