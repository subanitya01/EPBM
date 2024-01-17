using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.sst
{
    public partial class edit : System.Web.UI.Page
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

            string CommandText2 = "Select *, StatusSST as STATUS, " +
                    "CASE WHEN IdJenisPertimbangan = 99 THEN concat(JenisPertimbangan, ' - ', LainJenisPertimbangan) ELSE JenisPertimbangan END as JenisPertimbangan, " +
                    "CASE WHEN IdJenisPerolehan = 99 THEN concat(Nama_JPerolehan, ' - ', LainJenisPerolehan) ELSE Nama_JPerolehan END as JenisPerolehan, " +
                    "CASE WHEN IdSumberPeruntukan = 99 THEN concat(SumberPeruntukan, ' - ', LainSumberPeruntukan) ELSE SumberPeruntukan END as SumberPeruntukan " +
                   "from Papar_Permohonan WHERE Id=@Id and TarikhHapus IS NULL and IdStatusPengesahan = 4 AND IdStatusKeputusan = 1 AND IdJenisPertimbangan NOT IN (2, 99)";
            Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
            DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);
            ViewState["dtPermohonan"] = dtPermohonan;

            if (dtPermohonan.Rows.Count < 1) Utils.HttpNotFound();

            TajukPerolehan.Text = dtPermohonan.Rows[0]["Tajuk"].ToString();
            TajukMesyuarat.Text = dtPermohonan.Rows[0]["IdPBMMuktamad"].ToString()== "1" ? "MESYUARAT " + dtPermohonan.Rows[0]["MESYUARAT"].ToString() : "MOF";
            LtlStatus.Text = dtPermohonan.Rows[0]["STATUS"].ToString();
            LtlSyarikat.Text = dtPermohonan.Rows[0]["SyarikatBerjaya"].ToString();
            LtlTempoh.Text = dtPermohonan.Rows[0]["Tempoh"].ToString();
            LtlNilai.Text = dtPermohonan.Rows[0]["NilaiTawaran"].ToString();
            LtlPbmMuktamad.Text = dtPermohonan.Rows[0]["PBM"].ToString();
            txtTarikhSST.Text = !DBNull.Value.Equals(dtPermohonan.Rows[0]["TarikhSST"]) ? ((DateTime)dtPermohonan.Rows[0]["TarikhSST"]).ToString("yyyy-MM-dd") : "";
            txtRujukanSST.Text = dtPermohonan.Rows[0]["RujukanSST"].ToString();

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
            string idPBMMuktamad = ((DataTable)ViewState["dtPermohonan"]).Rows[0]["IdPBMMuktamad"].ToString();
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  id },
                    };
            if (idPBMMuktamad == "1")
            {
                string CommandText2 = "Select * from KeputusanKementerian WHERE IdPermohonan=@Id";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", id } };
                DataTable dtKeputusan = Utils.GetDataTable(CommandText2, queryParams2);

                if (dtKeputusan.Rows.Count < 1) Utils.HttpNotFound();

                Dictionary<string, dynamic> queryParamsSave = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  dtKeputusan.Rows[0]["Id"].ToString() },
                        {"@TarikhSST",  txtTarikhSST.Text },
                        {"@RujukanSST",  txtRujukanSST.Text },
                    };
                Utils.ExcuteQuery("UPDATE KeputusanPerlantikanKontraktor SET TarikhSuratSetujuTerima=@TarikhSST, RujukanSuratSetujuTerima=@RujukanSST WHERE IdKeputusanKementerian=@Id", queryParamsSave);

            }
            else
            {
                string CommandText2 = "Select * from KeputusanMOF WHERE IdPermohonan=@Id";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", id } };
                DataTable dtKeputusan = Utils.GetDataTable(CommandText2, queryParams2);

                if (dtKeputusan.Rows.Count < 1) Utils.HttpNotFound();

                Dictionary<string, dynamic> queryParamsSave = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  dtKeputusan.Rows[0]["Id"].ToString() },
                        {"@TarikhSST",  txtTarikhSST.Text },
                        {"@RujukanSST",  txtRujukanSST.Text },
                    };
                Utils.ExcuteQuery("UPDATE KeputusanPerlantikanKontraktor SET TarikhSuratSetujuTerima=@TarikhSST, RujukanSuratSetujuTerima=@RujukanSST WHERE IdKeputusanMOF=@Id", queryParamsSave);
            }

            Session["flash.success"] = "Maklumat SST berjaya dikemaskini.";

            if (Request.QueryString["ReturnURL"] != null)
                Response.Redirect(Request.QueryString["ReturnURL"]);
            else
                Response.Redirect("~/sst/edit.aspx?id=" + id);
        }
    }
}