using System.Configuration;
using System.Web;
using System.Web.Configuration;

namespace EPBM
{
    public class ServiceUtility
    {
        #region Constant and global object declarations

        private static string currentApplicationVirtualPath = HttpContext.Current.Request.ApplicationPath;
        private static Configuration configuration = WebConfigurationManager.OpenWebConfiguration(currentApplicationVirtualPath);
        private static KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;
        
        private const string CONFIG_GEMBOX_EXCEL_LICENCE = "GemBoxExcelLicense";
        private const string CONFIG_GEMBOX_WORD_LICENCE = "GemBoxWordLicense";

        #endregion

        public static string GetgGemBoxExcelLicense()
        {
            try
            {
                return settings[CONFIG_GEMBOX_EXCEL_LICENCE].Value;
            }
            catch
            {
                return null;
            }
        }

        public static string GetgGemBoxWordLicense()
        {
            try
            {
                return settings[CONFIG_GEMBOX_WORD_LICENCE].Value;
            }
            catch
            {
                return null;
            }
        }
    }
}