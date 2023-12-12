using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.keputusan
{
    public partial class mof : System.Web.UI.Page
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

            string CommandText1 = "select SyarikatBerjaya, case when TarikhSuratSetujuTerima > DATEADD(year,-2,GETDATE()) then 1 else 0 end as more2year, count(*) as total from Papar_Permohonan WHERE TarikhHapus IS NULL AND IdStatusPengesahan = 4 group by SyarikatBerjaya, case when TarikhSuratSetujuTerima > DATEADD(year,-2,GETDATE()) then 1 else 0 end";
            DataTable dtSyarikat = Utils.GetDataTable(CommandText1);
            SortedList<string, bool> sortlistSyarikat = new SortedList<string, bool>();
            SortedList<string, string> listSyarikat = new SortedList<string, string>();

            foreach (DataRow row in dtSyarikat.Rows)
            {
                if (sortlistSyarikat.ContainsKey(row["SyarikatBerjaya"].ToString()))
                {
                    if (Convert.ToInt32(sortlistSyarikat[row["SyarikatBerjaya"].ToString()]) == 0)
                        sortlistSyarikat[row["SyarikatBerjaya"].ToString()] = true;
                }
                else sortlistSyarikat.Add(row["SyarikatBerjaya"].ToString(), (Convert.ToInt32(row["more2year"]) == 1 && Convert.ToInt32(row["total"]) >= 3));
            }
            foreach (KeyValuePair<string, bool> kvp in sortlistSyarikat)
            {
                if (kvp.Value) listSyarikat.Add(kvp.Key, "Syarikat ini telah dilantik melebihi 2 kali dalam 2 tahun");
                else listSyarikat.Add(kvp.Key, "");
            }
            companyList.Text = JsonConvert.SerializeObject(listSyarikat);

            string CommandText2 = "Select Id, IdMesyuarat, IdStatusKeputusan, IdJenisPertimbangan, IdPBMMuktamad, MESYUARAT, Tajuk, KaedahPerolehan, Harga, TarikhSahlakuMS, TarikhTerimaMS, LulusPelanPPT, MOFSyarikatDiperaku, MOFNilaiTawaran, MOFTempoh, " +
                   "CASE WHEN IdJenisPertimbangan = 99 THEN LainJenisPertimbangan ELSE JenisPertimbangan END as JenisPertimbangan, " +
                   "CASE WHEN IdJenisPerolehan = 99 THEN LainJenisPerolehan ELSE Nama_JPerolehan END as JenisPerolehan, " +
                   "CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, " +
                   "CASE WHEN IdSumberPeruntukan = 99 THEN LainSumberPeruntukan ELSE SumberPeruntukan END as SumberPeruntukan, " +
                   "StatusKeputusan as STATUS, PBM, SyarikatBerjaya, Tempoh, TarikhSuratSetujuTerima, RujukanSuratSetujuTerima, LampiranKeputusan, AlasanKeputusan, NilaiTawaran, IdJenisPentadbiranKontrak " +
                   "from Papar_Permohonan WHERE Id=@Id and TarikhHapus IS NULL and IdStatusPengesahan = 4 AND IdStatusKeputusan = 1 AND IdJenisPertimbangan NOT IN (2, 99) AND IdPBMMuktamad = 2";
            Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
            DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);
            ViewState["dtPermohonan"] = dtPermohonan;

            if (dtPermohonan.Rows.Count < 1) Utils.HttpNotFound();

            TajukPerolehan.Text = dtPermohonan.Rows[0]["Tajuk"].ToString();
            TajukMesyuarat.Text = dtPermohonan.Rows[0]["MESYUARAT"].ToString();
            LtlStatus.Text = dtPermohonan.Rows[0]["STATUS"].ToString();
            LtlSyarikat.Text = dtPermohonan.Rows[0]["MOFSyarikatDiperaku"].ToString();
            LtlTempoh.Text = dtPermohonan.Rows[0]["MOFTempoh"].ToString();
            LtlNilai.Text = string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["MOFNilaiTawaran"]);
            LtlPbmMuktamad.Text = dtPermohonan.Rows[0]["PBM"].ToString();
            txtSyarikat.Text = dtPermohonan.Rows[0]["SyarikatBerjaya"].ToString();
            txtTempoh.Text = dtPermohonan.Rows[0]["Tempoh"].ToString();
            txtNilaiTawaran.Text = string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["NilaiTawaran"]);

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
                        {"@syarikat",  txtSyarikat.Text },
                        {"@Tempoh",  txtTempoh.Text },
                        {"@NilaiTawaran",  txtNilaiTawaran.Text },
                    };
            Utils.ExcuteQuery("UPDATE Permohonan SET SyarikatBerjaya=@syarikat, Tempoh=@Tempoh, NilaiTawaran=@NilaiTawaran WHERE Id=@Id", queryParams);
            Session["flash.success"] = "Keputusan MOF berjaya dikemaskini.";

            if (Request.QueryString["ReturnURL"] != null)
                Response.Redirect(Request.QueryString["ReturnURL"]);
            else
                Response.Redirect("~/keputusan/mof.aspx?id=" + id);
        }
    }
}