using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        protected void initStatus(string JenisPertimbangan, string selected = null)
        {
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@selected", selected } };
            DataTable dtStatus = Utils.GetDataTable("Select *, CASE WHEN Id = @selected THEN 1 ELSE 0 END as Selected from StatusKeputusan", queryParams);
            /*Repeater1.DataSource = dtStatus;
            Repeater1.DataBind();*/

            foreach (DataRow row in dtStatus.Rows)
            {
                ListItem item = new ListItem();
                item.Value = row["Id"].ToString();

                if (JenisPertimbangan == "2")
                    item.Text = "&nbsp;" + row["PentadbiranKontrakKementerian"].ToString();
                else if (JenisPertimbangan == "99")
                    item.Text = "&nbsp;" + row["Nama"].ToString();
                else
                    item.Text = "&nbsp;" + row["PerlantikanKontraktorKementerian"].ToString();

                item.Selected = Convert.ToBoolean(row["Selected"]);
                RadioStatus.Items.Add(item);
            }
        }

        protected void initJenisPentadbiranKontrakList(string selected = null)
        {
            DataTable dtJenisPentadbiranKontrak;

            if (!String.IsNullOrEmpty(selected))
                dtJenisPentadbiranKontrak = Utils.GetDataTable("Select *, CASE WHEN Id in (" + selected + ") THEN 1 ELSE 0 END as Selected from JenisPentadbiranKontrak");
            else
                dtJenisPentadbiranKontrak = Utils.GetDataTable("Select *, 0 as Selected from JenisPentadbiranKontrak");
            //listJenisPentadbiranKontrak.Items.Add(new ListItem("SILA PILIH", ""));

            foreach (DataRow row in dtJenisPentadbiranKontrak.Rows)
            {
                ListItem item = new ListItem(row["Nama"].ToString(), row["Id"].ToString());
                item.Selected = Convert.ToBoolean(row["Selected"]);
                listJenisPentadbiranKontrak.Items.Add(item);
            }
        }

        protected void BindData()
        {
            //try
            //{
            var Id = Request.QueryString["id"];
            if (string.IsNullOrEmpty(Id))
                Utils.HttpNotFound();

            string CommandText1 = "select Nama, case when TarikhSuratSetujuTerima > DATEADD(year,-2,GETDATE()) then 1 else 0 end as more2year, count(*) as total from PaparSyarikat group by Nama, case when TarikhSuratSetujuTerima > DATEADD(year,-2,GETDATE()) then 1 else 0 end";
            DataTable dtSyarikat = Utils.GetDataTable(CommandText1);
            SortedList<string, bool> sortlistSyarikat = new SortedList<string, bool>();
            SortedList<string, string> listSyarikat = new SortedList<string, string>();

            foreach (DataRow row in dtSyarikat.Rows)
            {
                if (sortlistSyarikat.ContainsKey(row["Nama"].ToString()))
                {
                    if (Convert.ToInt32(sortlistSyarikat[row["Nama"].ToString()]) == 0)
                        sortlistSyarikat[row["Nama"].ToString()] = true;
                }
                else sortlistSyarikat.Add(row["Nama"].ToString(), (Convert.ToInt32(row["more2year"]) == 1 && Convert.ToInt32(row["total"]) >= 3));
            }
            foreach (KeyValuePair<string, bool> kvp in sortlistSyarikat)
            {
                if (kvp.Value) listSyarikat.Add(kvp.Key, "Syarikat ini telah dilantik melebihi 2 kali dalam 2 tahun");
                else listSyarikat.Add(kvp.Key, "");
            }
            companyList.Text = JsonConvert.SerializeObject(listSyarikat);

            string CommandText2 = "Select *, NamaPendekBahagianJabatan as Jabatan, StatusKeputusan as STATUS, " +
                    "CASE WHEN IdJenisPertimbangan = 99 THEN concat(JenisPertimbangan, ' - ', LainJenisPertimbangan) ELSE JenisPertimbangan END as JenisPertimbangan, " +
                    "CASE WHEN IdJenisPerolehan = 99 THEN concat(Nama_JPerolehan, ' - ', LainJenisPerolehan) ELSE Nama_JPerolehan END as JenisPerolehan, " +
                    "CASE WHEN IdSumberPeruntukan = 99 THEN concat(SumberPeruntukan, ' - ', LainSumberPeruntukan) ELSE SumberPeruntukan END as SumberPeruntukan " +
                   "from Papar_Permohonan WHERE Id=@Id and TarikhHapus IS NULL and IdStatusPengesahan = 4 AND IdStatusKeputusanKementerian = 1 AND IdPBMMuktamad = 2";
            Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
            DataTable dtPermohonan = Utils.GetDataTable(CommandText2, queryParams2);
            ViewState["dtPermohonan"] = dtPermohonan;

            if (dtPermohonan.Rows.Count < 1) Utils.HttpNotFound();

            if (dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() == "2")
                LtlStatusKementerian.Text = dtPermohonan.Rows[0]["StatusPentadbiranKontrakKementerian"].ToString();
            else if (dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() == "99")
                LtlStatusKementerian.Text = dtPermohonan.Rows[0]["StatusLainKementerian"].ToString();
            else
                LtlStatusKementerian.Text = dtPermohonan.Rows[0]["StatusPerlantikanKontraktorKementerian"].ToString();

            TajukPerolehan.Text = dtPermohonan.Rows[0]["Tajuk"].ToString();
            TajukMesyuarat.Text = dtPermohonan.Rows[0]["MESYUARAT"].ToString();
            LtlJPKKementerian.Text = dtPermohonan.Rows[0]["JenisPentadbiranKontrakKementerian"].ToString();
            LtlSyarikatKementerian.Text = dtPermohonan.Rows[0]["SyarikatBerjayaKementerian"].ToString();
            LtlTempohKementerian.Text = dtPermohonan.Rows[0]["TempohKementerian"].ToString();
            LtlNilaiTawaranKementerian.Text = string.Format("{0:#,0.00}", dtPermohonan.Rows[0]["NilaiTawaranKementerian"]);
            LtlCatatanKementerian.Text = dtPermohonan.Rows[0]["CatatanKementerian"].ToString().Replace(Environment.NewLine, "<br />");

            if (string.IsNullOrEmpty(dtPermohonan.Rows[0]["LampiranKementerian"].ToString()))
                LinkLampiranKementerian.Visible = false;
            else
            {
                LinkLampiranKementerian.NavigateUrl = "/uploads/lampiran-keputusan/" + dtPermohonan.Rows[0]["LampiranKementerian"];
                LinkLampiranKementerian.Text = LinkLampiranKementerian.Text + dtPermohonan.Rows[0]["LampiranKementerian"];
                //LinkLampiran.Attributes.Add("download", dtPermohonan.Rows[0]["LampiranKeputusan"].ToString());
            }

            LtlIdStatusKementerian.Text = !string.IsNullOrEmpty(dtPermohonan.Rows[0]["IdStatusKeputusanKementerian"].ToString()) ? dtPermohonan.Rows[0]["IdStatusKeputusanKementerian"].ToString() : "null";
            LtlIdJenisPertimbangan.Text = dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString();

            txtSyarikat.Text = dtPermohonan.Rows[0]["SyarikatBerjayaMOF"].ToString();
            txtTempoh.Text = dtPermohonan.Rows[0]["TempohMOF"].ToString();
            txtNilaiTawaran.Text = string.Format("{0:0.00}", dtPermohonan.Rows[0]["NilaiTawaranMOF"]);
            txtAlasan.Text = txtAlasan2.Text = dtPermohonan.Rows[0]["CatatanMOF"].ToString();
            
            btnSubmit.CommandArgument = Id;
            btnSubmit2.CommandArgument = Id;


            initStatus(dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString(), dtPermohonan.Rows[0]["IdStatusKeputusanMOF"].ToString());
            //initPbmMuktamadList(dtPermohonan.Rows[0]["IdPBMMuktamad"].ToString());
            initJenisPentadbiranKontrakList(dtPermohonan.Rows[0]["IdJenisPentadbiranKontrakMOF"].ToString());

            if (dtPermohonan.Rows[0]["IdJenisPertimbangan"].ToString() == "2")
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

            if (string.IsNullOrEmpty(dtPermohonan.Rows[0]["LampiranMOF"].ToString()))
            {
                attachmentLabel.Attributes.Add("class", attachmentLabel.Attributes["class"] + " d-none");
                attachmentLabel2.Attributes.Add("class", attachmentLabel2.Attributes["class"] + " d-none");
                attachmentLabel3.Attributes.Add("class", attachmentLabel2.Attributes["class"] + " d-none");
                attachmentLabel4.Attributes.Add("class", attachmentLabel2.Attributes["class"] + " d-none");
            }
            else
            {
                //if (dtPermohonan.Rows[0]["IdStatusKeputusan"].ToString() == "3")
                LiteralFileName2.Text = dtPermohonan.Rows[0]["LampiranMOF"].ToString();
                //else
                LiteralFileName.Text = dtPermohonan.Rows[0]["LampiranMOF"].ToString();
                LiteralFileName3.Text = dtPermohonan.Rows[0]["LampiranMOF"].ToString();
                LiteralFileName4.Text = dtPermohonan.Rows[0]["LampiranMOF"].ToString();
            }
            // }
            // catch (Exception) { Utils.HttpNotFound(); }
        }

        protected void SaveSuccess(object sender, EventArgs e)
        {
            /*Page.Validate("success");

            if (Page.IsValid)
            {*/
            LinkButton btn = (LinkButton)sender;
            string IdJenisPertimbangan = ((DataTable)ViewState["dtPermohonan"]).Rows[0]["IdJenisPertimbangan"].ToString();

            Dictionary<string, dynamic> queryParamsKM1 = new Dictionary<string, dynamic>()
                    {
                        {"@IdPermohonan",  btn.CommandArgument },
                        {"@Status",  RadioStatus.SelectedValue },
                        {"@TarikhKemaskini", DateTime.Now },
                        {"@DikemaskiniOleh", Session["Profile.ICNO"] },
                    };

            try
            {

                if (IdJenisPertimbangan == "2")
                {

                    string lampiran = UploadAttachment(fileAttachment3, keepAttachment3.Value);
                    queryParamsKM1.Add("@Lampiran", lampiran);
                    queryParamsKM1.Add("@Alasan", "");

                    Utils.ExcuteQuery("IF EXISTS (SELECT 1 FROM KeputusanMOF WHERE IdPermohonan = @IdPermohonan) " +
                            "BEGIN UPDATE KeputusanMOF SET IdStatusKeputusan=@Status, Lampiran=@Lampiran, Catatan=@Alasan, TarikhKemaskini=@TarikhKemaskini, DikemaskiniOleh=@DikemaskiniOleh WHERE IdPermohonan=@IdPermohonan; END " +
                            "ELSE BEGIN INSERT INTO KeputusanMOF (IdStatusKeputusan, Lampiran, Catatan, IdPermohonan, TarikhDicipta, DiciptaOleh) values(@Status, @Lampiran, @Alasan, @IdPermohonan, @TarikhKemaskini, @DikemaskiniOleh) END", queryParamsKM1);

                    string CommandText3 = "select * from KeputusanMOF where IdPermohonan=@id";
                    Dictionary<string, dynamic> queryParams3 = new Dictionary<string, dynamic>() { { "@Id", btn.CommandArgument } };
                    DataTable dtKeputusanMOF = Utils.GetDataTable(CommandText3, queryParams3);

                    string insertQuery = "";
                    Dictionary<string, dynamic> queryParamsKK1 = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  dtKeputusanMOF.Rows[0]["Id"].ToString() },
                    };

                    for (int i = 0; i < listJenisPentadbiranKontrak.Items.Count; i++)
                    {
                        if (listJenisPentadbiranKontrak.Items[i].Selected)
                        {
                            insertQuery += "(@Id, @v" + i + "),";
                            //first = false;
                            queryParamsKK1.Add("@v" + i, listJenisPentadbiranKontrak.Items[i].Value);
                        }

                    }

                    Utils.ExcuteQuery("DELETE FROM KeputusanPentadbiranKontrak WHERE IdKeputusanMOF=@Id", queryParamsKK1);

                    if (insertQuery != "")
                    {
                        insertQuery = insertQuery.Remove(insertQuery.Length - 1, 1);
                        Utils.ExcuteQuery("INSERT INTO KeputusanPentadbiranKontrak(IdKeputusanMOF,IdJenisPentadbiranKontrak) values " + insertQuery, queryParamsKK1);
                    }
                }
                else if (IdJenisPertimbangan == "99")
                {
                    string lampiran = UploadAttachment(fileAttachment4, keepAttachment4.Value);
                    queryParamsKM1.Add("@Lampiran", lampiran);
                    queryParamsKM1.Add("@Alasan", txtAlasan2.Text);

                    Utils.ExcuteQuery("IF EXISTS (SELECT 1 FROM KeputusanMOF WHERE IdPermohonan = @IdPermohonan) " +
                            "BEGIN UPDATE KeputusanMOF SET IdStatusKeputusan=@Status, Lampiran=@Lampiran, Catatan=@Alasan, TarikhKemaskini=@TarikhKemaskini, DikemaskiniOleh=@DikemaskiniOleh WHERE IdPermohonan=@IdPermohonan; END " +
                            "ELSE BEGIN INSERT INTO KeputusanMOF (IdStatusKeputusan, Lampiran, Catatan, IdPermohonan, TarikhDicipta, DiciptaOleh) values(@Status, @Lampiran, @Alasan, @IdPermohonan, @TarikhKemaskini, @DikemaskiniOleh) END", queryParamsKM1);
                }
                else
                {
                    string lampiran = UploadAttachment(fileAttachment, keepAttachment.Value);
                    queryParamsKM1.Add("@Lampiran", lampiran);
                    queryParamsKM1.Add("@Alasan", "");

                    Utils.ExcuteQuery("IF EXISTS (SELECT 1 FROM KeputusanMOF WHERE IdPermohonan = @IdPermohonan) " +
                            "BEGIN UPDATE KeputusanMOF SET IdStatusKeputusan=@Status, Lampiran=@Lampiran, Catatan=@Alasan, TarikhKemaskini=@TarikhKemaskini, DikemaskiniOleh=@DikemaskiniOleh WHERE IdPermohonan=@IdPermohonan; END " +
                            "ELSE BEGIN INSERT INTO KeputusanMOF (IdStatusKeputusan, Lampiran, Catatan, IdPermohonan, TarikhDicipta, DiciptaOleh) values(@Status, @Lampiran, @Alasan, @IdPermohonan, @TarikhKemaskini, @DikemaskiniOleh) END", queryParamsKM1);

                    string CommandText3 = "select * from KeputusanMOF where IdPermohonan=@id";
                    Dictionary<string, dynamic> queryParams3 = new Dictionary<string, dynamic>() { { "@Id", btn.CommandArgument } };
                    DataTable dtKeputusanMOF = Utils.GetDataTable(CommandText3, queryParams3);

                    Dictionary<string, dynamic> queryParamsSyarikat = new Dictionary<string, dynamic>()
                    {
                        {"@Syarikat",  txtSyarikat.Text.Trim() },
                    };

                    Utils.ExcuteQuery("IF NOT EXISTS (SELECT 1 FROM Syarikat WHERE Nama = @Syarikat) " +
                        "BEGIN INSERT INTO Syarikat (Nama) values(@Syarikat) END", queryParamsSyarikat);

                    string CommandText4 = "select * from Syarikat where Nama=@Syarikat";
                    DataTable dtSyarikat = Utils.GetDataTable(CommandText4, queryParamsSyarikat);

                    Dictionary<string, dynamic> queryParamsKK2 = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  dtKeputusanMOF.Rows[0]["Id"].ToString() },
                        {"@IdSyarikat",  dtSyarikat.Rows[0]["Id"].ToString() },
                        {"@Tempoh",  txtTempoh.Text },
                        {"@NilaiTawaran",  Utils.ParseDecimal(txtNilaiTawaran.Text) },
                    };

                    Utils.ExcuteQuery("IF EXISTS (SELECT 1 FROM KeputusanPerlantikanKontraktor WHERE IdKeputusanMOF = @Id) " +
                        "BEGIN UPDATE KeputusanPerlantikanKontraktor SET IdSyarikat=@IdSyarikat, NilaiTawaran=@NilaiTawaran, Tempoh=@Tempoh WHERE IdKeputusanMOF=@Id; END " +
                        "ELSE BEGIN INSERT INTO KeputusanPerlantikanKontraktor (IdSyarikat, NilaiTawaran, Tempoh, IdKeputusanMOF) values(@IdSyarikat, @NilaiTawaran, @Tempoh, @Id) END", queryParamsKK2);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "window.notyf.error(\"" + ex.Message.Replace(System.Environment.NewLine, " ") + "\");", true);
                return;
            }

            Session["flash.success"] = "Keputusan Perolehan berjaya disimpan!";

            if (Request.QueryString["ReturnURL"] != null)
                Response.Redirect(System.Web.HttpUtility.UrlDecode(Request.QueryString["ReturnURL"]));
            else
                Response.Redirect("~/keputusan/mof.aspx?id=" + btn.CommandArgument);

            //}
        }

        protected void SaveFail(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            Dictionary<string, dynamic> queryParamsKM1 = new Dictionary<string, dynamic>()
                    {
                        {"@IdPermohonan",  btn.CommandArgument },
                        {"@Status",  RadioStatus.SelectedValue },
                        {"@Alasan", txtAlasan.Text },
                        {"@TarikhKemaskini", DateTime.Now },
                        {"@DikemaskiniOleh", Session["Profile.ICNO"] },
                    };

            try
            {
                string lampiran = UploadAttachment(fileAttachment2, keepAttachment2.Value);
                queryParamsKM1.Add("@Lampiran", lampiran);

                Utils.ExcuteQuery("IF EXISTS (SELECT 1 FROM KeputusanMOF WHERE IdPermohonan = @IdPermohonan) " +
                        "BEGIN UPDATE KeputusanMOF SET IdStatusKeputusan=@Status, Lampiran=@Lampiran, Catatan=@Alasan, TarikhKemaskini=@TarikhKemaskini, DikemaskiniOleh=@DikemaskiniOleh WHERE IdPermohonan=@IdPermohonan; END " +
                        "ELSE BEGIN INSERT INTO KeputusanMOF (IdStatusKeputusan, Lampiran, Catatan, IdPermohonan, TarikhDicipta, DiciptaOleh) values(@Status, @Lampiran, @Alasan, @IdPermohonan, @TarikhKemaskini, @DikemaskiniOleh) END", queryParamsKM1);

                string CommandText3 = "select * from KeputusanMOF where IdPermohonan=@id";
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                    {
                        {"@Id",  btn.CommandArgument },
                    };
                DataTable dtKeputusanMOF = Utils.GetDataTable(CommandText3, queryParams);

                Dictionary<string, dynamic> queryParams3 = new Dictionary<string, dynamic>() { { "@IdKeputusan", dtKeputusanMOF.Rows[0]["Id"] } };

                Utils.ExcuteQuery("DELETE FROM KeputusanPentadbiranKontrak where IdKeputusanMOF=@IdKeputusan", queryParams3);
                Utils.ExcuteQuery("DELETE FROM KeputusanPerlantikanKontraktor where IdKeputusanMOF=@IdKeputusan", queryParams3);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "window.notyf.error(\"" + ex.Message.Replace(System.Environment.NewLine, " ") + "\");", true);
                return;
            }

            Session["flash.success"] = "Keputusan Perolehan berjaya disimpan!";

            if (Request.QueryString["ReturnURL"] != null)
                Response.Redirect(System.Web.HttpUtility.UrlDecode(Request.QueryString["ReturnURL"]));
            else
                Response.Redirect("~/keputusan/mof.aspx?id=" + btn.CommandArgument);
        }

        protected string UploadAttachment(FileUpload attachment, string keep)
        {
            if (attachment.HasFile)
            {
                string fnwext = Path.GetFileNameWithoutExtension(attachment.PostedFile.FileName);
                fnwext = fnwext.Length > 40 ? fnwext.Substring(0, 30) : fnwext;
                string ext = Path.GetExtension(attachment.PostedFile.FileName);
                string fn = fnwext + " " + DateTime.Now.ToFileTime() + ext;
                string SaveLocation = Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + fn;
                attachment.SaveAs(SaveLocation);

                string oldFile = ((DataTable)ViewState["dtPermohonan"]).Rows[0]["LampiranMOF"].ToString();
                if (!string.IsNullOrEmpty(oldFile))
                {
                    if (File.Exists(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile))
                    {
                        File.Delete(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile);
                    }
                }
                return fn;
            }
            else if (keep == "0")
            {

                string oldFile = ((DataTable)ViewState["dtPermohonan"]).Rows[0]["LampiranMOF"].ToString();
                if (!string.IsNullOrEmpty(oldFile))
                {
                    if (File.Exists(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile))
                    {
                        File.Delete(Server.MapPath("..\\uploads\\lampiran-keputusan") + "\\" + oldFile);
                    }
                }
                return "";
            }
            else
                return ((DataTable)ViewState["dtPermohonan"]).Rows[0]["LampiranMOF"].ToString();
        }
    }
}