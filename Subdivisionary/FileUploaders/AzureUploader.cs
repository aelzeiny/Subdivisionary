using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Subdivisionary.Helpers;

namespace Subdivisionary.FileUploaders
{

    public class AzureUploader : IFileUploader
    {
        private static readonly string CONNECTION_ACCOUNT = CloudConfigurationManager.GetSetting("StorageAccountName");
        private static readonly string CONNECTION_KEY = CloudConfigurationManager.GetSetting("StorageAccountKey");

        private CloudBlobContainer cloudBlobContainer;

        /// <summary>
        /// Container Owners for Azure Blob cannot defy the naming conventions.
        /// (alphanumeric, all lowercase, & dashes only)
        /// </summary>
        /// <param name="application"></param>
        /// <returns>Async task that can be awaited</returns>
        public async Task InitializeRootFolder(string folder)
        {
            // Make folder abide by ContainerRuleConvention
            folder = ContainerRuleConvention(folder);

            // Get Cloud Storage Account, then blobclient, then container
            CloudStorageAccount cloudStorageAccount = CloudHelper.GetConnectionString();
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            cloudBlobContainer = cloudBlobClient.GetContainerReference(folder);
            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }
        }

        public async Task<string> StoreFile(HttpPostedFileBase file, string folderPath, string fileName)
        {
            // Combine folderPath & filename to get cloudBlockBlob
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(System.IO.Path.Combine(folderPath, fileName));

            // Upload File to Azure Blob
            cloudBlockBlob.Properties.ContentType = file.ContentType;
            await cloudBlockBlob.UploadFromStreamAsync(file.InputStream);
            return cloudBlockBlob.Uri.AbsoluteUri;
        }

        private string ContainerRuleConvention(string desiredName)
        {
            return Regex.Replace(desiredName, @"[^A-Za-z0-9-]+", "").ToLower();
        }


        public CloudStorageAccount GetConnectionString()
        {
            string connectionString =
                $"DefaultEndpointsProtocol=https;AccountName={CONNECTION_ACCOUNT};AccountKey={CONNECTION_KEY}";
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}