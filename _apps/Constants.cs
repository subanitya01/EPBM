using System.Web;

namespace EPBM
{
    public static class Constants
    {
        #region Shared Constants

        public const int RANDOM_FILENAME_CHAR_LENGTH = 6;

        public const string ALLOWED_FILE_TYPES = "jpeg,jpg,png,gif,pdf,doc,docx";
        public const string ASC = "ASC";
        public const string CONTENT_DISPOSITION = "content-disposition";
        public const string CONTENT_TYPE = "Application/octet-stream";
        public const string DESC = "DESC";
        public const string FORMAT_ATTACHMENT_FILE = "attachment; filename={0}";
        public const string FORMAT_DATE = "dd MMM yyyy";
        public const string FREE_LIMITED_KEY = "FREE-LIMITED-KEY";
        public const string READ_ONLY = "readonly";
        public const string SEPARATOR = "; ";

        #endregion

        #region System Path

        public const string RELATIVE_PATH = "~/";

        public const string IMAGE_LOGO = "~/Image/logo_federal.png";
        public const string PATH_LOGIN = "~/login";
        public const string TEMP_FOLDER = "~/temp/";
        
        public static string CURRENT_PATH
        {
            get
            {
                HttpRequest httpRequest = HttpContext.Current.Request;

                return RELATIVE_PATH + httpRequest.Url.AbsolutePath.Remove(0, httpRequest.ApplicationPath.Length);
            }
        }

        public static string LOGO_PATH
        {
            get
            {
                return HttpContext.Current.Server.MapPath(IMAGE_LOGO);
            }
        }

        public static string TEMP_PATH
        {
            get
            {
                return HttpContext.Current.Server.MapPath(TEMP_FOLDER);
            }
        }

        #endregion
    }
}