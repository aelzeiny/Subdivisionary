using System.Collections.Generic;
using System.Linq;
using Subdivisionary.Models;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// This call is a helper for the FileUploadViewModel. It does all the filtering 
    /// from all the Form File Uploads to generate FileUploadViewModels. This is intended for
    /// use with forms that may have more than one file upload property.
    /// </summary>
    public class FileUploadViewModelCollection
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private FileUploadProperty[] _properties;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private IEnumerable<FileUploadInfo>[] _files;
        /// <summary>
        /// Constructor takes the form and generates all the fileuploadviewmodels for that form
        /// </summary>
        /// <param name="form"></param>
        public FileUploadViewModelCollection(UploadableFileForm form)
        {
            _properties = form.FileUploadProperties;
            _files = new IEnumerable<FileUploadInfo>[_properties.Length];
            for (int i = 0; i < _properties.Length; i++)
            {
                var prop = _properties[i];
                _files[i] = form.FileUploads.Where(x => x.FileKey == prop.UniqueKey);
            }
        }
        /// <summary>
        /// Generate A FileUploadViewModel with this helper class.
        /// </summary>
        /// <param name="key">File Property Key</param>
        /// <param name="label">Label for the file upload View</param>
        /// <returns></returns>
        public FileUploadViewModel GetViewModel(string key, string label)
        {
            for(int i=0;i<_properties.Length;i++)
                if (_properties[i].UniqueKey == key)
                    return new FileUploadViewModel() {UploadList = _files[i], UploadProperty = _properties[i], LabelMessage = label};
            return null;
        }
    }
}