﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;

namespace EPBM
{
    public static class Utils
    {
        public static DataTable GetDataTable(string query, Dictionary<string, dynamic> queryParams = null, string connectionName = "DefaultConnection")
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                if (queryParams != null)
                {
                    foreach (KeyValuePair<string, dynamic> param in queryParams)
                    {
                        sqlCommand.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();

                return dataTable;
            }
        }

        public static bool ExcuteQuery(string query, Dictionary<string, dynamic> queryParams = null, string connectionName = "DefaultConnection")
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                if (queryParams != null)
                {
                    foreach (KeyValuePair<string, dynamic> param in queryParams)
                    {
                        sqlCommand.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                int rowEffected = sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return rowEffected > 0;
            }
        }
        public static void HttpNotFound()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.StatusCode = 404;
            HttpContext.Current.Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        public static Control FindControlRecursive(Control container, string name)
        {
            if (container.ID == name)
                return container;

            foreach (Control ctrl in container.Controls)
            {
                Control foundCtrl = FindControlRecursive(ctrl, name);

                if (foundCtrl != null)
                    return foundCtrl;
            }
            return null;
        }

        public static string BuildList(string[] item, bool ordered = true)
        {
            var sb = new StringBuilder();

            if (item.Length > 0)
            {
                if(ordered)
                    sb.Append("<ol>");
                else
                    sb.Append("<ul>");

                foreach (var n in item)
                {
                    sb.Append("<li>" + n + "</li>");
                }

                if (ordered)
                    sb.Append("</ol>");
                else
                    sb.Append("</ol>");
            }
            return sb.ToString();
        }
        public static decimal ParseDecimal(string s)
        {
            s = s.Replace(",", "");
            return decimal.Parse(s);
        }
    }
}