using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/auth/login.aspx", false);
                }
                if (!(User.IsInRole("Administrator") || User.IsInRole("Urusetia") || User.IsInRole("Pengesah")))
                {
                    Response.Redirect("~/keputusan/senarai.aspx", false);
                }
                initData();
            }

        }

        protected void initData()
        {
            if (User.IsInRole("Administrator") || User.IsInRole("Urusetia"))
            {
                Panel2Minggu.Visible = true;
                PanelNextMeeting.Visible = true;
                PanelPertimbangan.Visible = true;
            }
            if (User.IsInRole("Administrator") || User.IsInRole("Pengesah"))
            {
                PanelPenyemak1.Visible = true;
                PanelPenyemak2.Visible = true;
                PanelNextMeeting2.Visible = !(User.IsInRole("Administrator") || User.IsInRole("Urusetia"));
            }
            string CommandText = "Select TOP 1 * from PaparMesyuarat WHERE IdStatusPengesahan=3";
            DataTable dtKeputusanKembali = Utils.GetDataTable(CommandText);

            if (dtKeputusanKembali.Rows.Count > 0 && (User.IsInRole("Administrator") || User.IsInRole("Urusetia")))
            {
                PanelNotify.Visible = true;
                NotifyMsg.Text = "Mesyuarat <a href='/mesyuarat/keputusan.aspx?id="+ dtKeputusanKembali.Rows[0]["Id"].ToString() + "'>"+dtKeputusanKembali.Rows[0]["MESYUARAT"].ToString()+"</a> telah dikembalikan oleh pengesah untuk tindakan anda yang seterusnya.";
            }

            string CommandText1 = "select count(*) as total from Permohonan WHERE TarikhSahlaku >= DATEADD(day,-14, CAST( GETDATE() AS Date ) ) AND TarikhHapus IS NULL AND IdStatusPermohonan != 4";
            DataTable dtSah2Minggu = Utils.GetDataTable(CommandText1);

            if(dtSah2Minggu.Rows.Count > 0 )
            {
                Sah2Minggu.Text = dtSah2Minggu.Rows[0]["total"].ToString();
            }

            string CommandText2 = "select TOP 1 *, DATEDIFF(day, CAST( GETDATE() AS Date ) , Tarikh) AS DayLeft from Mesyuarat WHERE Tarikh >= CAST( GETDATE() AS Date ) AND TarikhHapus IS NULL ORDER BY Tarikh desc";
            DataTable dtNextMeeting = Utils.GetDataTable(CommandText2);

            if (dtNextMeeting.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtNextMeeting.Rows[0]["DayLeft"]) >= 1)
                    NextMeeting.Text = NextMeeting2.Text = dtNextMeeting.Rows[0]["DayLeft"] + " Hari Lagi";
                else
                    NextMeeting.Text = NextMeeting2.Text = "Hari Ini";

                NextMeetingLink.NavigateUrl = NextMeetingLink2.NavigateUrl = "/mesyuarat/papar.aspx?id=" + dtNextMeeting.Rows[0]["Id"];
            }
            else NextMeeting.Text = NextMeeting2.Text = "Tiada";

            string CommandText3 = "select count(*) as total from Permohonan WHERE TarikhHapus IS NULL AND IdStatusPermohonan != 4";
            DataTable dtBelumKeMesyuarat = Utils.GetDataTable(CommandText3);

            if (dtBelumKeMesyuarat.Rows.Count > 0)
            {
                BelumKeMesyuarat.Text = dtBelumKeMesyuarat.Rows[0]["total"].ToString();
            }
            /*
            string CommandText4 = "select count(*) as total from Permohonan WHERE TarikhHapus IS NULL";
            DataTable dtPermohonan = Utils.GetDataTable(CommandText4);

            string CommandText5 = "select count(*) as total from Permohonan P INNER JOIN Mesyuarat M ON P.IdMesyuarat=M.Id WHERE P.TarikhHapus IS NULL AND M.IdStatusPengesahan=4";
            DataTable dtPermohonanDiputuskan = Utils.GetDataTable(CommandText5);

            if (dtPermohonan.Rows.Count > 0 && dtPermohonanDiputuskan.Rows.Count > 0)
            {
                JumlahKeputusan.Text = dtPermohonanDiputuskan.Rows[0]["total"] + "/" + dtPermohonan.Rows[0]["total"];
            }
            */

            string CommandText6 = "Select count(*) as total from Permohonan where IdStatusPermohonan = '1' AND TarikhHapus IS NULL";
            DataTable dtPengesahanPerolehan = Utils.GetDataTable(CommandText6);

            if (dtPengesahanPerolehan.Rows.Count > 0)
            {
                PengesahanPerolehan.Text = dtPengesahanPerolehan.Rows[0]["total"].ToString();
            }

            string CommandText7 = "Select count(*) as total from Mesyuarat WHERE IdStatusPengesahan=2 AND TarikhHapus IS NULL";
            DataTable dtPerakuanMesyuarat = Utils.GetDataTable(CommandText7);

            if (dtPerakuanMesyuarat.Rows.Count > 0)
            {
                PerakuanMesyuarat.Text = dtPerakuanMesyuarat.Rows[0]["total"].ToString();
            }
        }
    }
}