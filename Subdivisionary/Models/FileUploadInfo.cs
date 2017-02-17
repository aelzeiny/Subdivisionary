using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Models
{
    public class FileUploadInfo
    {
        public int Id { get; set; }

        public int FormId { get; set; }
        public UploadableFileForm Form { get; set; }

        public string Url { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public string FileKey { get; set; }
    }
}