using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Subdivisionary.Helpers;
using Subdivisionary.Models.Applications;
using Microsoft.Ajax.Utilities;

namespace Subdivisionary.FileUploaders
{
    public interface IFileUploader
    {
        Task InitializeRootFolder(string folder);
        Task<string> StoreFile(HttpPostedFileBase file, string folderPath, string fileName);
    }
}
