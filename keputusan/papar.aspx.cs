using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.mesyuarat
{
    public partial class papar_keputusan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        protected void BindData()
        {
            try
            {
                var Id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(Id))
                    Utils.HttpNotFound();

                string CommandText2 = "Select Id, IdMesyuarat, IdStatusKeputusan, PBM, Tajuk, KaedahPerolehan, Harga, TarikhSahlakuMS, TarikhTerimaMS, LulusPelanPPT, " +
                    "CASE WHEN IdJenisPertimbangan = 99 THEN LainJenisPertimbangan ELSE JenisPertimbangan END as JenisPertimbangan, " +
                    "CASE WHEN IdJenisPerolehan = 99 THEN LainJenisPerolehan ELSE Nama_JPerolehan END as JenisPerolehan, " +
                    "CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, " +
                    "CASE WHEN IdSumberPeruntukan = 99 THEN LainSumberPeruntukan ELSE SumberPeruntukan END as SumberPeruntukan, " +
                    "StatusKeputusan, SyarikatBerjaya, Tempoh, TarikhSuratSetujuTerimaMS, RujukanSuratSetujuTerima, LampiranKeputusan, AlasanKeputusan " +
                    "from Papar_Permohonan WHERE Id=@Id and TarikhHapus IS NULL and (IdStatusPermohonan IN (3,4))";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);

                string CommandText = "Select * from PaparMesyuarat WHERE Id=@Id";
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@Id", dtPermohonan.Rows[0]["IdMesyuarat"] } };
                DataTable dtMesyuarat = Utils.GetDataTable(CommandText, queryParams);
                Tajuk.Text = dtPermohonan.Rows[0]["TAJUK"].ToString();// "MESYUARAT " + dtMesyuarat.Rows[0]["JENIS"] + " BIL. " + dtMesyuarat.Rows[0]["BILANGAN"];

                LtlTajuk.Text = dtPermohonan.Rows[0]["TAJUK"].ToString();
                LtlKaedahPerolehan.Text = dtPermohonan.Rows[0]["KaedahPerolehan"].ToString();
                LtlJenisPertimbangan.Text = dtPermohonan.Rows[0]["JenisPertimbangan"].ToString();
                LtlJenisPerolehan.Text = dtPermohonan.Rows[0]["JenisPerolehan"].ToString();
                LtlJabatan.Text = dtPermohonan.Rows[0]["Jabatan"].ToString();
                LtlHarga.Text = string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["Harga"]);
                LtlSumberPeruntukan.Text = dtPermohonan.Rows[0]["SumberPeruntukan"].ToString();
                LtlTarikhSahlaku.Text = dtPermohonan.Rows[0]["TarikhSahlakuMS"].ToString();
                LtlTarikhTerima.Text = dtPermohonan.Rows[0]["TarikhTerimaMS"].ToString();
                LtlLulusPelan.Text = dtPermohonan.Rows[0]["LulusPelanPPT"].ToString();

                LtlStatus.Text = dtPermohonan.Rows[0]["StatusKeputusan"].ToString();
                LtlMesyuarat.Text = dtMesyuarat.Rows[0]["JENIS"] + " BIL. " + dtMesyuarat.Rows[0]["BILANGAN"];
                LtlSyarikat.Text = dtPermohonan.Rows[0]["SyarikatBerjaya"].ToString();
                LtlTempoh.Text = dtPermohonan.Rows[0]["Tempoh"].ToString();
                LtlPbmMuktamad.Text = dtPermohonan.Rows[0]["PBM"].ToString();
                LtlTarikhSST.Text = dtPermohonan.Rows[0]["TarikhSuratSetujuTerimaMS"].ToString();
                LtlRujukanSST.Text = dtPermohonan.Rows[0]["RujukanSuratSetujuTerima"].ToString();
                LtlAlasan.Text = dtPermohonan.Rows[0]["AlasanKeputusan"].ToString().Replace(Environment.NewLine, "<br />");
                LtlIdStatus.Text = dtPermohonan.Rows[0]["IdStatusKeputusan"].ToString();
                string returnUrl = "";
                if (Request.QueryString["ReturnURL"] != null)
                {
                    returnUrl = "&ReturnURL=" + System.Web.HttpUtility.UrlEncode(Request.QueryString["ReturnURL"]);
                    if (Request.QueryString["ReturnURL"] == "/keputusan/senarai.aspx")
                        LinkToList.Text = LinkToList.Text + " Senarai Keputusan";
                    else
                        LinkToList.Text = LinkToList.Text + " Perakuan Mesyuarat";

                    LinkToList.NavigateUrl = Request.QueryString["ReturnURL"];
                }
                else
                {
                    LinkToList.Text = LinkToList.Text + " Senarai Keputusan Bagi Mesyuarat Sama";
                    LinkToList.NavigateUrl = "/mesyuarat/senarai-keputusan.aspx?id=" + dtPermohonan.Rows[0]["IdMesyuarat"];
                }


                LinkToEdit.NavigateUrl = "/keputusan/edit.aspx?id=" + dtPermohonan.Rows[0]["Id"] + returnUrl;

                if (string.IsNullOrEmpty(dtPermohonan.Rows[0]["LampiranKeputusan"].ToString()))
                    LinkLampiran.Visible = false;
                else
                {
                    LinkLampiran.NavigateUrl = "/uploads/lampiran-keputusan/" + dtPermohonan.Rows[0]["LampiranKeputusan"];
                    LinkLampiran.Text = LinkLampiran.Text + dtPermohonan.Rows[0]["LampiranKeputusan"];
                    //LinkLampiran.Attributes.Add("download", dtPermohonan.Rows[0]["LampiranKeputusan"].ToString());
                }
            }
            catch (Exception) { Utils.HttpNotFound(); }
        }
    }
}