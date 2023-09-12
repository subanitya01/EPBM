using EPBM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.mesyuarat
{
    public partial class pengesahan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initMesyuaratList();
            }
        }

        protected void initMesyuaratList()
        {
            string CommandText = "Select * from PaparMesyuarat WHERE IdStatusPengesahan=1 and JumlahPermohonan > 0 ORDER BY Id";
            DataTable dtMesyuarat = Utils.GetDataTable(CommandText);
            bool selected = false;

            foreach (DataRow row in dtMesyuarat.Rows)
            {
                ListItem item = new ListItem(row["JENIS"].ToString() + " BIL. " + row["BILANGAN"].ToString(), row["Id"].ToString());
                //item.Value = row["Id"].ToString();
                if (!selected)
                {
                    item.Selected = true;
                    selected = true;
                }
                listMesyuarat.Items.Add(item);
            }

            if (dtMesyuarat.Rows.Count > 0)
            {
                BindData();
            }
        }
        protected void BindData()
        {
            /*try
            {*/

            string CommandText2 = "Select Id, Tajuk, CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, IdStatusKeputusan, StatusKeputusan as STATUS, SyarikatBerjaya, Harga, Tempoh, AlasanKeputusan as KETERANGAN from Papar_Permohonan WHERE IdMesyuarat=@Id and TarikhHapus IS NULL ORDER BY Id";
            Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", listMesyuarat.SelectedValue } };
            DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);
            GridView1.DataSource = dtPermohonan;
            GridView1.DataBind();
            ViewState["dtPermohonan"] = dtPermohonan;
            /*}
            catch (Exception) { Utils.HttpNotFound(); }*/
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                Label lblStatus = e.Row.FindControl("lblStatus") as Label;
                Label LblKeterangan = e.Row.FindControl("LblKeterangan") as Label;
                ListView detailsList = e.Row.FindControl("DetailsList") as ListView;

                if (drv.Row["IdStatusKeputusan"].ToString() == "1")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-info";
                else if (drv.Row["IdStatusKeputusan"].ToString() == "2")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-success";
                else
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-danger";

                if (drv.Row["IdStatusKeputusan"].ToString() == "3")
                {
                    detailsList.Visible = false;
                }
                else
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
        protected void RefreshPage(object sender, EventArgs e)
        {
            if (listMesyuarat.Items.Count > 0)
            {
                BindData();
            }
        }
    }
}