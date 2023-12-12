using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
            if (!(User.IsInRole("Administrator") || User.IsInRole("Urusetia") || User.IsInRole("Pengesah")))
            {
                Utils.HttpNotFound();
            }
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
                item.Text = "&nbsp;" + row["Nama"].ToString();
                item.Value = row["Id"].ToString();
                item.Selected = Convert.ToBoolean(row["Selected"]);
                RadioStatus.Items.Add(item);
            }
        }

        protected void initJenisPentadbiranKontrakList(string selected = null)
        {
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@selected", selected } };
            DataTable dtJenisPentadbiranKontrak = Utils.GetDataTable("Select *, CASE WHEN Id = @selected THEN 1 ELSE 0 END as Selected from JenisPentadbiranKontrak", queryParams);
            listJenisPentadbiranKontrak.Items.Add(new ListItem("SILA PILIH", ""));

            foreach (DataRow row in dtJenisPentadbiranKontrak.Rows)
            {
                ListItem item = new ListItem(row["Nama"].ToString(), row["Id"].ToString());
                item.Selected = Convert.ToBoolean(row["Selected"]);
                listJenisPentadbiranKontrak.Items.Add(item);
            }
        }

        protected void initPbmMuktamadList(string selected = null)
        {
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@selected", selected } };
            DataTable dtMuktamad = Utils.GetDataTable("Select *, CASE WHEN Id = @selected THEN 1 ELSE 0 END as Selected from PBMMuktamad", queryParams);
            listPbmMuktamad.Items.Add(new ListItem("SILA PILIH", ""));
            listPbmMuktamad2.Items.Add(new ListItem("SILA PILIH", ""));
            listPbmMuktamad3.Items.Add(new ListItem("SILA PILIH", ""));
            listPbmMuktamad4.Items.Add(new ListItem("SILA PILIH", ""));

            foreach (DataRow row in dtMuktamad.Rows)
            {
                ListItem item = new ListItem(row["Nama"].ToString(), row["Id"].ToString());
                item.Selected = Convert.ToBoolean(row["Selected"]);
                listPbmMuktamad.Items.Add(item);
                listPbmMuktamad2.Items.Add(item);
                listPbmMuktamad3.Items.Add(item);
                listPbmMuktamad4.Items.Add(item);
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

                foreach(DataRow row in dtSyarikat.Rows)
                {
                    if (sortlistSyarikat.ContainsKey(row["SyarikatBerjaya"].ToString()))
                    {
                        if (Convert.ToInt32(sortlistSyarikat[row["SyarikatBerjaya"].ToString()]) == 0)
                            sortlistSyarikat[row["SyarikatBerjaya"].ToString()] = true;
                    }
                    else sortlistSyarikat.Add(row["SyarikatBerjaya"].ToString(), (Convert.ToInt32(row["more2year"])==1 && Convert.ToInt32(row["total"]) >= 3));
                }
                foreach (KeyValuePair<string, bool> kvp in sortlistSyarikat)
                {
                    if (kvp.Value) listSyarikat.Add(kvp.Key, "Syarikat ini telah dilantik melebihi 2 kali dalam 2 tahun");
                    else listSyarikat.Add(kvp.Key, "");
                }
                companyList.Text = JsonConvert.SerializeObject(listSyarikat);
                
                string CommandText2 = "Select Id, IdMesyuarat, IdStatusKeputusan, IdJenisPertimbangan, IdPBMMuktamad, Tajuk, KaedahPerolehan, Harga, TarikhSahlakuMS, TarikhTerimaMS, LulusPelanPPT, " +
                    "CASE WHEN IdJenisPertimbangan = 99 THEN concat(JenisPertimbangan, ' - ', LainJenisPertimbangan) ELSE JenisPertimbangan END as JenisPertimbangan, " +
                    "CASE WHEN IdJenisPerolehan = 99 THEN concat(Nama_JPerolehan, ' - ', LainJenisPerolehan) ELSE Nama_JPerolehan END as JenisPerolehan, " +
                    "CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, " +
                    "CASE WHEN IdSumberPeruntukan = 99 THEN concat(SumberPeruntukan, ' - ', LainSumberPeruntukan) ELSE SumberPeruntukan END as SumberPeruntukan, " +
                    "StatusKeputusan as STATUS, SyarikatBerjaya, Tempoh, TarikhSuratSetujuTerima, RujukanSuratSetujuTerima, LampiranKeputusan, AlasanKeputusan, NilaiTawaran, IdJenisPentadbiranKontrak, " +
                    "MOFSyarikatDiperaku, MOFNilaiTawaran, MOFTempoh " +
                    "from Papar_Permohonan WHERE Id=@Id and TarikhHapus IS NULL and (IdStatusPermohonan IN (3,4)) and IdStatusPengesahan <> 4";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);

                if (dtPermohonan.Rows.Count < 1) Utils.HttpNotFound();

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

                if(dtPermohonan.Rows[0]["IdPBMMuktamad"].ToString() == "1")
                {
                    txtSyarikat.Text = dtPermohonan.Rows[0]["SyarikatBerjaya"].ToString();
                    txtTempoh.Text = txtTempoh2.Text = dtPermohonan.Rows[0]["Tempoh"].ToString();
                    txtNilaiTawaran.Text = string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["NilaiTawaran"]);
                }
                else
                {
                    txtSyarikat.Text = dtPermohonan.Rows[0]["MOFSyarikatDiperaku"].ToString();
                    txtTempoh.Text = txtTempoh2.Text = dtPermohonan.Rows[0]["MOFTempoh"].ToString();
                    txtNilaiTawaran.Text = string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["MOFNilaiTawaran"]);
                }
                txtAlasan.Text = txtAlasan2.Text = dtPermohonan.Rows[0]["AlasanKeputusan"].ToString();

                initStatus(dtPermohonan.Rows[0]["IdStatusKeputusan"].ToString());
                initPbmMuktamadList(dtPermohonan.Rows[0]["IdPBMMuktamad"].ToString());
                initJenisPentadbiranKontrakList(dtPermohonan.Rows[0]["IdJenisPentadbiranKontrak"].ToString());

                if(dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() == "2")
                {
                    PanelSuccess1.Visible = false;
                    PanelSuccess2.Visible = true;
                    PanelSuccess3.Visible = false;
                }
                else if (dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() == "99")
                {
                    PanelSuccess1.Visible = false;
                    PanelSuccess2.Visible = false;
                    PanelSuccess3.Visible = true;
                }
                else
                {
                    PanelSuccess1.Visible = true;
                    PanelSuccess2.Visible = false;
                    PanelSuccess3.Visible = false;
                }

                if (string.IsNullOrEmpty(dtPermohonan.Rows[0]["LampiranKeputusan"].ToString()))
                {
                    attachmentLabel.Attributes.Add("class", attachmentLabel.Attributes["class"] + " d-none");
                    attachmentLabel2.Attributes.Add("class", attachmentLabel2.Attributes["class"] + " d-none");
                    attachmentLabel3.Attributes.Add("class", attachmentLabel2.Attributes["class"] + " d-none");
                    attachmentLabel4.Attributes.Add("class", attachmentLabel2.Attributes["class"] + " d-none");
                }
                else{
                    //if (dtPermohonan.Rows[0]["IdStatusKeputusan"].ToString() == "3")
                        LiteralFileName2.Text = dtPermohonan.Rows[0]["LampiranKeputusan"].ToString();
                    //else
                        LiteralFileName.Text = dtPermohonan.Rows[0]["LampiranKeputusan"].ToString();
                        LiteralFileName3.Text = dtPermohonan.Rows[0]["LampiranKeputusan"].ToString();
                        LiteralFileName4.Text = dtPermohonan.Rows[0]["LampiranKeputusan"].ToString();
                }
           // }
           // catch (Exception) { Utils.HttpNotFound(); }
        }

        protected void SaveSuccess(object sender, EventArgs e)
        {
            /*Page.Validate("success");

            if (Page.IsValid)
            {*/
            string IdJenisPertimbangan = ((DataTable)ViewState["dtPermohonan"]).Rows[0]["IdJenisPertimbangan"].ToString();

            if (IdJenisPertimbangan == "2")
            {
                LinkButton btn = (LinkButton)sender;
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  btn.CommandArgument },
                        {"@Status",  RadioStatus.SelectedValue },
                        {"@Tempoh",  txtTempoh2.Text },
                        {"@PbmMuktamad",  listPbmMuktamad3.SelectedValue },
                        {"@JenisPentadbiranKontrak",  listJenisPentadbiranKontrak.SelectedValue },
                        {"@Alasan", "" },
                    };

                try
                {
                    if (fileAttachment3.HasFile)
                    {
                        string fnwext = Path.GetFileNameWithoutExtension(fileAttachment3.PostedFile.FileName);
                        fnwext = fnwext.Length > 40 ? fnwext.Substring(0, 30) : fnwext;
                        string ext = Path.GetExtension(fileAttachment3.PostedFile.FileName);
                        string fn = fnwext + " " + DateTime.Now.ToFileTime() + ext;
                        string SaveLocation = Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + fn;
                        fileAttachment3.SaveAs(SaveLocation);
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
                    else if (keepAttachment3.Value == "0")
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
                    
                    Utils.ExcuteQuery("UPDATE Permohonan SET IdStatusKeputusan=@Status, IdJenisPentadbiranKontrak=@JenisPentadbiranKontrak, IdPBMMuktamad=@PbmMuktamad, Tempoh=@Tempoh, LampiranKeputusan=@Lampiran, AlasanKeputusan=@Alasan WHERE Id=@Id", queryParams);
                    
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "window.notyf.error(\"" + ex.Message.Replace(System.Environment.NewLine, " ") + "\");", true);
                    return;
                }
            }
            else if (IdJenisPertimbangan == "99")
            {
                LinkButton btn = (LinkButton)sender;
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  btn.CommandArgument },
                        {"@Status",  RadioStatus.SelectedValue },
                        {"@PbmMuktamad",  listPbmMuktamad4.SelectedValue },
                        {"@Alasan", txtAlasan2.Text },
                    };

                try
                {
                    if (fileAttachment4.HasFile)
                    {
                        string fnwext = Path.GetFileNameWithoutExtension(fileAttachment4.PostedFile.FileName);
                        fnwext = fnwext.Length > 40 ? fnwext.Substring(0, 30) : fnwext;
                        string ext = Path.GetExtension(fileAttachment4.PostedFile.FileName);
                        string fn = fnwext + " " + DateTime.Now.ToFileTime() + ext;
                        string SaveLocation = Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + fn;
                        fileAttachment4.SaveAs(SaveLocation);
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
                    else if (keepAttachment4.Value == "0")
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

                    Utils.ExcuteQuery("UPDATE Permohonan SET IdStatusKeputusan=@Status, IdPBMMuktamad=@PbmMuktamad, LampiranKeputusan=@Lampiran, AlasanKeputusan=@Alasan WHERE Id=@Id", queryParams);

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "window.notyf.error(\"" + ex.Message.Replace(System.Environment.NewLine, " ") + "\");", true);
                    return;
                }
            }
            else
            {
                LinkButton btn = (LinkButton)sender;
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  btn.CommandArgument },
                        {"@Status",  RadioStatus.SelectedValue },
                        {"@syarikat",  txtSyarikat.Text },
                        {"@Tempoh",  txtTempoh.Text },
                        {"@PbmMuktamad",  listPbmMuktamad.SelectedValue },
                        {"@NilaiTawaran",  txtNilaiTawaran.Text },
                        {"@Alasan", "" },
                    };

                try
                {
                    if (fileAttachment.HasFile)
                    {
                        string fnwext = Path.GetFileNameWithoutExtension(fileAttachment.PostedFile.FileName);
                        fnwext = fnwext.Length > 40 ? fnwext.Substring(0, 30) : fnwext;
                        string ext = Path.GetExtension(fileAttachment.PostedFile.FileName);
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
                    else if (keepAttachment.Value == "0")
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
                    if (listPbmMuktamad.SelectedValue == "1")
                        Utils.ExcuteQuery("UPDATE Permohonan SET IdStatusKeputusan=@Status, MOFSyarikatDiperaku=NULL, MOFTempoh=NULL, MOFNilaiTawaran=NULL, SyarikatBerjaya=@syarikat, Tempoh=@Tempoh, NilaiTawaran=@NilaiTawaran, IdPBMMuktamad=@PbmMuktamad, LampiranKeputusan=@Lampiran, AlasanKeputusan=@Alasan WHERE Id=@Id", queryParams);
                    else
                        Utils.ExcuteQuery("UPDATE Permohonan SET IdStatusKeputusan=@Status, MOFSyarikatDiperaku=@syarikat, MOFTempoh=@Tempoh, MOFNilaiTawaran=@NilaiTawaran, SyarikatBerjaya=NULL, Tempoh=NULL, NilaiTawaran=NULL, IdPBMMuktamad=@PbmMuktamad, LampiranKeputusan=@Lampiran, AlasanKeputusan=@Alasan WHERE Id=@Id", queryParams);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "window.notyf.error(\"" + ex.Message.Replace(System.Environment.NewLine, " ") + "\");", true);
                    return;
                }
            }

                if (Request.QueryString["ReturnURL"] != null)
                    Response.Redirect(Request.QueryString["ReturnURL"]);
                else
                    Response.Redirect("/mesyuarat/keputusan.aspx?id=" + ((DataTable)ViewState["dtPermohonan"]).Rows[0]["IdMesyuarat"].ToString());
            
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
                        {"@NilaiTawaran",  Convert.DBNull },
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
                    "NilaiTawaran=@NilaiTawaran, " +
                    "LampiranKeputusan=@Lampiran, " +
                    "AlasanKeputusan=@Alasan " +
                    "WHERE Id=@Id", queryParams);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "window.notyf.error(\"" + ex.Message.Replace(System.Environment.NewLine, " ") + "\");", true);
                return;
            }

            Response.Redirect("/mesyuarat/keputusan.aspx?id=" + ((DataTable)ViewState["dtPermohonan"]).Rows[0]["IdMesyuarat"].ToString());
        }
        protected void SyarikatBerjaya_Change(Object sender, EventArgs e)
        {
        }
    }
}