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
    public abstract class FileUploadList : SerializableList<IFileUploadItem>
    {
    }

    /// <summary>
    /// File Upload Lists are serializable lists that MUST contain the FilePath property
    /// </summary>
    [ComplexType]
    public abstract class FileUploadList<T> : FileUploadList where T : IFileUploadItem
    {
    }

    public interface IFileUploadItem
    {
        string FilePath { get; set; }
    }

}