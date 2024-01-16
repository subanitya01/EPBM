using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Globalization;


namespace EPBM
{
    public partial class segera : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private ReportFunction reportFunction = new ReportFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/auth/login.aspx", true);
                }
                if (!(User.IsInRole("Administrator") || User.IsInRole("Urusetia") || User.IsInRole("Pengesah")))
                {
                    Response.Redirect("/", true);
                }
                Load_GridData();
            }

        }
        

        private void Load_GridData()
        {
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>();
            string selectText = "Select * from  Papar_Permohonan ";
            string commandText = "where TarikhSahlaku <= DATEADD(day,14, CAST( GETDATE() AS Date ) ) AND TarikhHapus IS NULL and IdStatusPermohonan IN ('1','2','3')";

            commandText += " order by ID desc";

            DataTable dtProfile = Utils.GetDataTable(selectText + commandText, queryParams, "DefaultConnection");
            dtProfile.TableName = "Papar_Permohonan";

            DataSet ds = new DataSet();
            ds.Tables.Add(dtProfile);

            Senarai.DataSource = ds;
            Senarai.DataBind();


            string selectData2 = "Select *, PBM as MUKTAMAD, StatusKeputusan as STATUS, NamaPendekBahagianJabatan as JABATAN, " +
                                    "CASE WHEN IdPBMMuktamad = 1 THEN IdStatusKeputusanKementerian ELSE IdStatusKeputusanMOF END as IdStatusKeputusan, " +
                                    "CASE WHEN IdPBMMuktamad = 1 THEN CatatanKementerian ELSE CatatanMOF END as KETERANGAN ";
            string CommandText2 = "from Papar_Permohonan WHERE TarikhSahlaku <= DATEADD(day,14, CAST( GETDATE() AS Date ) ) AND TarikhHapus IS NULL AND " +
                "IdStatusPengesahan = 4 AND IdPBMMuktamad = 2 AND IdStatusKeputusanKementerian = 1 AND IdStatusKeputusanMOF IS NULL ORDER BY Id desc";

            DataTable dtPermohonan = Utils.GetDataTable(selectData2 + CommandText2, queryParams);

            GridView1.DataSource = dtPermohonan;
            GridView1.DataBind();
            ViewState["dtPermohonan"] = dtPermohonan;
        }


        protected void Senarai_DataBound(object sender, EventArgs e)
        {

            for (int i = 0; i <= Senarai.Rows.Count - 1; i++)
            {
                //ImageButton btnhapus = (ImageButton)Senarai.Rows[i].FindControl("btnhapus");
                HyperLink HyperLinkEdit = (HyperLink)Senarai.Rows[i].FindControl("HyperLinkEdit");
                //HyperLink HyperLinkMaju = (HyperLink)Senarai.Rows[i].FindControl("HyperLinkMaju");

                Label lblStatus = (Label)Senarai.Rows[i].FindControl("lblStatus");
                Label lblIDStatus = (Label)Senarai.Rows[i].FindControl("lblIDStatus");

                if (lblIDStatus.Text == "1")
                {
                    lblStatus.CssClass = "badge text-bg-primary";
                    HyperLinkEdit.Visible = true;
                    //btnhapus.Visible = true;
                }

                if (lblIDStatus.Text == "2")
                {
                    lblStatus.CssClass = "badge text-bg-warning";
                    HyperLinkEdit.Visible = true;
                    //btnhapus.Visible = true;
                }

                if (lblIDStatus.Text == "3")
                {
                    lblStatus.CssClass = "badge text-bg-success";
                    HyperLinkEdit.Visible = true;
                    //HyperLinkMaju.Visible = true;

                }

            }

        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                Label lblStatus = e.Row.FindControl("lblStatus") as Label;
                Label LblKeterangan = e.Row.FindControl("LblKeterangan") as Label;
                ListView detailsList = e.Row.FindControl("DetailsList") as ListView;
                HyperLink LinkEditMOF = e.Row.FindControl("LinkEditMOF") as HyperLink;
                string IdStatusKeputusan = !string.IsNullOrEmpty(drv.Row["IdStatusKeputusanMOF"].ToString()) ? drv.Row["IdStatusKeputusanMOF"].ToString() : drv.Row["IdStatusKeputusanKementerian"].ToString();

                LinkEditMOF.NavigateUrl = "/keputusan/mof.aspx?id=" + drv.Row["Id"] + "&ReturnURL=" + System.Web.HttpUtility.UrlEncode("/segera.aspx");

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
    }
}