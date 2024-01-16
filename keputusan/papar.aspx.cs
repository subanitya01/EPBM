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

                string CommandText2 = "Select *, " +
                    "CASE WHEN IdJenisPertimbangan = 99 THEN concat(JenisPertimbangan, ' - ', LainJenisPertimbangan) ELSE JenisPertimbangan END as JenisPertimbangan, " +
                    "CASE WHEN IdJenisPerolehan = 99 THEN concat(Nama_JPerolehan, ' - ', LainJenisPerolehan) ELSE Nama_JPerolehan END as JenisPerolehan, " +
                    "CASE WHEN IdSumberPeruntukan = 99 THEN concat(SumberPeruntukan, ' - ', LainSumberPeruntukan) ELSE SumberPeruntukan END as SumberPeruntukan " +
                    "from Papar_Permohonan WHERE Id=@Id and TarikhHapus IS NULL and (IdStatusPermohonan IN (3,4))";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);

                if (dtPermohonan.Rows.Count < 1) Utils.HttpNotFound();

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

                string CommandText = "Select * from PaparMesyuarat WHERE Id=@Id";
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@Id", dtPermohonan.Rows[0]["IdMesyuarat"] } };
                DataTable dtMesyuarat = Utils.GetDataTable(CommandText, queryParams);
                Tajuk.Text = dtPermohonan.Rows[0]["TAJUK"].ToString();// "MESYUARAT " + dtMesyuarat.Rows[0]["JENIS"] + " BIL. " + dtMesyuarat.Rows[0]["BILANGAN"];

                LtlTajuk.Text = dtPermohonan.Rows[0]["TAJUK"].ToString();
                LtlKaedahPerolehan.Text = dtPermohonan.Rows[0]["KaedahPerolehan"].ToString();
                LtlJenisPertimbangan.Text = dtPermohonan.Rows[0]["JenisPertimbangan"].ToString();
                LtlJenisPerolehan.Text = dtPermohonan.Rows[0]["JenisPerolehan"].ToString();
                LtlJabatan.Text = dtPermohonan.Rows[0]["BahagianJabatan"].ToString();
                LtlHarga.Text = string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["Harga"]);
                LtlSumberPeruntukan.Text = dtPermohonan.Rows[0]["SumberPeruntukan"].ToString();
                LtlTarikhSahlaku.Text = dtPermohonan.Rows[0]["TarikhSahlakuMS"].ToString();
                LtlTarikhTerima.Text = dtPermohonan.Rows[0]["TarikhTerimaMS"].ToString();
                LtlLulusPelan.Text = dtPermohonan.Rows[0]["LulusPelanPPT"].ToString();

                LtlMesyuarat.Text = dtMesyuarat.Rows[0]["MESYUARAT"].ToString();

                if (dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() == "2")
                    LtlStatusKementerian.Text = dtPermohonan.Rows[0]["StatusPentadbiranKontrakKementerian"].ToString();
                else if (dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() == "99")
                    LtlStatusKementerian.Text = dtPermohonan.Rows[0]["StatusLainKementerian"].ToString();
                else
                    LtlStatusKementerian.Text = dtPermohonan.Rows[0]["StatusPerlantikanKontraktorKementerian"].ToString();

                LtlPbmMuktamad.Text = dtPermohonan.Rows[0]["PBM"].ToString();
                LtlJPKKementerian.Text = dtPermohonan.Rows[0]["JenisPentadbiranKontrakKementerian"].ToString();
                LtlSyarikatKementerian.Text = dtPermohonan.Rows[0]["SyarikatBerjayaKementerian"].ToString();
                LtlTempohKementerian.Text = dtPermohonan.Rows[0]["TempohKementerian"].ToString();
                LtlNilaiTawaranKementerian.Text = "RM " + string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["NilaiTawaranKementerian"]);
                LtlCatatanKementerian.Text = dtPermohonan.Rows[0]["CatatanKementerian"].ToString().Replace(Environment.NewLine, "<br />");

                if (string.IsNullOrEmpty(dtPermohonan.Rows[0]["LampiranKementerian"].ToString()))
                    LinkLampiranKementerian.Visible = false;
                else
                {
                    LinkLampiranKementerian.NavigateUrl = "/uploads/lampiran-keputusan/" + dtPermohonan.Rows[0]["LampiranKementerian"];
                    LinkLampiranKementerian.Text = LinkLampiranKementerian.Text + dtPermohonan.Rows[0]["LampiranKementerian"];
                    //LinkLampiran.Attributes.Add("download", dtPermohonan.Rows[0]["LampiranKeputusan"].ToString());
                }

                if (dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() != "2" && dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() != "99")
                {
                    if (dtPermohonan.Rows[0]["IdPBMMuktamad"].ToString() == "2" && dtPermohonan.Rows[0]["IdStatusKeputusanMOF"].ToString() == "1" )
                    {
                        LtlTarikhSST.Text = dtPermohonan.Rows[0]["TarikhSSTMOF"].ToString();
                        LtlRujukanSST.Text = dtPermohonan.Rows[0]["RujukanSSTMOF"].ToString();
                        LinkToEditSST.NavigateUrl = "~/keputusan/sst.aspx?id=" + dtPermohonan.Rows[0]["Id"];

                        if (dtPermohonan.Rows[0]["IdStatusPengesahan"].ToString() == "4")
                            PanelSST.Visible = true;
                    }
                    else if (dtPermohonan.Rows[0]["IdPBMMuktamad"].ToString() == "1" && dtPermohonan.Rows[0]["IdStatusKeputusanKementerian"].ToString() == "1" )
                    {
                        LtlTarikhSST.Text = dtPermohonan.Rows[0]["TarikhSSTKementerian"].ToString();
                        LtlRujukanSST.Text = dtPermohonan.Rows[0]["RujukanSSTKementerian"].ToString();
                        LinkToEditSST.NavigateUrl = "~/keputusan/sst.aspx?id=" + dtPermohonan.Rows[0]["Id"] + returnUrl;

                        if (dtPermohonan.Rows[0]["IdStatusPengesahan"].ToString() == "4")
                            PanelSST.Visible = true;
                    }
                }
                if (dtPermohonan.Rows[0]["IdPBMMuktamad"].ToString() == "2")
                {
                    LtlLabelSyarikat.Text = "SYARIKAT DIPERAKU";

                    if (dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() == "2")
                        LtlStatusMOF.Text = dtPermohonan.Rows[0]["StatusPentadbiranKontrakMOF"].ToString();
                    else if (dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() == "99")
                        LtlStatusMOF.Text = dtPermohonan.Rows[0]["StatusLainMOF"].ToString();
                    else
                        LtlStatusMOF.Text = dtPermohonan.Rows[0]["StatusPerlantikanKontraktorMOF"].ToString();

                    LtlJPKMOF.Text = dtPermohonan.Rows[0]["JenisPentadbiranKontrakMOF"].ToString();
                    LtlSyarikatMOF.Text = dtPermohonan.Rows[0]["SyarikatBerjayaMOF"].ToString();
                    LtlTempohMOF.Text = dtPermohonan.Rows[0]["TempohMOF"].ToString();
                    LtlNilaiTawaranMOF.Text = "RM " + string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["NilaiTawaranMOF"]);
                    LtlCatatanMOF.Text = dtPermohonan.Rows[0]["CatatanMOF"].ToString().Replace(Environment.NewLine, "<br />");


                    if (string.IsNullOrEmpty(dtPermohonan.Rows[0]["LampiranMOF"].ToString()))
                        LinkLampiranMOF.Visible = false;
                    else
                    {
                        LinkLampiranMOF.NavigateUrl = "/uploads/lampiran-keputusan/" + dtPermohonan.Rows[0]["LampiranMOF"];
                        LinkLampiranMOF.Text = LinkLampiranMOF.Text + dtPermohonan.Rows[0]["LampiranMOF"];
                        //LinkLampiran.Attributes.Add("download", dtPermohonan.Rows[0]["LampiranKeputusan"].ToString());
                    }

                    if (dtPermohonan.Rows[0]["IdStatusPengesahan"].ToString() == "4")
                        PanelMOF.Visible = true;
                }

                LtlIdStatusKementerian.Text = !string.IsNullOrEmpty(dtPermohonan.Rows[0]["IdStatusKeputusanKementerian"].ToString()) ? dtPermohonan.Rows[0]["IdStatusKeputusanKementerian"].ToString() : "null";
                LtlIdStatusMOF.Text = !string.IsNullOrEmpty(dtPermohonan.Rows[0]["IdStatusKeputusanMOF"].ToString()) ? dtPermohonan.Rows[0]["IdStatusKeputusanMOF"].ToString() : "null";
                LtlIdJenisPertimbangan.Text = dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString();

                LinkToEditMOF.NavigateUrl = "~/keputusan/mof.aspx?id=" + dtPermohonan.Rows[0]["Id"] + returnUrl;

                if (Convert.ToInt32(dtMesyuarat.Rows[0]["IdStatusPengesahan"]) == 4)
                    LinkToEdit.Visible = false;
                else
                    LinkToEdit.NavigateUrl = "/keputusan/edit.aspx?id=" + dtPermohonan.Rows[0]["Id"] + "&muktamad=" + dtPermohonan.Rows[0]["IdPBMMuktamad"] + returnUrl;

            //}
            //catch (Exception) { Utils.HttpNotFound(); }
        }
    }
}