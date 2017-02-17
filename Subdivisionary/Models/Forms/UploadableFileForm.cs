using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public abstract class UploadableFileForm : Form
    {
        public virtual ICollection<FileUploadInfo> FileUploads { get; set; }

        public abstract FileUploadProperty[] FileUploadProperties { get; }

        public IEnumerable<FileUploadInfo> GetFileUploadList(string key)
        {
            return FileUploads.Where(x => x.FileKey == key);
        }

        public FileUploadProperty GetFileUploadInfo(string key)
        {
            return FileUploadProperties.First(x => x.UniqueKey == key);
        }

        public FileUploadProperty? FindEmptyFileProperty()
        {
            var props = FileUploadProperties;
            for (int i = 0; i < props.Length; i++)
            {
                if (!props[i].IsRequiredUpload)
                    continue;
                var uniqueKey = props[i].UniqueKey;
                bool found = false;
                foreach (var upload in FileUploads)
                {
                    if (upload.FileKey == uniqueKey)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    return props[i];
            }
            return null;
        }

        public bool MaximumFileUploadsExceeded(FileUploadProperty prop)
        {
            int propsCount = 0;
            foreach (var upload in FileUploads)
            {
                if (prop.UniqueKey != upload.FileKey)
                    continue;
                propsCount++;
                if (propsCount >= prop.MaxFileCount)
                    return true;
            }
            return false;
        }
    }
}