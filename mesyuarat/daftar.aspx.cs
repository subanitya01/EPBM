using EPBM.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace EPBM.mesyuarat
{
    public partial class daftar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initTypeList();
            }
        }

        protected void initTypeList()
        {
            DataTable dtRoles = Utils.GetDataTable("Select * from JenisMesyuarat");
            ddlJenis.Items.Add(new ListItem("SILA PILIH", ""));

            foreach (DataRow row in dtRoles.Rows)
            {
                ListItem item = new ListItem(row["Nama"].ToString(), row["Id"].ToString());
                //item.Value = row["Id"].ToString();
                //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                ddlJenis.Items.Add(item);
            }
        }

        protected void Save(object sender, EventArgs e)
        {
            /*Mesyuarat reg = new Mesyuarat();

            reg.Jenis = ddlJenis.SelectedValue.ToString();
            reg.Bil = txtBil.Text.ToString();
            reg.Tahun = txtTahun.Text.ToString();
            reg.Tarikh = txtTarikh.Text.ToString();

            var context = new ValidationContext(reg, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(reg, context, results, true);*/
            Page.Validate();
            if (Page.IsValid)
            {
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                {
                    {"@Jenis",  ddlJenis.SelectedValue },
                    {"@Bil",  txtBil.Text },
                    {"@Tahun",  txtTahun.Text },
                    {"@Tarikh",  txtTarikh.Text },
                    {"@TarikhCipta", DateTime.Now },
                    {"@DiciptaOleh", Session["Profile.ICNO"] },
                };
                Utils.ExcuteQuery("INSERT INTO Mesyuarat(IdJenisMesyuarat, Bilangan, Tahun, Tarikh, TarikhDicipta, DiciptaOleh) VALUES(@Jenis, @Bil, @Tahun, @Tarikh, @TarikhCipta, @DiciptaOleh)", queryParams);

                Response.Redirect("/mesyuarat/senarai.aspx");
            }
            /*else
            {
                ListItem msg = new ListItem();

                foreach (var validationResult in results)
                {
                    ErrorList.Items.Add(new ListItem(validationResult.ErrorMessage.ToString()));
                }
                errorMsg.Visible = true;
            }*/
        }
    }
}