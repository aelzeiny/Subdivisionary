using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Subdivisionary.Models.Applications;

namespace Subdivisionary.Helpers
{


    public static class CloudHelper
    {
        private static readonly string CONNECTION_ACCOUNT = CloudConfigurationManager.GetSetting("StorageAccountName");
        private static readonly string CONNECTION_KEY = CloudConfigurationManager.GetSetting("StorageAccountKey");

        /// <summary>
        /// Container Names for Azure Blob cannot defy the naming conventions.
        /// (alphanumeric, all lowercase, & dashes only)
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public static string GetContainerName(Application application)
        {
            return Regex.Replace($"{application.Id}-{application.DisplayName}", @"[^A-Za-z0-9-]+", "").ToLower();
        }
        public static CloudStorageAccount GetConnectionString()
        {
            string connectionString =
                $"DefaultEndpointsProtocol=https;AccountName={CONNECTION_ACCOUNT};AccountKey={CONNECTION_KEY}";
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}