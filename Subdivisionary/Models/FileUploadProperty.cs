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
        public static readonly int DEFAULT_MAX_COUNT = 10;
        public static readonly int DEFAULT_MAX_SIZE = 10000;
        public static readonly string[] DEFAULT_EXTENSIONS = {"pdf", "gif", "png", "jpg", "jpeg", "doc", "docx", "xls", "xlsx", "PNG", "GIF", "JPG", "JPEG"};
        
        public bool IsSingleUpload { get; set; }
        public bool IsRequiredUpload { get; set; }
        public string StandardName { get; set; }
        public string UniqueKey { get; set; }
        public string FolderPath { get; set; }
        public int FormId { get; set; }
        public int MaxFileSize { get; set; }
        public int MaxFileCount { get;  set; }
        public List<string> Extensions { get; set; }

        public FileUploadProperty(int formId, string uniqueKey, string folderPath, string standardName,
            bool isSingleUpload=true, bool isRequiredUpload = true)
        {
            FormId = formId;
            UniqueKey = uniqueKey;
            StandardName = standardName;
            IsSingleUpload = isSingleUpload;
            IsRequiredUpload = isRequiredUpload;
            FolderPath = folderPath;
            
            MaxFileSize = DEFAULT_MAX_SIZE;
            MaxFileCount = DEFAULT_MAX_COUNT;
            Extensions = new List<string>(DEFAULT_EXTENSIONS.Length);
            Extensions.AddRange(DEFAULT_EXTENSIONS);
        }
    }
}