using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Subdivisionary.Models;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// View Model For Kartic's File Uplaod Collection.
    /// This allows old uploads to be displayed in the same upload
    /// property along with new uploads.
    /// </summary>
    public class FileUploadViewModel
    {
        /// <summary>
        /// Label of the File Upload Library View
        /// </summary>
        public string LabelMessage { get; set; }

        /// <summary>
        /// Upload Property of the File Upload Library View
        /// </summary>
        public FileUploadProperty UploadProperty { get; set; }

        /// <summary>
        /// Current list of all the file uploads that should be displayed in this view.
        /// This may require some sective filtering if the form contains more than
        /// one file upload.
        /// </summary>
        public IEnumerable<FileUploadInfo> UploadList { get; set; }

        /// <summary>
        /// Create Intial Previews for the Bootstrap Dialog View
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string InitialPreviews(UrlHelper url)
        {
            FileUploadJsonViewModel vm = FileUploadJsonViewModel.Create();
            foreach (var file in UploadList)
            {
                vm.AddFile(url, file.Id, file.FormId, file.Url, file.Type, file.Size, UploadProperty.StandardName);
            }
            var toWrite = JsonConvert.SerializeObject(vm.initialPreviewConfig);
            return toWrite;
        }

        /// <summary>
        /// Convert Types from HTML to Bootstrap Dialog Viewbox
        /// </summary>
        /// <returns>A list of converted types</returns>
        public IEnumerable<string> ConvertTypes()
        {
            return UploadList.Select(upload=> FileUploadJsonViewModel.ConvertType(upload.Type));
        }
    }
}