using Newtonsoft.Json;
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
            try
            {
                var Id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(Id))
                    Utils.HttpNotFound();

                string CommandText = "Select * from PaparMesyuarat WHERE Id=@Id";
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtMesyuarat = Utils.GetDataTable(CommandText, queryParams);
                TajukMesyuaratModal.Text = TajukMesyuarat.Text = "MESYUARAT " + dtMesyuarat.Rows[0]["JENIS"] + " BIL. " + dtMesyuarat.Rows[0]["BILANGAN"];

                string CommandText2 = "Select Id, Tajuk, CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, IdStatusKeputusan, StatusKeputusan as STATUS, SyarikatBerjaya, Harga, Tempoh, AlasanKeputusan as KETERANGAN from Papar_Permohonan WHERE IdMesyuarat=@Id and TarikhHapus IS NULL ORDER BY Id";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);

                GridView1.DataSource = dtPermohonan;
                GridView1.DataBind();
                ViewState["dtPermohonan"] = dtPermohonan;
            }
            catch (Exception) { Utils.HttpNotFound(); }
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                Label lblStatus = e.Row.FindControl("lblStatus") as Label;
                Label LblKeterangan = e.Row.FindControl("LblKeterangan") as Label;
                ListView detailsList = e.Row.FindControl("DetailsList") as ListView;

                if (drv.Row["IdStatusKeputusan"].ToString()=="1")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-info";
                else if (drv.Row["IdStatusKeputusan"].ToString() == "2")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-success";
                else
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-danger";

                if (drv.Row["IdStatusKeputusan"].ToString() == "3")
                {
                    detailsList.Visible = false;
                }
                else if(!string.IsNullOrEmpty(drv.Row["IdStatusKeputusan"].ToString()))
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Label"), new DataColumn("Text") });
                    dt.Rows.Add("SYARIKAT BERJAYA", drv.Row["SyarikatBerjaya"].ToString());
                    dt.Rows.Add("NILAI", "RM " + string.Format("{0:#,0.00}", drv.Row["Harga"]));
                    dt.Rows.Add("TEMPOH", drv.Row["Tempoh"].ToString() + " BULAN");
                    detailsList.DataSource = dt;
                    detailsList.DataBind();
                    LblKeterangan.Visible = false;
                }
            }
        }
    }
}