using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web.Razor.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using GFile = Google.Apis.Drive.v3.Data.File;

namespace Subdivisionary.FileUploaders
{
    public class GoogleDriveUploader : IFileUploader
    {
        private static DriveService _service;

        public Task InitializeRootFolder(string folder)
        {
            throw new NotImplementedException();
        }

        public Task<string> StoreFile(HttpPostedFileBase file, string folderPath, string fileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes Drive Service with Json File
        /// </summary>
        /// <param name="keyFilePath"></param>
        public static void InitializeCredentials(string keyFilePath)
        {
            if (!File.Exists(keyFilePath))
            {
                throw new KeyNotFoundException("No Google Drive Key File found @ designated path: " + keyFilePath);
            }

            GoogleCredential cred;
            using (var reader = new StreamReader(keyFilePath))
            {
                cred = GoogleCredential.FromJson(reader.ReadToEnd());
            }
            // Create the service
            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = cred,
                ApplicationName = "Subdivision Application System"
            });
        }

        private sealed class GDirectory : GFile
        {
            public GDirectory(string name)
            {
                this.Name = name;
                this.MimeType = "application/vnd.google-apps.folder";
            }
        }
    }
}