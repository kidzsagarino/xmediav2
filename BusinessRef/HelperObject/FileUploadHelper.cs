using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessRef.HelperObject
{
    public class FileUploadHelper
    {
        public static bool File(HttpPostedFileBase file, string filePath)
        {
            HttpPostedFileBase httpFile = file;
            //var filePath = config.FilePath;

            if (httpFile.ContentLength > 5000000)
            {
                return false;
            }

            try
            {
                httpFile.SaveAs(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
