using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.keputusan
{
    public partial class sst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(User.IsInRole("Administrator") || User.IsInRole("Urusetia") || User.IsInRole("Pengesah")))
            {
                Utils.HttpNotFound();
            }
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

            string CommandText2 = "Select Id, IdMesyuarat, IdStatusKeputusan, IdJenisPertimbangan, IdPBMMuktamad, MESYUARAT, Tajuk, KaedahPerolehan, Harga, TarikhSahlakuMS, TarikhTerimaMS, LulusPelanPPT, " +
                   "CASE WHEN IdJenisPertimbangan = 99 THEN LainJenisPertimbangan ELSE JenisPertimbangan END as JenisPertimbangan, " +
                   "CASE WHEN IdJenisPerolehan = 99 THEN LainJenisPerolehan ELSE Nama_JPerolehan END as JenisPerolehan, " +
                   "CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, " +
                   "CASE WHEN IdSumberPeruntukan = 99 THEN LainSumberPeruntukan ELSE SumberPeruntukan END as SumberPeruntukan, " +
                   "StatusKeputusan as STATUS, PBM, SyarikatBerjaya, Tempoh, TarikhSuratSetujuTerima, RujukanSuratSetujuTerima, LampiranKeputusan, AlasanKeputusan, NilaiTawaran, IdJenisPentadbiranKontrak " +
                   "from Papar_Permohonan WHERE Id=@Id and TarikhHapus IS NULL and IdStatusPengesahan = 4 AND IdStatusKeputusan = 1 AND IdJenisPertimbangan NOT IN (2, 99) AND IdPBMMuktamad = 1";
            Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
            DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);
            ViewState["dtPermohonan"] = dtPermohonan;

            if (dtPermohonan.Rows.Count < 1) Utils.HttpNotFound();

            TajukPerolehan.Text = dtPermohonan.Rows[0]["Tajuk"].ToString();
            TajukMesyuarat.Text = dtPermohonan.Rows[0]["MESYUARAT"].ToString();
            LtlStatus.Text = dtPermohonan.Rows[0]["STATUS"].ToString();
            LtlSyarikat.Text = dtPermohonan.Rows[0]["SyarikatBerjaya"].ToString();
            LtlTempoh.Text = dtPermohonan.Rows[0]["Tempoh"].ToString();
            LtlNilai.Text = dtPermohonan.Rows[0]["NilaiTawaran"].ToString();
            LtlPbmMuktamad.Text = dtPermohonan.Rows[0]["PBM"].ToString();
            txtTarikhSST.Text = !DBNull.Value.Equals(dtPermohonan.Rows[0]["TarikhSuratSetujuTerima"]) ? ((DateTime)dtPermohonan.Rows[0]["TarikhSuratSetujuTerima"]).ToString("yyyy-MM-dd") : "";
            txtRujukanSST.Text = dtPermohonan.Rows[0]["RujukanSuratSetujuTerima"].ToString();

            if (string.IsNullOrEmpty(dtPermohonan.Rows[0]["LampiranKeputusan"].ToString()))
                LinkLampiran.Visible = false;
            else
            {
                LinkLampiran.NavigateUrl = "/uploads/lampiran-keputusan/" + dtPermohonan.Rows[0]["LampiranKeputusan"];
                LinkLampiran.Text = LinkLampiran.Text + dtPermohonan.Rows[0]["LampiranKeputusan"];
                //LinkLampiran.Attributes.Add("download", dtPermohonan.Rows[0]["LampiranKeputusan"].ToString());
            }
            // }
            // catch (Exception) { Utils.HttpNotFound(); }
        }

        protected void Save(object sender, EventArgs e)
        {
            //LinkButton btn = (LinkButton)sender;
            string id = ((DataTable)ViewState["dtPermohonan"]).Rows[0]["Id"].ToString();
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  id },
                        {"@TarikhSST",  txtTarikhSST.Text },
                        {"@RujukanSST",  txtRujukanSST.Text },
                    };
            Utils.ExcuteQuery("UPDATE Permohonan SET TarikhSuratSetujuTerima=@TarikhSST, RujukanSuratSetujuTerima=@RujukanSST WHERE Id=@Id", queryParams);
            Session["flash.success"] = "Maklumat SST berjaya dikemaskini.";

            if (Request.QueryString["ReturnURL"] != null)
                Response.Redirect(Request.QueryString["ReturnURL"]);
            else
                Response.Redirect("~/keputusan/sst.aspx?id=" + id);
        }
    }
}