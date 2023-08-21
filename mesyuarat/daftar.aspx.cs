using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPBM.mesyuarat
{
    public partial class daftar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initTypeList();
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
    }
}