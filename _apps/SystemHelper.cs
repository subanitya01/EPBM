using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;

namespace EPBM
{
    public class SystemHelper
    {
        private const string SQL_DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public static DataTable InitializeDataTable(string[] dataFields)
        {
            DataTable dataTable = new DataTable();

            foreach (string dataField in dataFields) dataTable.Columns.Add(dataField);

            return dataTable;
        }

        public static DateTime GetCalendarDate(string value)
        {
            DateTime dateTime = new DateTime();
            return DateTime.TryParseExact(value, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime) ? new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0) : DateTime.MinValue;
        }

        public static DateTime GetDate(string value)
        {
            DateTime dateTime = new DateTime();

            return (DateTime.TryParse(value, out dateTime)) ? new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0) : DateTime.MinValue;
        }

        public static DateTime GetDateTime(string value)
        {
            DateTime dateTime = new DateTime();

            return DateTime.TryParse(value, out dateTime) ? dateTime : DateTime.MinValue;
        }

        public static bool GetBoolean(string value)
        {
            bool current = false;

            return bool.TryParse(value, out current) ? current : false;
        }

        public static bool IsValidDateTime(DateTime value)
        {
            return !value.Equals(DateTime.MinValue);
        }

        public static byte[] GetFileByteArray(string filePath)
        {
            try
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                byte[] byteArray = new byte[fileStream.Length];

                fileStream.Read(byteArray, 0, System.Convert.ToInt32(fileStream.Length));
                fileStream.Close();

                return byteArray;
            }
            catch
            {
                return new byte[] { };
            }
        }

        public static double GetDouble(string amount)
        {
            double current = 0;

            return double.TryParse(amount, out current) ? current : 0;
        }

        public static int GetInteger(string value)
        {
            int current = 0;

            return int.TryParse(value, out current) ? current : 0;
        }

        public static string CastToSQLDateTime(DateTime input)
        {
            return (input != null) ? input.ToString(SQL_DATETIME_FORMAT) : String.Empty;
        }

        public static string CheckFileName(string fileName)
        {
            return fileName.Replace(" ", "_").Replace("(", "_").Replace(")", "_");
        }

        public static string GetAbsoluteUrl(string relativeUrl)
        {
            relativeUrl = relativeUrl.Replace("~/", string.Empty);
            string[] splits = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Split('/');
            if (splits.Length >= 2)
            {
                string url = splits[0] + "//";
                for (int i = 2; i < splits.Length - 1; i++)
                {
                    url += splits[i];
                    url += "/";
                }

                return url + relativeUrl;
            }
            return relativeUrl;
        }


        public static void SelectedIndex(DropDownList dropDownList, int index)
        {
            dropDownList.ClearSelection();

            try
            {
                dropDownList.SelectedIndex = index;
            }
            catch
            {
                dropDownList.ClearSelection();
            }
        }

        public static void SelectedIndex(RadioButtonList radioButtonList, int index)
        {
            radioButtonList.ClearSelection();

            try
            {
                radioButtonList.SelectedIndex = index;
            }
            catch
            {
                radioButtonList.ClearSelection();
            }
        }

        public static void SelectedText(DropDownList dropDownList, string text)
        {
            dropDownList.ClearSelection();

            try
            {
                dropDownList.Items.FindByText(text).Selected = true;
            }
            catch
            {
                dropDownList.ClearSelection();
            }
        }

        public static void SelectedText(RadioButtonList radioButtonList, string text)
        {
            radioButtonList.ClearSelection();

            try
            {
                radioButtonList.Items.FindByText(text).Selected = true;
            }
            catch
            {
                radioButtonList.ClearSelection();
            }
        }

        public static void SelectedValue(DropDownList dropDownList, string value)
        {
            dropDownList.ClearSelection();

            try
            {
                dropDownList.Items.FindByValue(value).Selected = true;
            }
            catch
            {
                dropDownList.ClearSelection();
            }
        }
    }
}