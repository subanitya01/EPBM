
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.SyarikatBerjayaSblm2023
{
    public partial class papar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Papar();          
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



                int i;

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                 
                    IdJenisPerolehan = dt.Rows[i]["IdJenisPerolehan"].ToString();
                    Organisasi_Grp_ID = dt.Rows[i]["Organisasi_Grp_ID"].ToString();

                 
                    lblTajuk.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Tajuk"].ToString()) ? String.Empty : (dt.Rows[0]["Tajuk"]).ToString();

                    lblLainLainJPerolehan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["LainJenisPerolehan"].ToString()) ? String.Empty : (dt.Rows[0]["LainJenisPerolehan"]).ToString();                
                    lblJenisPerolehan_Kontrak.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Nama"].ToString()) ? String.Empty : (dt.Rows[0]["Nama"]).ToString();
                    lblJabatan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["NamaJabatan"].ToString()) ? String.Empty : (dt.Rows[0]["NamaJabatan"]).ToString();
                    lblHargaIndikatif.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Harga"].ToString()) ? String.Empty : ((Decimal)dt.Rows[0]["Harga"]).ToString("n2");
               
                    lblTahunLantiakan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["TahunLantikan"].ToString()) ? String.Empty : ((DateTime)dt.Rows[0]["TahunLantikan"]).ToString("dd-MMM-yyyy");
                    lblTempoh.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Tempoh"].ToString()) ? String.Empty : (dt.Rows[0]["Tempoh"]).ToString();
                    lblNamaSyarikat.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["Tempoh"].ToString()) ? String.Empty : (dt.Rows[0]["Tempoh"]).ToString();
                    lblCatatan.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["CatatanPendaftar"].ToString()) ? String.Empty : (dt.Rows[0]["CatatanPendaftar"]).ToString();
                    lblBahagian.Text = String.IsNullOrWhiteSpace(dt.Rows[0]["NamaBahagian"].ToString()) ? String.Empty : (dt.Rows[0]["NamaBahagian"]).ToString();


                   
                    if (IdJenisPerolehan == "99")
                    {
                        lblLainLainJPerolehan.Visible = true;
                    }
                    
                    if (Organisasi_Grp_ID =="1")

                    {
                        PnlBahagian.Visible = true;

                    }

                }
            }
        }
    }
}