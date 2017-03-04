using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class DbiInspectionRequirementsForm : UploadableFileForm
    {
        public override string DisplayName => "DBI Inspection Requirements";

        [RequiredIfNoFloorPlansUpload]
        [Display(Name = "Area of Wall(s)")]
        public float? AreaOfWalls { get; set; }

        [RequiredIfNoFloorPlansUpload]
        [Display(Name = "Area of Opening(s)")]
        public float? AreaOfOpenings { get; set; }

        [RequiredIfNoFloorPlansUpload]
        [Display(Name = "Construction Material")]
        public string ConstructionMaterial { get; set; }

        [Display(Name = "Other Information")]
        public string Other { get; set; }

        public static readonly string DBI_INSPECTION_FLOORPLANS_KEY = "dbiFloorplansId";
        public static readonly string DBI_INSPECTION_PHOTOS_KEY = "dbiPhotosId";
        public static readonly string DBI_INSPECTION_DIRECTORY = "DBI Directory";
        public static readonly string DBI_INSPECTION_FEE = "$375.00";

        public override FileUploadProperty[] FileUploadProperties => new []
        {
            new FileUploadProperty(this.Id, DBI_INSPECTION_FLOORPLANS_KEY, DBI_INSPECTION_DIRECTORY, "Architectural Floor Plans", true, false), 
            new FileUploadProperty(this.Id, DBI_INSPECTION_PHOTOS_KEY, DBI_INSPECTION_DIRECTORY, "Architectural Floor Plans", false, true), 
        };
    }

    public class RequiredIfNoFloorPlansUpload : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var me = (DbiInspectionRequirementsForm) validationContext.ObjectInstance;
            if(me.FileUploads.Count(x=>x.FileKey == DbiInspectionRequirementsForm.DBI_INSPECTION_FLOORPLANS_KEY) < 0)
                return new ValidationResult("This field is required if no floorplans are provided");
            return base.IsValid(value, validationContext);
        }
    }
}