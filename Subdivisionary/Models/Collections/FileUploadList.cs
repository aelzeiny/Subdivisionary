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
    public class FileUploadList : SerializableList<string>
    {
        protected override int ParamCount => 1;

        protected override string ParseObject(string[] param)
        {
            return param[0];
        }

        protected override string[] SerializeObject(string serialize)
        {
            return new[] {serialize};
        }
    }

}