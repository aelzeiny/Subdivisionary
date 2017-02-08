using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models
{
    public struct FileUploadProperty
    {
        public static int DEFAULT_MAX_COUNT = 10;
        public static int DEFAULT_MAX_SIZE = 10000;
        public static string[] DEFAULT_EXTENSIONS = new string[] {
                            "pdf", "gif", "png", "jpg", "jpeg", "doc", "docx", "xls", "xlsx", "PNG", "GIF", "JPG", "JPEG"};

        [NotMapped]
        public bool IsSingleUpload { get; set; }
        [NotMapped]
        public string StandardName { get; set; }
        [NotMapped]
        public string UniqueKey { get; set; }
        [NotMapped]
        public string FolderPath { get; set; }
        [NotMapped]
        public int FormId { get; set; }
        [NotMapped]
        public int MaxFileSize { get; set; }
        [NotMapped]
        public int MaxFileCount { get;  set; }
        [NotMapped]
        public List<string> Extensions { get; set; }

        public FileUploadProperty(int formId, string uniqueKey, string folderPath, string standardName, bool isSingleUpload=true)
        {
            FormId = formId;
            UniqueKey = uniqueKey;
            StandardName = standardName;
            IsSingleUpload = isSingleUpload;
            FolderPath = folderPath;
            
            MaxFileSize = DEFAULT_MAX_SIZE;
            MaxFileCount = DEFAULT_MAX_COUNT;
            Extensions = new List<string>(DEFAULT_EXTENSIONS.Length);
            Extensions.AddRange(DEFAULT_EXTENSIONS);
        }
    }
}