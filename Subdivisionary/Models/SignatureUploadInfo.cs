using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Models
{
    public class SignatureUploadInfo
    {
        public int Id { get; set; }

        public SignatureForm SignatureForm { get; set; }
        public int SignatureFormId { get; set; }

        public DateTime DateStamp { get; set; }
        public string UserStamp { get; set; }

        public string SignerName { get; set; }

        public string DataFormat { get; set; }
        public string Data { get; set; }

        [Required]
        public bool IsSignatureFinalized { get; set; }
    }
}