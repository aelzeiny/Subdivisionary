using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Subdivisionary.Models;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Helpers
{
    public static class FormJavaScriptHelper
    {
        public static MvcHtmlString RenderUploadableFileJavascript(this HtmlHelper helper, IUploadableFileForm form, HttpServerUtilityBase server)
        {
            StringBuilder answer = new StringBuilder();
            var uploads = form.FileUploadProperties();

            foreach (FileUploadProperty upload in uploads)
            {
                answer.AppendLine("$('#" + upload.UniqueKey + "').fileinput({");
                answer.AppendLine("showUpload: false,");
                answer.AppendLine("initialPreviewAsData: true,");
                answer.AppendLine(string.Format("maxFileSize: {0},", upload.MaxFileSize));
                answer.AppendLine(string.Format("maxFileCount: {0},", upload.MaxFileCount));

                // Cannot preview already uploaded files
                /*answer.AppendLine("initialPreview: [");
                RenderList(upload.SavedFiles.Select(x=>x.Substring(1).Replace('\\','/')).ToList(), answer);
                answer.AppendLine("],");*/

                // Render Allowed File Extensions
                answer.AppendLine("allowedFileExtensions: [");
                RenderList(upload.Extensions, answer);
                answer.AppendLine("]");
                answer.AppendLine("});");
            }

            return new MvcHtmlString(answer.ToString());
        }

        private static void RenderList(IList<string> extensionsList, StringBuilder answer)
        {
            for (int k = 0; k < extensionsList.Count; k++)
            {
                answer.Append("'" + extensionsList[k] + "'");
                if (k != extensionsList.Count - 1)
                    answer.Append(',');
            }
        }
    }
}