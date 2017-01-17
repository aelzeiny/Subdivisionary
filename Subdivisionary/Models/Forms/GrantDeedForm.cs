using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Subdivisionary.DAL;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class GrantDeedForm : Form, IUploadableFileForm
    {
        public GrantDeedList List { get; set; }
        public override string DisplayName => "Grant Deeds";

        public BasicFileUploadList GrantPiqFile { get; set; }
        public BasicFileUploadList GrantAdjoinerFiles { get; set; }

        private static readonly string GRANT_PIQ_KEY = "grantPIQId";
        private static readonly string GRANT_ADJOINER_KEY = "grantAdjoinerId";
        private static readonly string GRANT_DIRECTORY = "Grant Deed";

        public GrantDeedForm()
        {
            GrantPiqFile = new BasicFileUploadList();
            GrantAdjoinerFiles = new BasicFileUploadList();
            List = new GrantDeedList();
        }

        public FileUploadProperty[] FileUploadProperties()
        {
            FileUploadProperty[] property = new FileUploadProperty[2]
            {
                new FileUploadProperty(GRANT_PIQ_KEY, GRANT_DIRECTORY, "Deed PIQ"),
                new FileUploadProperty(GRANT_ADJOINER_KEY, GRANT_DIRECTORY, "Deed Adjoiner", false)
            };
            return property;
        }

        public BasicFileUploadList[] FileUploadsLists()
        {
            return new BasicFileUploadList[]
            {
                GrantPiqFile, GrantAdjoinerFiles
            };
        }

        public FileUploadList GetFileUploadList(string key)
        {
            if (key == GRANT_PIQ_KEY)
            {
                return GrantPiqFile;
            }
            else if (key == GRANT_ADJOINER_KEY)
            {
                return GrantAdjoinerFiles;
            }
            return null;
        }

        public void SyncFile(string key, string file)
        {
            if (key == GRANT_PIQ_KEY)
            {
                GrantPiqFile.Clear();
                GrantPiqFile.Add(file);
            }
            else if(key == GRANT_ADJOINER_KEY)
            {
                GrantAdjoinerFiles.Add(file);
            }
        }
    }

    //[ComplexType]
    public class GrantDeedInfo
    {
        public string Block { get; set; }
        public string Lot { get; set; }

        public string ScanPath { get; set; }
    }
}