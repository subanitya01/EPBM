using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Security;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using Newtonsoft.Json;
namespace EPBM.Permohonan
{
    public partial class senarai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            
            {
                Load_GridData();
          

            }
 
        }

        //private void Load_GridData()
        //{

        //    DataTable EpbmDT = Utils.GetDataTable("Select * from Papar_Permohonan where IdStatusPermohonan IN ('1','2','3')");
        //    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        //    conn.Open();

        //    SqlDataAdapter Sqa = new SqlDataAdapter("select * FROM Papar_Permohonan where IdStatusPermohonan IN ('1','2','3') ", conn);
        //    DataSet ds = new DataSet();
        //    Sqa.Fill(ds);

        //    Senarai.DataSource = ds;
        //    Senarai.DataBind();
        //    conn.Close();

        //}

        private void Load_GridData()
        {
            string searchTerm = Convert.ToString(ViewState["txtSearch"]) ?? null;
            string searchCol = Convert.ToString(ViewState["listSearchCol"]) ?? null;

            DataTable EpbmDT = Utils.GetDataTable("Select * from Papar_Permohonan where IdStatusPermohonan IN ('1','2','3')");

            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>();
            string CommandText = "Select * from Papar_Permohonan where IdStatusPermohonan IN ('1','2','3')";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (string.IsNullOrEmpty(searchCol))
                {
                    CommandText += " AND (NamaJabatan LIKE '%' + @searchTerm + '%' OR Tajuk LIKE '%' + @searchTerm + '%' OR Status_Permohonan LIKE '%' + @searchTerm + '%')";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "JABATAN")
                {
                    CommandText += " AND NamaJabatan LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "TAJUK")
                {
                    CommandText += " AND Tajuk LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
                else if (searchCol == "STATUS")
                {
                    CommandText += " AND Status_Permohonan LIKE '%' + @searchTerm + '%'";
                    queryParams.Add("@searchTerm", searchTerm);
                }
            }

            DataTable dtProfile = Utils.GetDataTable(CommandText, queryParams, "DefaultConnection");
            dtProfile.TableName = "Papar_Permohonan";

            DataSet ds = new DataSet();
            ds.Tables.Add(dtProfile);

            Senarai.DataSource = ds;
            Senarai.DataBind();
            ViewState["dtProfile"] = dtProfile;
        }

        protected void Senarai_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Senarai.PageIndex = e.NewPageIndex;
            Load_GridData();
        }


        protected void btn_hapus_Click(object sender, System.EventArgs e)
        {
        
            GridViewRow gr = (GridViewRow)(((ImageButton)sender).Parent.Parent);
        
            string sid = ((Label)gr.FindControl("lblID")).Text.ToString();
            string tajuk = ((Label)gr.FindControl("lblTajukUtama")).Text.ToString();
            lblTajukUtama.Text = tajuk;
            ID.Text = sid;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "opendeleteModal();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "#deleteModal", "$('#deleteModal').modal('show')", true);

        }

        protected void btnSahHapus_Click(object sender, System.EventArgs e)
        {
          
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Permohonan WHERE ID ='" + ID.Text + "'", conn);

            conn.Open();
            string insertQuery = "DELETE From Permohonan where ID ='" + ID.Text + "'";
            SqlCommand com = new SqlCommand(insertQuery, conn);
            com.ExecuteNonQuery();

            Response.Redirect("/Permohonan/Senarai.aspx");
            conn.Close();

        }


        protected void Senarai_DataBound(object sender, EventArgs e)
        {

            for (int i = 0; i <= Senarai.Rows.Count - 1; i++)
            {
                ImageButton btnhapus = (ImageButton)Senarai.Rows[i].FindControl("btnhapus");
                HyperLink HyperLinkEdit = (HyperLink)Senarai.Rows[i].FindControl("HyperLinkEdit");
                HyperLink HyperLinkMaju = (HyperLink)Senarai.Rows[i].FindControl("HyperLinkMaju");

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
                    HyperLinkMaju.Visible = true;
                  
                }                

            }        
        
        }

        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Permohonan/Senarai.aspx");
          
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Senarai.PageIndex = 0;
            //Panel2.Visible = true;

            ViewState["txtSearch"] = txtSearch.Text.Trim();
            ViewState["listSearchCol"] = listSearchCol.Text.Trim();
            Load_GridData();
        }

        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection == "ASC" ? "&uarr;" : "&darr;";
        }

        protected void Senarai_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dtProfile"];
            if (dtrslt.Rows.Count > 0)
            {
                lblSortRecord.Text = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                dtrslt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"];
                Senarai.DataSource = dtrslt;
                Senarai.DataBind();
            }
        }

    }
}