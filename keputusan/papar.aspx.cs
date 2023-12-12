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
            //try
            //{
                var Id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(Id))
                    Utils.HttpNotFound();

                string CommandText2 = "Select Id, IdMesyuarat, IdStatusKeputusan, IdStatusPengesahan, IdJenisPertimbangan, IdPBMMuktamad, PBM, Tajuk, KaedahPerolehan, Harga, TarikhSahlakuMS, TarikhTerimaMS, LulusPelanPPT, MESYUARAT, " +
                    "CASE WHEN IdJenisPertimbangan = 99 THEN concat(JenisPertimbangan, ' - ', LainJenisPertimbangan) ELSE JenisPertimbangan END as JenisPertimbangan, NilaiTawaran, " +
                    "CASE WHEN IdJenisPerolehan = 99 THEN concat(Nama_JPerolehan, ' - ', LainJenisPerolehan) ELSE Nama_JPerolehan END as JenisPerolehan, " +
                    "CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, " +
                    "CASE WHEN IdSumberPeruntukan = 99 THEN concat(SumberPeruntukan, ' - ', LainSumberPeruntukan) ELSE SumberPeruntukan END as SumberPeruntukan, " +
                    "StatusKeputusan, SyarikatBerjaya, Tempoh, TarikhSuratSetujuTerimaMS, RujukanSuratSetujuTerima, LampiranKeputusan, AlasanKeputusan, MOFSyarikatDiperaku, MOFNilaiTawaran, MOFTempoh, JenisPentadbiranKontrak " +
                    "from Papar_Permohonan WHERE Id=@Id and TarikhHapus IS NULL and (IdStatusPermohonan IN (3,4))";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);

                if (dtPermohonan.Rows.Count < 1) Utils.HttpNotFound();

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
                LtlMesyuarat.Text = dtMesyuarat.Rows[0]["MESYUARAT"].ToString();
                LtlJenisPentadbiranKontrak.Text = dtPermohonan.Rows[0]["JenisPentadbiranKontrak"].ToString();

                if (dtPermohonan.Rows[0]["IdPBMMuktamad"].ToString() == "1")
                {
                    LtlTempoh.Text = dtPermohonan.Rows[0]["Tempoh"].ToString();

                    if (dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() == "2")
                    {
                        LtlJenisPentadbiranKontrak.Text = dtPermohonan.Rows[0]["JenisPentadbiranKontrak"].ToString();
                    }
                    if (dtPermohonan.Rows[0]["IdStatusKeputusan"].ToString() == "1" && dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() != "2" && dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() != "99")
                    {
                        LtlSyarikat.Text = dtPermohonan.Rows[0]["SyarikatBerjaya"].ToString();
                        LtlNilaiTawaran.Text = "RM " + string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["NilaiTawaran"]);
                        LtlTarikhSST.Text = dtPermohonan.Rows[0]["TarikhSuratSetujuTerimaMS"].ToString();
                        LtlRujukanSST.Text = dtPermohonan.Rows[0]["RujukanSuratSetujuTerima"].ToString();
                        LinkToEditSST.NavigateUrl = "~/keputusan/sst.aspx?id=" + dtPermohonan.Rows[0]["Id"];

                        if (dtPermohonan.Rows[0]["IdStatusPengesahan"].ToString() == "4")
                            PanelSST.Visible = true;
                    }
                }
                else if (dtPermohonan.Rows[0]["IdStatusKeputusan"].ToString() == "1")
                {
                    LtlLabelSyarikat.Text = "SYARIKAT DIPERAKU";
                    LtlSyarikat.Text = dtPermohonan.Rows[0]["MOFSyarikatDiperaku"].ToString();
                    LtlSyarikat2.Text = dtPermohonan.Rows[0]["SyarikatBerjaya"].ToString();
                    LtlTempoh.Text = dtPermohonan.Rows[0]["MOFTempoh"].ToString();
                    LtlTempoh2.Text = dtPermohonan.Rows[0]["Tempoh"].ToString();
                    LtlNilaiTawaran.Text = "RM " + string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["MOFNilaiTawaran"]);
                    LtlNilaiTawaran2.Text = "RM " + string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["NilaiTawaran"]);
                    LinkToEditMOF.NavigateUrl = "~/keputusan/mof.aspx?id=" + dtPermohonan.Rows[0]["Id"];

                    if (dtPermohonan.Rows[0]["IdStatusPengesahan"].ToString() == "4")
                        PanelMOF.Visible = true;
                }

                LtlPbmMuktamad.Text = dtPermohonan.Rows[0]["PBM"].ToString();
                LtlAlasan.Text = dtPermohonan.Rows[0]["AlasanKeputusan"].ToString().Replace(Environment.NewLine, "<br />");
                LtlIdStatus.Text = dtPermohonan.Rows[0]["IdStatusKeputusan"].ToString();
                LtlIdJenisPertimbangan.Text = dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString();
                string returnUrl = "";

                if (Request.QueryString["ReturnURL"] != null)
                {
                    returnUrl = "&ReturnURL=" + System.Web.HttpUtility.UrlEncode(Request.QueryString["ReturnURL"]);
                    /*if (Request.QueryString["ReturnURL"] == "/keputusan/senarai.aspx")
                        LinkToList.Text = LinkToList.Text + " Senarai Keputusan";
                    else
                        LinkToList.Text = LinkToList.Text + " Perakuan Mesyuarat";*/

                    //LinkToList.NavigateUrl = Request.QueryString["ReturnURL"];
                }
                else
                {
                    //LinkToList.Text = LinkToList.Text + " Senarai Keputusan Bagi Mesyuarat Sama";
                    //LinkToList.NavigateUrl = "/mesyuarat/senarai-keputusan.aspx?id=" + dtPermohonan.Rows[0]["IdMesyuarat"];
                }

                if (Convert.ToInt32(dtMesyuarat.Rows[0]["IdStatusPengesahan"]) == 4)
                    LinkToEdit.Visible = false;
                else
                    LinkToEdit.NavigateUrl = "/keputusan/edit.aspx?id=" + dtPermohonan.Rows[0]["Id"] + returnUrl;

                if (string.IsNullOrEmpty(dtPermohonan.Rows[0]["LampiranKeputusan"].ToString()))
                    LinkLampiran.Visible = false;
                else
                {
                    LinkLampiran.NavigateUrl = "/uploads/lampiran-keputusan/" + dtPermohonan.Rows[0]["LampiranKeputusan"];
                    LinkLampiran.Text = LinkLampiran.Text + dtPermohonan.Rows[0]["LampiranKeputusan"];
                    //LinkLampiran.Attributes.Add("download", dtPermohonan.Rows[0]["LampiranKeputusan"].ToString());
                }
            //}
            //catch (Exception) { Utils.HttpNotFound(); }
        }
    }
}