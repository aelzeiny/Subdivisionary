using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Applications;

namespace Subdivisionary.Helpers
{
    public static class DirectoryHelper
    {

        public static readonly string APPLICATIONS_DIRECTORY = Path.Combine("~", "App_Data", "Application Files");
        public static string GetApplicationsDirectory(this HttpServerUtilityBase server)
        {
            return server.MapPath(APPLICATIONS_DIRECTORY);
        }

        public static string GetApplicationDirectory(this HttpServerUtilityBase server, Application application)
        {
            var dir = Path.Combine(server.GetApplicationsDirectory(), application.DirectoryName);
            EnsureDirectoryExists(dir);
            return dir;
        }

        public static string GetApplicationIdFromFilePath(string filePath)
        {
            string appId = filePath.Substring(APPLICATIONS_DIRECTORY.Length);
            appId = appId.Substring(1, appId.IndexOf('_')-1);
            return appId;
        }



        public static string UnmapPath(this HttpServerUtilityBase server, string fullPath)
        {
            var serverPath = server.MapPath("~/");
            var answer = fullPath.Replace(serverPath, "~/");
            return answer;
        }

        public static bool FilePathExists(this HttpServerUtilityBase server, string fullPath)
        {
            return File.Exists(server.MapPath(fullPath));
        }

        public static string FindUntakenFilename(string directory, string standardName, string ext)
        {
            string basename = Path.Combine(directory, standardName);
            if (!File.Exists(basename + ext))
                return basename + ext;

            for (int i = 1; true; i++)
            {
                string currName = basename + "_" + i + ext;
                if (!System.IO.File.Exists(currName))
                    return currName;
            }
        }

        public static void EnsureDirectoryExists(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }
}