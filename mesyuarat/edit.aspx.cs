using Microsoft.Ajax.Utilities.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EPBM.mesyuarat
{
    public partial class edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void initTypeList(string selected = null)
        {
            DataTable dtRoles = Utils.GetDataTable("Select * from JenisMesyuarat");
            ddlJenis.Items.Add(new ListItem("SILA PILIH", ""));

            foreach (DataRow row in dtRoles.Rows)
            {
                ListItem item = new ListItem(row["Nama"].ToString(), row["Id"].ToString());
                //item.Value = row["Id"].ToString();
                item.Selected = selected == row["Id"].ToString();
                ddlJenis.Items.Add(item);
            }
        }
        protected void BindData()
        {
            try
            {
                var Id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(Id))
                    Utils.HttpNotFound();

                string CommandText = "Select TOP 1 * from Mesyuarat WHERE Id=@Id and TarikhHapus IS NULL and IdStatusPengesahan<>4";
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>() { { "@Id", Id } };
                DataTable dtMesyuarat = Utils.GetDataTable(CommandText, queryParams);

                if (dtMesyuarat.Rows.Count == 0)
                    Utils.HttpNotFound();

                ViewState["IdMesyuarat"] = Id;
                DataRow row = dtMesyuarat.Rows[0];
                txtTahun.Text = row["Tahun"].ToString();
                txtBil.Text = row["Bilangan"].ToString();
                txtTarikh.Text = ((DateTime)row["Tarikh"]).ToString("yyyy-MM-dd");
                txtPengerusi.Text = row["Pengerusi"].ToString();
                btnSubmit.CommandArgument = row["Id"].ToString();
                initTypeList(row["IdJenisMesyuarat"].ToString());

                string CommandText2 = "Select * from AhliMesyuarat WHERE IdMesyuarat=@Id";
                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@Id", row["Id"].ToString() } };
                DataTable dtAhliMesyuarat = Utils.GetDataTable(CommandText2, queryParams2);
                Repeater1.DataSource = dtAhliMesyuarat;
                ViewState["dtAhliMesyuarat"] = dtAhliMesyuarat;
                Repeater1.DataBind();

                string CommandText3 = "Select Id, Tajuk, IdMesyuarat from Permohonan WHERE (IdMesyuarat=@Id or IdMesyuarat IS NULL) and (IdStatusPermohonan IN (3,4)) and TarikhHapus IS NULL";
                Dictionary<string, dynamic> queryParams3 = new Dictionary<string, dynamic>() { { "@Id", row["Id"].ToString() } };
                DataTable dtPermohonan = Utils.GetDataTable(CommandText3, queryParams3);
                Repeater2.DataSource = dtPermohonan;
                ViewState["dtPermohonan"] = dtPermohonan;
                Repeater2.DataBind();

                /*foreach (DataRow rowPermohonan in dtPermohonan.Rows)
                {
                    ListItem li = new ListItem();
                    li.Attributes["class"] = "list-group-item";
                    li.Text = rowPermohonan["Tajuk"].ToString();
                    ListPermohonan.Items.Add(li);
                }*/
            }
            catch (Exception) { Utils.HttpNotFound(); }
        }

        protected void Save(object sender, EventArgs e)
        {
            Page.Validate("submit");
            if (Page.IsValid)
            {
                LinkButton btn = (LinkButton)sender;
                Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>()
                {
                    {"@Id",  btn.CommandArgument },
                    {"@Jenis",  ddlJenis.SelectedValue },
                    {"@Bil",  txtBil.Text },
                    {"@Tahun",  txtTahun.Text },
                    {"@Tarikh",  txtTarikh.Text },
                    {"@Pengerusi",  txtPengerusi.Text },
                };
                Utils.ExcuteQuery("UPDATE Mesyuarat SET IdJenisMesyuarat=@Jenis, Bilangan=@Bil, Tahun=@Tahun, Tarikh=@Tarikh, Pengerusi=@Pengerusi WHERE Id=@Id", queryParams);


                Dictionary<string, dynamic> queryParams2 = new Dictionary<string, dynamic>() { { "@IdMesyuarat", btn.CommandArgument } };
                //Delete Existing Ahli Mesyuarat
                Utils.ExcuteQuery("DELETE FROM AhliMesyuarat where IdMesyuarat=@IdMesyuarat", queryParams2);

                DataTable dtAhliMesyuarat = (DataTable)ViewState["dtAhliMesyuarat"];
                Int32 batchSize = 0; //how many rows we have build up so far
                Int32 p = 1; //the current paramter name (i.e. "@p1") we're going to use
                StringBuilder insertAhliValues = new StringBuilder();
                Dictionary<string, dynamic> queryParams3 = new Dictionary<string, dynamic>() { { "@IdMesyuarat", btn.CommandArgument } };

                foreach (DataRow entry in dtAhliMesyuarat.Rows)
                {
                    //Build the names of the parameters
                    String pName = String.Format("@p{0}", p + 1); //the "Name" parameter name (i.e. "p1")
                    p += 1;

                    //Build a single "(IdMesyuarat, p1)" row
                    String row = String.Format("(@IdMesyuarat, {0})", pName); //a single values tuple

                    //Add the row to our running SQL batch
                    if (batchSize > 0)
                        insertAhliValues.AppendLine(",");
                    insertAhliValues.Append(row);
                    batchSize += 1;

                    //Add the parameter values for this row
                    queryParams3.Add(pName, entry["Nama"].ToString());
                }

                //Insert Ahli Mesyuarat if any
                if (batchSize > 0)
                {
                    Utils.ExcuteQuery("INSERT INTO AhliMesyuarat (IdMesyuarat, Nama) VALUES" + insertAhliValues.ToString(), queryParams3);
                }

                Dictionary<string, dynamic> queryParams4 = new Dictionary<string, dynamic>() { { "@IdMesyuarat", btn.CommandArgument } };
                Dictionary<string, dynamic> queryParams5 = new Dictionary<string, dynamic>() { { "@IdMesyuarat", btn.CommandArgument } };
                StringBuilder uncheckPermohonanId = new StringBuilder();
                StringBuilder updatePermohonanId = new StringBuilder();
                Int32 count = 0;
                Int32 p1 = 1;
                Int32 p2 = 1;
                DataTable dtPermohonan = (DataTable)ViewState["dtPermohonan"];
                Int32[] existingPermohonanIds = new int[dtPermohonan.Rows.Count];

                foreach (DataRow data in dtPermohonan.Rows)
                {
                    existingPermohonanIds[count] = Convert.ToInt32(data["Id"]);
                    count++;
                }

                foreach (RepeaterItem item in Repeater2.Items)
                {
                    CheckBox CheckBoxPermohonan = (CheckBox)item.FindControl("CheckBoxPermohonan");
                    if (!CheckBoxPermohonan.Checked && existingPermohonanIds.Contains(Convert.ToInt32(dtPermohonan.Rows[item.ItemIndex]["Id"])))
                    {
                        String pName = String.Format("@p{0}", p1 + 1);
                        if (p1 > 1)
                            uncheckPermohonanId.AppendLine(",");
                        uncheckPermohonanId.Append(pName);
                        queryParams4.Add(pName, dtPermohonan.Rows[item.ItemIndex]["Id"]);
                        p1 += 1;
                    }
                    else if (CheckBoxPermohonan.Checked)
                    {
                        String pName = String.Format("@p{0}", p2 + 1);
                        if (p2 > 1)
                            updatePermohonanId.AppendLine(",");
                        updatePermohonanId.Append(pName);
                        queryParams5.Add(pName, dtPermohonan.Rows[item.ItemIndex]["Id"]);
                        p2 += 1;
                    }
                }

                if (p1 > 1) //Delete Existing Permohonan
                    Utils.ExcuteQuery("UPDATE Permohonan SET " +
                        "IdMesyuarat=NULL, " +
                        "IdStatusPermohonan=3, " +
                        "IdStatusKeputusan=NULL, " +
                        "SyarikatBerjaya=NULL, " +
                        "Tempoh=NULL, " +
                        "LampiranKeputusan=NULL, " +
                        "TarikhSuratSetujuTerima=NULL, " +
                        "RujukanSuratSetujuTerima=NULL, " +
                        "AlasanKeputusan=NULL " +
                        "where IdMesyuarat=@IdMesyuarat and Id in (" + uncheckPermohonanId.ToString() + ")", queryParams4);

                if (p2 > 1)
                {
                    Utils.ExcuteQuery("UPDATE Permohonan SET IdMesyuarat=@IdMesyuarat, IdStatusPermohonan=4 WHERE Id IN (" + updatePermohonanId.ToString() + ")", queryParams5);
                }

                Response.Redirect("/mesyuarat/papar.aspx?id=" + btn.CommandArgument);
            }
        }
        protected void removeMember_Click(object sender, EventArgs e)
        {
            DataTable dtAhliMesyuarat = (DataTable)ViewState["dtAhliMesyuarat"];
            LinkButton btn = (LinkButton)sender;
            RepeaterItem gRow = (RepeaterItem)btn.NamingContainer;
            dtAhliMesyuarat.Rows[gRow.ItemIndex].Delete();
            dtAhliMesyuarat.AcceptChanges();
            Repeater1.DataSource = dtAhliMesyuarat;
            Repeater1.DataBind();
            ViewState["dtAhliMesyuarat"] = dtAhliMesyuarat;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "refreshIcon", "feather.replace()", true);
            //Debug.Print("row index click = " + gRow.ItemIndex);
            //Debug.Print("PK passed by button = " + btn.CommandArgument);
        }
        protected void updateMember(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            RepeaterItem gRow = (RepeaterItem)txt.NamingContainer;
            DataTable dtAhliMesyuarat = (DataTable)ViewState["dtAhliMesyuarat"];
            dtAhliMesyuarat.Rows[gRow.ItemIndex]["Nama"] = txt.Text;
            Repeater1.DataSource = dtAhliMesyuarat;
            Repeater1.DataBind();
            ViewState["dtAhliMesyuarat"] = dtAhliMesyuarat;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "refreshIcon", "feather.replace()", true);
        }
        protected void addMember_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newMember.Text))
            {
                DataTable dtAhliMesyuarat = (DataTable)ViewState["dtAhliMesyuarat"];

                if (dtAhliMesyuarat.Rows.Count < 50) 
                { 
                    DataRow newRow = dtAhliMesyuarat.NewRow();
                    newRow["Nama"] = newMember.Text;
                    newRow["IdMesyuarat"] = ViewState["IdMesyuarat"];
                    dtAhliMesyuarat.Rows.Add(newRow);
                    Repeater1.DataSource = dtAhliMesyuarat;
                    Repeater1.DataBind();
                    ViewState["dtAhliMesyuarat"] = dtAhliMesyuarat;
                    newMember.Text = "";
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "refreshIcon", "feather.replace()", true);
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            /*RepeaterItem ri = e.Item;
            var dataItem = ri.DataItem as;
            var isDefaultCheckBox = ri.FindControl("CheckBoxPermohonan") as CheckBox;
            isDefaultCheckBox.Checked = dataItem.IsDefault;*/
            ((CheckBox)e.Item.FindControl("CheckBoxPermohonan")).InputAttributes.Add("class", "form-check-input me-2");
            ((CheckBox)e.Item.FindControl("CheckBoxPermohonan")).LabelAttributes.Add("class", "form-check-label");
        }
    }
}