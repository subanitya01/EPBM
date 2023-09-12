﻿using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace EPBM.mesyuarat
{
    public partial class edit_keputusan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void initStatus(string selected = null)
        {
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@selected", selected } };
            DataTable dtStatus = Utils.GetDataTable("Select *, CASE WHEN Id = @selected THEN 1 ELSE 0 END as Selected from StatusKeputusan", queryParams);
            /*Repeater1.DataSource = dtStatus;
            Repeater1.DataBind();*/

            foreach (DataRow row in dtStatus.Rows)
            {
                ListItem item = new ListItem();
                item.Text = row["Nama"].ToString();
                item.Value = row["Id"].ToString();
                item.Selected = Convert.ToBoolean(row["Selected"]);
                RadioStatus.Items.Add(item);
            }
        }

        protected void initPbmMuktamadList(string selected = null)
        {
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@selected", selected } };
            DataTable dtMuktamad = Utils.GetDataTable("Select *, CASE WHEN Id = @selected THEN 1 ELSE 0 END as Selected from PBMMuktamad", queryParams);
            listPbmMuktamad.Items.Add(new ListItem("SILA PILIH", ""));

            foreach (DataRow row in dtMuktamad.Rows)
            {
                ListItem item = new ListItem(row["Nama"].ToString(), row["Id"].ToString());
                item.Selected = Convert.ToBoolean(row["Selected"]);
                listPbmMuktamad.Items.Add(item);
                listPbmMuktamad2.Items.Add(item);
            }
        }
        protected void BindData()
        {
            try
            {
                var Id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(Id))
                    Utils.HttpNotFound();

                string CommandText2 = "Select Id, IdMesyuarat, IdStatusKeputusan, IdPBMMuktamad, Tajuk, KaedahPerolehan, Harga, TarikhSahlakuMS, TarikhTerimaMS, LulusPelanPPT, " +
                    "CASE WHEN IdJenisPertimbangan = 99 THEN LainJenisPertimbangan ELSE JenisPertimbangan END as JenisPertimbangan, " +
                    "CASE WHEN IdJenisPerolehan = 99 THEN LainJenisPerolehan ELSE Nama_JPerolehan END as JenisPerolehan, " +
                    "CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, " +
                    "CASE WHEN IdSumberPeruntukan = 99 THEN LainSumberPeruntukan ELSE SumberPeruntukan END as SumberPeruntukan, " +
                    "StatusKeputusan as STATUS, SyarikatBerjaya, Tempoh, TarikhSuratSetujuTerima, RujukanSuratSetujuTerima, LampiranKeputusan, AlasanKeputusan " +
                    "from Papar_Permohonan WHERE Id=@Id and TarikhHapus IS NULL and (IdStatusPermohonan IN (3,4))";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);

                LtlTajuk.Text               = dtPermohonan.Rows[0]["TAJUK"].ToString();
                LtlKaedahPerolehan.Text     = dtPermohonan.Rows[0]["KaedahPerolehan"].ToString();
                LtlJenisPertimbangan.Text   = dtPermohonan.Rows[0]["JenisPertimbangan"].ToString();
                LtlJenisPerolehan.Text      = dtPermohonan.Rows[0]["JenisPerolehan"].ToString();
                LtlJabatan.Text             = dtPermohonan.Rows[0]["Jabatan"].ToString();
                LtlHarga.Text               = string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["Harga"]);
                LtlSumberPeruntukan.Text    = dtPermohonan.Rows[0]["SumberPeruntukan"].ToString();
                LtlTarikhSahlaku.Text       = dtPermohonan.Rows[0]["TarikhSahlakuMS"].ToString();
                LtlTarikhTerima.Text        = dtPermohonan.Rows[0]["TarikhTerimaMS"].ToString();
                LtlLulusPelan.Text          = dtPermohonan.Rows[0]["LulusPelanPPT"].ToString();
                ViewState["dtPermohonan"]    = dtPermohonan;

                string CommandText = "Select * from PaparMesyuarat WHERE Id=@Id";
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@Id", dtPermohonan.Rows[0]["IdMesyuarat"] } };
                DataTable dtMesyuarat = Utils.GetDataTable(CommandText, queryParams);
                TajukMesyuarat.Text = "MESYUARAT " + dtMesyuarat.Rows[0]["JENIS"] + " BIL. " + dtMesyuarat.Rows[0]["BILANGAN"];

                btnSubmit.CommandArgument = Id;
                btnSubmit2.CommandArgument = Id;

                txtSyarikat.Text = dtPermohonan.Rows[0]["SyarikatBerjaya"].ToString();
                txtTempoh.Text = dtPermohonan.Rows[0]["Tempoh"].ToString();
                txtTarikhSetuju.Text = dtPermohonan.Rows[0]["TarikhSuratSetujuTerima"] == null ? ((DateTime)dtPermohonan.Rows[0]["TarikhSuratSetujuTerima"]).ToString("yyyy-MM-dd") : null;
                txtRujukanSetuju.Text = dtPermohonan.Rows[0]["RujukanSuratSetujuTerima"].ToString();
                txtAlasan.Text = dtPermohonan.Rows[0]["AlasanKeputusan"].ToString();

                initStatus(dtPermohonan.Rows[0]["IdStatusKeputusan"].ToString());
                initPbmMuktamadList(dtPermohonan.Rows[0]["IdPBMMuktamad"].ToString());

                if (string.IsNullOrEmpty(dtPermohonan.Rows[0]["LampiranKeputusan"].ToString()))
                {
                    attachmentLabel.Attributes.Add("class", attachmentLabel.Attributes["class"] + " d-none");
                    attachmentLabel2.Attributes.Add("class", attachmentLabel2.Attributes["class"] + " d-none");
                }
                else{
                    //if (dtPermohonan.Rows[0]["IdStatusKeputusan"].ToString() == "3")
                        LiteralFileName2.Text = dtPermohonan.Rows[0]["LampiranKeputusan"].ToString();
                    //else
                        LiteralFileName.Text = dtPermohonan.Rows[0]["LampiranKeputusan"].ToString();
                }
            }
            catch (Exception) { Utils.HttpNotFound(); }
        }

        protected void SaveSuccess(object sender, EventArgs e)
        {
            /*Page.Validate("success");

            if (Page.IsValid)
            {*/
                LinkButton btn = (LinkButton)sender;
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  btn.CommandArgument },
                        {"@Status",  RadioStatus.SelectedValue },
                        {"@syarikat",  txtSyarikat.Text },
                        {"@Tempoh",  txtTempoh.Text },
                        {"@PbmMuktamad",  listPbmMuktamad.SelectedValue },
                        {"@TarikhSetuju",  txtTarikhSetuju.Text ?? Convert.DBNull },
                        {"@RujukanSetuju",  txtRujukanSetuju.Text },
                        {"@Alasan", "" },
                    };

                try
                {
                    if (fileAttachment.HasFile)
                    {
                        string fnwext = Path.GetFileNameWithoutExtension(fileAttachment.PostedFile.FileName);
                        fnwext = fnwext.Length>40 ? fnwext.Substring(0, 30) : fnwext;
                        string ext = Path.GetExtension(fileAttachment.PostedFile.FileName);
                        string fn = fnwext + " " + DateTime.Now.ToFileTime() + ext;
                        string SaveLocation = Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + fn;
                        fileAttachment.SaveAs(SaveLocation);
                        queryParams.Add("@Lampiran", fn);

                        string oldFile = ((DataTable)ViewState["dtPermohonan"]).Rows[0]["LampiranKeputusan"].ToString();
                        if (!string.IsNullOrEmpty(oldFile)){
                            if (File.Exists(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile))
                            {
                                File.Delete(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile);
                            }
                        }
                    }
                    else if(keepAttachment.Value == "0")
                    {

                        queryParams.Add("@Lampiran", "");
                        string oldFile = ((DataTable)ViewState["dtPermohonan"]).Rows[0]["LampiranKeputusan"].ToString();
                        if (!string.IsNullOrEmpty(oldFile))
                        {
                            if (File.Exists(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile))
                            {
                                File.Delete(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile);
                            }
                        }
                    }
                    else
                        queryParams.Add("@Lampiran", ((DataTable)ViewState["dtPermohonan"]).Rows[0]["LampiranKeputusan"].ToString());

                    Utils.ExcuteQuery("UPDATE Permohonan SET IdStatusKeputusan=@Status, SyarikatBerjaya=@syarikat, Tempoh=@Tempoh, IdPBMMuktamad=@PbmMuktamad, TarikhSuratSetujuTerima=@TarikhSetuju, RujukanSuratSetujuTerima=@RujukanSetuju, LampiranKeputusan=@Lampiran, AlasanKeputusan=@Alasan WHERE Id=@Id", queryParams);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "window.notyf.error(\"" + ex.Message.Replace(System.Environment.NewLine, " ")+ "\");", true);
                    return;
                }
            
                Response.Redirect("/mesyuarat/senarai-keputusan.aspx?id=" + ((DataTable)ViewState["dtPermohonan"]).Rows[0]["IdMesyuarat"].ToString());

            //}
        }

        protected void SaveFail(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  btn.CommandArgument },
                        {"@Status",  RadioStatus.SelectedValue },
                        {"@syarikat",  Convert.DBNull },
                        {"@Tempoh",  Convert.DBNull },
                        {"@PbmMuktamad",   listPbmMuktamad2.SelectedValue },
                        {"@TarikhSetuju",  Convert.DBNull },
                        {"@RujukanSetuju",  "" },
                        {"@Alasan", txtAlasan.Text },
                    };

            try
            {
                if (fileAttachment2.HasFile)
                {
                    string fnwext = Path.GetFileNameWithoutExtension(fileAttachment2.PostedFile.FileName);
                    fnwext = fnwext.Length > 40 ? fnwext.Substring(0, 30) : fnwext;
                    string ext = Path.GetExtension(fileAttachment2.PostedFile.FileName);
                    string fn = fnwext + " " + DateTime.Now.ToFileTime() + ext;
                    string SaveLocation = Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + fn;
                    fileAttachment.SaveAs(SaveLocation);
                    queryParams.Add("@Lampiran", fn);

                    string oldFile = ((DataTable)ViewState["dtPermohonan"]).Rows[0]["LampiranKeputusan"].ToString();
                    if (!string.IsNullOrEmpty(oldFile))
                    {
                        if (File.Exists(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile))
                        {
                            File.Delete(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile);
                        }
                    }
                }
                else if (keepAttachment2.Value == "0")
                {

                    queryParams.Add("@Lampiran", "");
                    string oldFile = ((DataTable)ViewState["dtPermohonan"]).Rows[0]["LampiranKeputusan"].ToString();
                    if (!string.IsNullOrEmpty(oldFile))
                    {
                        if (File.Exists(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile))
                        {
                            File.Delete(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile);
                        }
                    }
                }
                else
                    queryParams.Add("@Lampiran", ((DataTable)ViewState["dtPermohonan"]).Rows[0]["LampiranKeputusan"].ToString());

                Utils.ExcuteQuery("UPDATE Permohonan SET " +
                    "IdStatusKeputusan=@Status, " +
                    "SyarikatBerjaya=@syarikat, " +
                    "Tempoh=@Tempoh, " +
                    "IdPBMMuktamad=@PbmMuktamad, " +
                    "TarikhSuratSetujuTerima=@TarikhSetuju, " +
                    "RujukanSuratSetujuTerima=@RujukanSetuju, " +
                    "LampiranKeputusan=@Lampiran, " +
                    "AlasanKeputusan=@Alasan " +
                    "WHERE Id=@Id", queryParams);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "window.notyf.error(\"" + ex.Message.Replace(System.Environment.NewLine, " ") + "\");", true);
                return;
            }

            Response.Redirect("/mesyuarat/senarai-keputusan.aspx?id=" + ((DataTable)ViewState["dtPermohonan"]).Rows[0]["IdMesyuarat"].ToString());
        }
    }
}