using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class PhotographForm : Form, IUploadableFileForm
    {
        public override string DisplayName => "Photographs";

        [FileUploadRequired]
        public FileUploadList PhotoLeft { get; set; }
        [FileUploadRequired]
        public FileUploadList PhotoRight { get; set; }
        [FileUploadRequired]
        public FileUploadList PhotoFront { get; set; }
        [FileUploadRequired]
        public FileUploadList PhotoBack { get; set; }

        private static readonly string PHOTO_FRONT_KEY = "grantFrontId";
        private static readonly string PHOTO_BACK_KEY = "grantBackId";
        private static readonly string PHOTO_LEFT_KEY = "grantLeftId";
        private static readonly string PHOTO_RIGHT_KEY = "grantRightId";
        
        private static readonly string PHOTO_DIRECTORY = "Photographs";

        public PhotographForm()
        {
            PhotoLeft = new FileUploadList();
            PhotoRight = new FileUploadList();
            PhotoFront = new FileUploadList();
            PhotoBack = new FileUploadList();
        }

        public FileUploadProperty[] FileUploadProperties()
        {
            FileUploadProperty[] property = new FileUploadProperty[4]
            {
                new FileUploadProperty(PHOTO_FRONT_KEY, PHOTO_DIRECTORY, "Front Photo"),
                new FileUploadProperty(PHOTO_BACK_KEY, PHOTO_DIRECTORY, "Back Photo"),
                new FileUploadProperty(PHOTO_LEFT_KEY, PHOTO_DIRECTORY, "Left Photo"),
                new FileUploadProperty(PHOTO_RIGHT_KEY, PHOTO_DIRECTORY, "Right Photo")
            };
            return property;
        }

        public FileUploadList GetFileUploadList(string key)
        {
            if (key == PHOTO_FRONT_KEY)
                return PhotoFront;
            if (key == PHOTO_BACK_KEY)
                return PhotoBack;
            if (key == PHOTO_LEFT_KEY)
                return PhotoLeft;
            if (key == PHOTO_RIGHT_KEY)
                return PhotoRight;
            return null;
        }

        public void SyncFile(string key, string file)
        {
            FileUploadList current = GetFileUploadList(key);
            current.Clear();
            current.Add(file);
        }
    }
}