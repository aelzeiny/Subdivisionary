using System.Collections.Generic;
using System.Web.Mvc;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// This view model corresponds with Kartic's Bootstrap FileUploader API.
    /// Variables are given names and significance based on the ability to serialize
    /// to the FileUploader Library. 
    /// </summary>
    
    public struct FileUploadJsonViewModel
    {
        /// <summary>
        /// Whether an upload should append or overwrite the other uploads
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool append { get; set; }
        /// <summary>
        /// Initial Preview Configuration Settings
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public List<InitialPreivewConfigViewModel> initialPreviewConfig { get; set; }
        /// <summary>
        /// Initial Preview Settings
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string initialPreview { get; set; }
        /// <summary>
        /// Initial Preview Component Type
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string initialPreviewFileType { get; set; }

        /// <summary>
        /// This is the Factory Constructor for the struct, since 
        /// in C# structure constructors are all or nothing type deals.
        /// </summary>
        /// <returns>Fresh VM with default initialization</returns>
        public static FileUploadJsonViewModel Create()
        {
            return new FileUploadJsonViewModel()
            {
                append = true,
                initialPreviewConfig = new List<InitialPreivewConfigViewModel>(1),
            };
        }
        /// <summary>
        /// Initial Preview Configuration
        /// </summary>
        // ReSharper disable InconsistentNaming <-- Removes resharper warnings
        public struct InitialPreivewConfigViewModel
        {
            /// <summary>
            /// Caption HTML
            /// </summary>
            public string caption { get; set; }
            /// <summary>
            /// File Preview Type 
            /// </summary>
            public string type { get; set; }
            /// <summary>
            /// File content Length/Size
            /// </summary>
            public long size { get; set; }
            /// <summary>
            /// Should almost always be set to true
            /// </summary>
            public bool previewAsData { get; set; }
            /// <summary>
            /// Deletion Url
            /// </summary>
            public string url { get; set; }
        }
        /// <summary>
        /// You can directly set the initial preview thumb tags here
        /// </summary>
        public struct InitialPreviewThumbTags
        {
            /// <summary>
            /// HTML of a sample thumb tag
            /// </summary>
            public string sampleTag { get; set; }
        }

        /// <summary>
        /// Add File to the ViewModel and set the inital configurations of that file
        /// </summary>
        /// <param name="url">Used to map Controller Actions, i.e: Url.Action(...)</param>
        /// <param name="mfileId">File ID</param>
        /// <param name="mformId">Form ID</param>
        /// <param name="absoluteUri">Visitation Path for preview container</param>
        /// <param name="contentType">File Content Type (default HTML content type)</param>
        /// <param name="contentSize">Content Size/Length</param>
        /// <param name="mcaption">Desired File Caption in preview</param>
        public void AddFile(UrlHelper url, int mfileId, int mformId, string absoluteUri, string contentType, long contentSize, string mcaption)
        {
            initialPreview = absoluteUri;//"<div class='file-preview-text'><h1><i class='fa fa-check-circle'></i></h1></div>";//.Add(absoluteUri);
            this.initialPreviewConfig.Add(new InitialPreivewConfigViewModel()
            {
                caption = "<i class='fa fa-check'></i> " + mcaption,
                type = ConvertType(contentType),
                size = contentSize,
                previewAsData = true,
                url = url.Action("DeleteFile","File", new
                {
                    id = mfileId,
                    formId = mformId
                })
            });
        }
        /// <summary>
        /// There is a difference between Kartic's Bootstrapper File type and the
        /// HTML file Type. This takes common HTML file types and converts them to
        /// something the API can understand
        /// </summary>
        /// <param name="uploadType">HTML Upload type (i.e: Application/pdf)</param>
        /// <returns>Kartic Bootstrap's upload type (i.e: pdf)</returns>
        public static string ConvertType(string uploadType)
        {
            if (uploadType == "application/pdf")
                return "pdf";
            if (uploadType.Contains("image"))
                return "image";
            if (uploadType == "application/x-shockwave-flash")
                return "flash";
            if (uploadType.Contains("text"))
                return "text";
            if (uploadType.Contains("html"))
                return "html";
            if (uploadType.Contains("audio"))
                return "audio";
            if (uploadType.Contains("video"))
                return "video";
            return "object";
        }
    }
}