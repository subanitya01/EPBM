using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

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
    }
}