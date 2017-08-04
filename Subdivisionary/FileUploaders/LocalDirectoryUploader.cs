using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Subdivisionary.FileUploaders
{
    public class LocalDirectoryUploader : IFileUploader
    {
        private static string ROOT_DIR = "~/App_Data/UploadedFiles";
        private HttpServerUtilityBase _server;
        private string _directory;
        public LocalDirectoryUploader(HttpServerUtilityBase server)
        {
            _server = server;
        }
        public async Task InitializeRootFolder(string folder)
        {
            var directory = Path.Combine(_server.MapPath(ROOT_DIR), folder);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            _directory = directory;
        }

        public async Task<string> StoreFile(HttpPostedFileBase file, string folderPath, string fileName)
        {
            var dirPath = folderPath.IsNullOrWhiteSpace() ? _directory : Path.Combine(_directory, folderPath);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            // Upload File to FileSystem 
            string finalName = Path.Combine(dirPath, fileName);
            file.SaveAs(finalName);
            return finalName;
        }
    }
}