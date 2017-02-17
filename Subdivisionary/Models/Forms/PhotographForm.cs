using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class PhotographForm : UploadableFileForm
    {
        public override string DisplayName => "Photographs";

        public static readonly string PHOTO_FRONT_KEY = "grantFrontId";
        public static readonly string PHOTO_BACK_KEY = "grantBackId";
        public static readonly string PHOTO_LEFT_KEY = "grantLeftId";
        public static readonly string PHOTO_RIGHT_KEY = "grantRightId";
        
        private static readonly string PHOTO_DIRECTORY = "Photographs";
        
        public override FileUploadProperty[] FileUploadProperties =>
        new []
        {
            new FileUploadProperty(this.Id, PHOTO_FRONT_KEY, PHOTO_DIRECTORY, "Front Photo"),
            new FileUploadProperty(this.Id, PHOTO_BACK_KEY, PHOTO_DIRECTORY, "Back Photo"),
            new FileUploadProperty(this.Id, PHOTO_LEFT_KEY, PHOTO_DIRECTORY, "Left Photo"),
            new FileUploadProperty(this.Id, PHOTO_RIGHT_KEY, PHOTO_DIRECTORY, "Right Photo")
        };
    }
}