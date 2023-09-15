using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Internal;
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
                sendBtn.CommandArgument = dtMesyuarat.Rows[0]["Id"].ToString();
                int IdPengesahan = Convert.ToInt32(dtMesyuarat.Rows[0]["IdStatusPengesahan"]);
                ViewState["dtMesyuarat"] = dtMesyuarat;

                if (IdPengesahan == 3)
                {
                    CatatanPengesahan.Text = dtMesyuarat.Rows[0]["CatatanPengesahan"].ToString().Replace(System.Environment.NewLine, "<br>");
                    PanelComment.Visible = true;
                }
                if(IdPengesahan == 1 || IdPengesahan == 3)
                {
                    PanelSendApprove.Visible = true;
                    int JumlahMohon = Convert.ToInt32(dtMesyuarat.Rows[0]["JumlahPermohonan"]);
                    int JumlahLulus = Convert.ToInt32(dtMesyuarat.Rows[0]["JumlahKelulusan"]);
                    if (JumlahMohon < 1 || JumlahMohon != JumlahLulus)
                    {
                        confirmBtn.CssClass = confirmBtn.CssClass + " disabled";
                        //confirmBtn.Attributes["title"] = "Sila lengkapkan maklumat keputusan bagi setiap permohonan";
                        confirmBtn.Enabled = false;
                        confirmBtn.Attributes.Remove("href");
                        confirmBtn.OnClientClick = null;
                    }
                }

                string CommandText2 = "Select Id, Tajuk, CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, IdStatusPengesahan, IdStatusKeputusan, StatusKeputusan as STATUS, SyarikatBerjaya, Harga, Tempoh, AlasanKeputusan as KETERANGAN from Papar_Permohonan WHERE IdMesyuarat=@Id and TarikhHapus IS NULL ORDER BY Id";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);

                GridView1.DataSource = dtPermohonan;
                GridView1.DataBind();
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
                HyperLink viewButton = e.Row.FindControl("viewButton") as HyperLink;
                HyperLink editButton = e.Row.FindControl("editButton") as HyperLink;

                viewButton.NavigateUrl = "/keputusan/papar.aspx?id=" + drv.Row["Id"];
                editButton.NavigateUrl = "/keputusan/edit.aspx?id=" + drv.Row["Id"]; 
                
                if (Convert.ToInt32(drv.Row["IdStatusPengesahan"]) == 4)
                {
                    editButton.Visible = false;
                }

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

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            DataTable dtrslt = ((DataTable)ViewState["dtMesyuarat"]);
            int id = Convert.ToInt32(dtrslt.Rows[0]["Id"]);
            int idStatus = Convert.ToInt32(dtrslt.Rows[0]["IdStatusPengesahan"]);
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>(){ {"@Id",  id } };

            if (idStatus == 1 || idStatus == 3)
            {
                Utils.ExcuteQuery("UPDATE Mesyuarat SET IdStatusPengesahan = 2 WHERE Id = @Id", queryParams);
                //Session["flash.success"] = "Mesyuarat telah dihantar untuk kelulusan penyelia!";
                //Response.Redirect("/mesyuarat/senarai.aspx");
                PanelComment.Visible = false;
                PanelSendApprove.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "notyf.success('Mesyuarat telah dihantar untuk kelulusan penyelia!');", true);
            }
        }
    }
}