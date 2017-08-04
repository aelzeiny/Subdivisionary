using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class SignedDcpMotionForm : UploadableFileForm
    {
        public override string DisplayName => "Signed DCP Motion";

        public static readonly string DCP_MOTION_KEY = "dcpMotionKey";

        [Required]
        [Display(Name = "Motion #")]
        public string MotionNo { get; set; }

        [Required]
        [Display(Name = "File #")]
        public string FileNo { get; set; }

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, DCP_MOTION_KEY, "", "DCP Motion") 
        };
    }
}