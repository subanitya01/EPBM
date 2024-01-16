using EPBM.Models;
using EPBM.permohonan;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.mesyuarat
{
    public partial class keputusan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(User.IsInRole("Administrator") || User.IsInRole("Urusetia")))
            {
                Utils.HttpNotFound();
            }
            if (!IsPostBack)
            {
                initMesyuaratList();
            }
        }

        protected void initMesyuaratList()
        {
            string CommandText = "Select * from PaparMesyuarat WHERE IdStatusPengesahan<>2 and IdStatusPengesahan<>4 ORDER BY Id";
            DataTable dtMesyuarat = Utils.GetDataTable(CommandText);
            ViewState["dtMesyuarat"] = dtMesyuarat;
            //bool selected = false;
            int id = Convert.ToInt32(Request.QueryString["id"]);

            foreach (DataRow row in dtMesyuarat.Rows)
            {
                ListItem item = new ListItem(row["Mesyuarat"].ToString(), row["Id"].ToString());
                //item.Value = row["Id"].ToString();
                if (id == Convert.ToInt32(row["Id"]))
                {
                    item.Selected = true;
                    //selected = true;
                }
                listMesyuarat.Items.Add(item);
            }

            if (dtMesyuarat.Rows.Count > 0)
            {
                BindData();
                PanelFound.Visible = true;
                PanelNotFound.Visible = false;
            }
        }
        protected void BindData()
        {
            //try
            //{
                DataRow[] dtMesyuarat = ((DataTable)ViewState["dtMesyuarat"]).Select("Id = " + listMesyuarat.SelectedValue);
                TajukMesyuaratModal.Text = dtMesyuarat[0]["MESYUARAT"].ToString();
                TajukPermohonan.Text = "BAGI MESYUARAT " + TajukMesyuaratModal.Text;
                string CommandText2 = "Select *, PBM as MUKTAMAD, StatusKeputusan as STATUS, NamaPendekBahagianJabatan as JABATAN, " +
                                    "CASE WHEN IdPBMMuktamad = 1 THEN IdStatusKeputusanKementerian ELSE IdStatusKeputusanMOF END as IdStatusKeputusan, " +
                                    "CASE WHEN IdPBMMuktamad = 1 THEN CatatanKementerian ELSE CatatanMOF END as KETERANGAN " +
                                    "from Papar_Permohonan  WHERE IdStatusPengesahan<>2 and IdStatusPengesahan<>4 and IdMesyuarat=@Id and TarikhHapus IS NULL ORDER BY Id";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", listMesyuarat.SelectedValue } };
                DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);
                GridView1.DataSource = dtPermohonan;
                GridView1.DataBind();
                int IdPengesahan = Convert.ToInt32(dtMesyuarat[0]["IdStatusPengesahan"]);
                sendBtn.CommandArgument = listMesyuarat.SelectedValue;
                PanelInfo.Visible = false;
                PanelSendBtn.Visible = true;
                confirmBtn.CssClass = confirmBtn.CssClass.Replace(" disabled", "");
                confirmBtn.Enabled = true;
                if (string.IsNullOrEmpty((string)ViewState["hrefConfirmBtn"]))
                {
                    ViewState["hrefConfirmBtn"] = confirmBtn.Attributes["href"];
                }
                else
                    confirmBtn.Attributes["href"] = (string)ViewState["hrefConfirmBtn"];


                if (IdPengesahan == 3)
                {
                    CatatanPengesahan.Text = dtMesyuarat[0]["CatatanPengesahan"].ToString().Replace(System.Environment.NewLine, "<br>");
                    PanelComment.Visible = true;
                }
                if (IdPengesahan == 1 || IdPengesahan == 3)
                {
                    int JumlahMohon = Convert.ToInt32(dtMesyuarat[0]["JumlahPermohonan"]);
                    int JumlahLulus = Convert.ToInt32(dtMesyuarat[0]["JumlahKelulusan"]);
                    if (JumlahMohon < 1 || JumlahMohon != JumlahLulus)
                    {
                        confirmBtn.CssClass = confirmBtn.CssClass + " disabled";
                        //confirmBtn.Attributes["title"] = "Sila lengkapkan maklumat keputusan bagi setiap permohonan";
                        confirmBtn.Enabled = false;
                        confirmBtn.Attributes.Remove("href");
                        confirmBtn.OnClientClick = null;
                        Info.Text = JumlahLulus + " daripada " + JumlahMohon + " Permohonan telah diputuskan.";
                        PanelInfo.Visible = true;
                    }
                }
                else if (IdPengesahan == 2)
                {
                    Info.Text = "Mesyuarat ini belum disahkan oleh penyelia.";
                    PanelInfo.Visible = true;
                    PanelSendBtn.Visible = false;
                }
            //}
            //catch (Exception) { Utils.HttpNotFound(); }
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                Label lblStatus = e.Row.FindControl("lblStatus") as Label;
                Label LblKeterangan = e.Row.FindControl("LblKeterangan") as Label;
                ListView detailsList = e.Row.FindControl("DetailsList") as ListView;
                HyperLink lnkEdit = e.Row.FindControl("lnkEdit") as HyperLink;

                lnkEdit.NavigateUrl = "/keputusan/edit.aspx?id=" + drv.Row["Id"] + "&ReturnURL=" + System.Web.HttpUtility.UrlEncode("/mesyuarat/keputusan.aspx?id=" + drv.Row["IdMesyuarat"]);

                string IdStatusKeputusan = !string.IsNullOrEmpty(drv.Row["IdStatusKeputusanMOF"].ToString()) ? drv.Row["IdStatusKeputusanMOF"].ToString() : drv.Row["IdStatusKeputusanKementerian"].ToString();

                if (IdStatusKeputusan == "1")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-info";
                else if (IdStatusKeputusan == "2")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-success";
                else if (IdStatusKeputusan == "5")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-warning";
                else
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-danger";

                if (IdStatusKeputusan == "3" || IdStatusKeputusan == "5" || (IdStatusKeputusan == "1" && drv.Row["IdJenisPertimbangan"].ToString() == "99"))
                {

                    LblKeterangan.Text = !string.IsNullOrEmpty(drv.Row["IdStatusKeputusanMOF"].ToString()) ? drv.Row["CatatanMOF"].ToString() : drv.Row["CatatanKementerian"].ToString();
                    detailsList.Visible = false;
                }
                else if (IdStatusKeputusan == "1")
                {
                    LblKeterangan.Visible = false;
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Label"), new DataColumn("Text") });

                    if (drv.Row["IdJenisPertimbangan"].ToString() == "2")
                    {
                        string[] JPK = !string.IsNullOrEmpty(drv.Row["IdStatusKeputusanMOF"].ToString()) ? drv.Row["JenisPentadbiranKontrakMOF"].ToString().Split(',') : drv.Row["JenisPentadbiranKontrakKementerian"].ToString().Split(',');

                        if (JPK.Length > 0)
                        {
                            LblKeterangan.Text = "JENIS PENTADBIRAN KONTRAK:";
                            LblKeterangan.CssClass = "fw-bold text-sm";
                            LblKeterangan.Visible = true;

                            for (var i = 0; i < JPK.Length; i++)
                            {
                                dt.Rows.Add(i + 1, JPK[i]);
                            }
                        }
                    }
                    else if (drv.Row["IdPBMMuktamad"].ToString() == "1")
                    {
                        dt.Rows.Add("SYARIKAT BERJAYA", drv.Row["SyarikatBerjayaKementerian"].ToString());
                        dt.Rows.Add("NILAI TAWARAN", "RM " + string.Format("{0:#,0.00}", drv.Row["NilaiTawaranKementerian"]));
                        dt.Rows.Add("TEMPOH", drv.Row["TempohKementerian"].ToString() + " BULAN");
                    }
                    else if ((drv.Row["IdPBMMuktamad"].ToString() == "2" && !string.IsNullOrEmpty(drv.Row["IdStatusKeputusanMOF"].ToString())))
                    {
                        dt.Rows.Add("SYARIKAT BERJAYA", drv.Row["SyarikatBerjayaMOF"].ToString());
                        dt.Rows.Add("NILAI TAWARAN", "RM " + string.Format("{0:#,0.00}", drv.Row["NilaiTawaranMOF"]));
                        dt.Rows.Add("TEMPOH", drv.Row["TempohMOF"].ToString() + " BULAN");
                    }
                    else if (drv.Row["IdPBMMuktamad"].ToString() == "2")
                    {
                        dt.Rows.Add("SYARIKAT DIPERAKU", drv.Row["SyarikatBerjayaKementerian"].ToString());
                        dt.Rows.Add("NILAI TAWARAN", "RM " + string.Format("{0:#,0.00}", drv.Row["NilaiTawaranKementerian"]));
                        dt.Rows.Add("TEMPOH", drv.Row["TempohKementerian"].ToString() + " BULAN");
                    }
                    detailsList.DataSource = dt;
                    detailsList.DataBind();
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

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            DataTable dtrslt = ((DataTable)ViewState["dtMesyuarat"]);
            int id = Convert.ToInt32(dtrslt.Rows[0]["Id"]);
            int idStatus = Convert.ToInt32(dtrslt.Rows[0]["IdStatusPengesahan"]);
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@Id", id } };

            if (idStatus == 1 || idStatus == 3)
            {
                Utils.ExcuteQuery("UPDATE Mesyuarat SET IdStatusPengesahan = 2 WHERE Id = @Id", queryParams);
                Session["flash.success"] = "Mesyuarat telah dihantar untuk pengesahan penyelia!";
                Response.Redirect("/mesyuarat/keputusan.aspx");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "notyf.success('Mesyuarat telah dihantar untuk kelulusan penyelia!');", true);
            }
        }
    }
}