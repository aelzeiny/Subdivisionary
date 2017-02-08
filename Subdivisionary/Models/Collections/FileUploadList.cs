using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{

    /// <summary>
    /// Fun fact, in Java we can use Generics like FileUploadList&lt;?&gt;.
    /// In C# we have to make an abstract class. So all FileUploadLists are 
    /// FileUploadList&lt;T&gt; where T inherits from the IFileUploadItem interface
    /// </summary>
    [ComplexType]
    public class FileUploadList : SerializableList<FileUploadInfo>
    {
        protected override int ParamCount => 3;

        protected override FileUploadInfo ParseObject(string[] param)
        {
            return new FileUploadInfo()
            {
                Url = param[0],
                Size = int.Parse(param[1]),
                Type = param[2]
            };
        }

        protected override string[] SerializeObject(FileUploadInfo serialize)
        {
            return new []
            {
                serialize.Url,
                serialize.Size.ToString(),
                serialize.Type
            };
        }
    }

    public class FileUploadInfo
    {
        public string Url { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
    }

}