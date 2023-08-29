using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.mesyuarat
{
    public partial class senarai_keputusan : System.Web.UI.Page
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
            /*try
            {*/
                var Id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(Id))
                    Utils.HttpNotFound();

                string CommandText = "Select * from PaparMesyuarat WHERE Id=@Id";
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtMesyuarat = Utils.GetDataTable(CommandText, queryParams);
                TajukMesyuarat.Text = "MESYUARAT " + dtMesyuarat.Rows[0]["JENIS"] + " BIL. " + dtMesyuarat.Rows[0]["BILANGAN"];

                string CommandText2 = "Select Id, Tajuk, CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, StatusKeputusan as STATUS, SyarikatBerjaya, Harga, Tempoh, AlasanKeputusan as KETERANGAN from Papar_Permohonan WHERE IdMesyuarat=@Id";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);

                GridView1.DataSource = dtPermohonan;
                GridView1.DataBind();
                ViewState["dtPermohonan"] = dtPermohonan;
            /*}
            catch (Exception) { Utils.HttpNotFound(); }*/
        }
    }
}