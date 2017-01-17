using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class BasicFileUploadList : FileUploadList<BasicFileUploadItem>
    {
        protected override int ParamCount => 1;

        protected override IFileUploadItem ParseObject(string[] param)
        {
            return new BasicFileUploadItem(param[0]);
        }

        protected override string[] SerializeObject(IFileUploadItem serialize)
        {
            return new[] { ((BasicFileUploadItem)serialize).FilePath };
        }

        public void Add(string filePath)
        {
            data.Add(new BasicFileUploadItem(filePath));
        }
    }

    public class BasicFileUploadItem : IFileUploadItem
    {
        public string FilePath { get; set; }

        public BasicFileUploadItem()
        {
        }

        public BasicFileUploadItem(string filePath)
        {
            FilePath = filePath;
        }
    }
}