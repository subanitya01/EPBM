using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EPBM.mesyuarat
{
    public partial class papar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(User.IsInRole("Administrator") || User.IsInRole("Urusetia")))
            {
                Utils.HttpNotFound();
            }
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
                if(string.IsNullOrEmpty(Id) )
                    Utils.HttpNotFound();

                string CommandText = "Select TOP 1 * from PaparMesyuarat WHERE Id=@Id";
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtMesyuarat = Utils.GetDataTable(CommandText, queryParams);

                if (dtMesyuarat.Rows.Count == 0)
                    Utils.HttpNotFound();

                ViewState["dtMesyuarat"] = dtMesyuarat;
                DataRow row = dtMesyuarat.Rows[0];
                PaparJenis.Text = row["JENIS"].ToString();
                PaparBil.Text = row["BILANGAN"].ToString();
                PaparTarikh.Text = row["TARIKHMS"].ToString();
                PaparPengerusi.Text = row["PENGERUSI"].ToString();
                pageTitle.Text = row["JENIS"].ToString() + " BIL. " + row["BILANGAN"].ToString();
                deleteTitle.Text = "MESYUARAT " + row["JENIS"].ToString() + " BIL. " + row["BILANGAN"].ToString();
                lnkDelete.CommandArgument = row["Id"].ToString();
                HyperLink1.NavigateUrl = "/mesyuarat/edit.aspx?id=" + row["Id"].ToString();

                if (new[] { 2, 4 }.Contains(Convert.ToInt32(row["IdStatusPengesahan"])))
                {
                    actionButtons.Visible = false;
                }

                string CommandText2 = "Select * from AhliMesyuarat WHERE IdMesyuarat=@Id";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", row["Id"].ToString() } };
                DataTable dtAhliMesyuarat = Utils.GetDataTable(CommandText2, queryParams2);

                foreach (DataRow rowAhli in dtAhliMesyuarat.Rows)
                {
                    ListItem li = new ListItem();
                    li.Attributes["class"] = "list-group-item";
                    li.Text = rowAhli["Nama"].ToString();
                    ListAhli.Items.Add(li);
                }

                string CommandText3 = "Select * from Permohonan WHERE IdMesyuarat=@Id";
                Dictionary<string, dynamic> queryParams3 = new Dictionary<string, dynamic>() { { "@Id", row["Id"].ToString() } };
                DataTable dtPermohonan = Utils.GetDataTable(CommandText3, queryParams3);

                foreach (DataRow rowPermohonan in dtPermohonan.Rows)
                {
                    ListItem li = new ListItem();
                    li.Attributes["class"] = "list-group-item";
                    li.Text = rowPermohonan["Tajuk"].ToString();
                    ListPermohonan.Items.Add(li);
                }
            }
            catch (Exception) { Utils.HttpNotFound(); }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                {
                    {"@Id",  btn.CommandArgument }
                };
            DataTable dtrslt = ((DataTable)ViewState["dtMesyuarat"]);

            if (Convert.ToInt32(dtrslt.Rows[0]["IdStatusPengesahan"]) != 4)
            {
                Utils.ExcuteQuery("UPDATE Mesyuarat SET TarikhHapus = GETDATE() WHERE Id = @Id", queryParams);

                Utils.ExcuteQuery("UPDATE Permohonan SET " +
                    "IdMesyuarat = NULL, " +
                    "IdStatusKeputusan = NULL, " +
                    "SyarikatBerjaya = NULL, " +
                    "Tempoh = NULL, " +
                    "LampiranKeputusan = NULL, " +
                    "TarikhSuratSetujuTerima = NULL, " +
                    "RujukanSuratSetujuTerima = NULL, " +
                    "AlasanKeputusan = NULL, " +
                    "IdStatusPermohonan=3 " +
                    "WHERE IdMesyuarat = @Id", queryParams);
                Session["flash.success"] = "Mesyuarat " + dtrslt.Rows[0]["JENIS"] + " Bil. " + dtrslt.Rows[0]["BILANGAN"] + " telah dihapuskan!";
                Response.Redirect("/mesyuarat/daftar.aspx");
            }
        }
    }
}