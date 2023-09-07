using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
            /*try
            {*/
                var Id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(Id))
                    Utils.HttpNotFound();

                string CommandText = "Select * from PaparMesyuarat WHERE Id=@Id";
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtMesyuarat = Utils.GetDataTable(CommandText, queryParams);
                TajukMesyuarat.Text = "MESYUARAT " + dtMesyuarat.Rows[0]["JENIS"] + " BIL. " + dtMesyuarat.Rows[0]["BILANGAN"];

                string CommandText2 = "Select Id, Tajuk, CASE WHEN IdJabatan = 1 THEN NamaBahagian ELSE NamaJabatan END as Jabatan, IdStatusKeputusan, StatusKeputusan as STATUS, SyarikatBerjaya, Harga, Tempoh, AlasanKeputusan as KETERANGAN from Papar_Permohonan WHERE IdMesyuarat=@Id and TarikhHapus IS NULL";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", Id } };
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

                if (drv.Row["IdStatusKeputusan"].ToString()=="1")
                    lblStatus.Attributes.Add("class", "text-bg-info");
                else if (drv.Row["IdStatusKeputusan"].ToString() == "1")
                    lblStatus.Attributes.Add("class", "text-bg-success");
                else
                    lblStatus.Attributes.Add("class", "text-bg-danger");

                /*ListView roleList = e.Row.FindControl("RoleList") as ListView;
                Label activeLbl = e.Row.FindControl("lblStatusActive") as Label;
                Label inactiveLbl = e.Row.FindControl("lblStatusInactive") as Label;
                LinkButton btnEditUser = e.Row.FindControl("BtnEditUser") as LinkButton;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("UserId"), new DataColumn("Role") });
                DataRowView drv = e.Row.DataItem as DataRowView;
                DataRow[] rows = drv.Row.GetChildRows("userRoles");
                List<string> roles = new List<string>();

                foreach (DataRow row in rows)
                {
                    dt.Rows.Add(row["UserId"].ToString(), row["Name"].ToString());
                    roles.Add(row["Name"].ToString());
                }
                btnEditUser.Attributes["data-roles"] = JsonConvert.SerializeObject(roles.ToArray());
                roleList.DataSource = dt;
                roleList.DataBind();

                if (drv["Inactive"].ToString() == "1")
                {
                    activeLbl.Visible = false;
                    inactiveLbl.Visible = true;
                }
                else
                {
                    activeLbl.Visible = true;
                    inactiveLbl.Visible = false;
                }*/
            }
        }
    }
}