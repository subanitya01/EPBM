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


            string selectData2 = "Select Id, Tajuk, CASE WHEN IdJabatan = 1 THEN NamaPendekBahagian ELSE ShortName END as Jabatan, IdStatusKeputusan, IdJenisPertimbangan, IdPBMMuktamad, PBM as MUKTAMAD, " +
                                "StatusKeputusan as STATUS, SyarikatBerjaya, NilaiTawaran, Tempoh, MOFSyarikatDiperaku, MOFNilaiTawaran, MOFTempoh, AlasanKeputusan as KETERANGAN, MESYUARAT, JenisPentadbiranKontrak ";
            string CommandText2 = "from Papar_Permohonan WHERE TarikhSahlaku <= DATEADD(day,14, CAST( GETDATE() AS Date ) ) AND TarikhHapus IS NULL AND " +
                "IdStatusPengesahan = 4 AND IdStatusKeputusan = 1 AND IdPBMMuktamad = 2 AND IdJenisPertimbangan NOT IN (2, 99) AND (SyarikatBerjaya IS NULL OR SyarikatBerjaya = '') ORDER BY Id desc";

            DataTable dtPermohonan = Utils.GetDataTable(selectData2 + CommandText2, queryParams);

            GridView1.DataSource = dtPermohonan;
            GridView1.DataBind();
            ViewState["dtPermohonan"] = dtPermohonan;
        }


        protected void Senarai_DataBound(object sender, EventArgs e)
        {

            for (int i = 0; i <= Senarai.Rows.Count - 1; i++)
            {
                ImageButton btnhapus = (ImageButton)Senarai.Rows[i].FindControl("btnhapus");
                HyperLink HyperLinkEdit = (HyperLink)Senarai.Rows[i].FindControl("HyperLinkEdit");
                //HyperLink HyperLinkMaju = (HyperLink)Senarai.Rows[i].FindControl("HyperLinkMaju");

                Label lblStatus = (Label)Senarai.Rows[i].FindControl("lblStatus");
                Label lblIDStatus = (Label)Senarai.Rows[i].FindControl("lblIDStatus");

                if (lblIDStatus.Text == "1")
                {
                    lblStatus.CssClass = "badge text-bg-primary";
                    HyperLinkEdit.Visible = true;
                    btnhapus.Visible = true;
                }

                if (lblIDStatus.Text == "2")
                {
                    lblStatus.CssClass = "badge text-bg-warning";
                    HyperLinkEdit.Visible = true;
                    btnhapus.Visible = true;
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
                ListView detailsList = e.Row.FindControl("DetailsList") as ListView;
                HyperLink LinkEditMOF = e.Row.FindControl("LinkEditMOF") as HyperLink;

                if (drv.Row["IdStatusKeputusan"].ToString() == "1")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-info";
                else if (drv.Row["IdStatusKeputusan"].ToString() == "2")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-success";
                else if (drv.Row["IdStatusKeputusan"].ToString() == "5")
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-warning";
                else
                    lblStatus.CssClass = lblStatus.CssClass + " text-bg-danger";

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Label"), new DataColumn("Text") });

                dt.Rows.Add("SYARIKAT DIPERAKU", drv.Row["MOFSyarikatDiperaku"].ToString());
                dt.Rows.Add("NILAI TAWARAN", "RM " + string.Format("{0:#,0.00}", drv.Row["MOFNilaiTawaran"]));
                dt.Rows.Add("TEMPOH", drv.Row["MOFTempoh"].ToString() + " BULAN");
                detailsList.DataSource = dt;
                detailsList.DataBind();
                
                LinkEditMOF.NavigateUrl = "/keputusan/mof.aspx?id=" + drv.Row["Id"] + "&ReturnURL=" + System.Web.HttpUtility.UrlEncode("/keputusan/senarai.aspx");
                    
            }
        }
    }
}