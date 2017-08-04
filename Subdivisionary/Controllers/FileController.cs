using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Subdivisionary.FileUploaders;
using Subdivisionary.Helpers;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Forms;
using Subdivisionary.ViewModels;
using Subdivisionary.ViewModels.ApplicationViewModels;
using HttpResponseException = System.Web.Http.HttpResponseException;
namespace Subdivisionary.Controllers
{
    /// <summary>
    /// File Controller responsible for the maintainance of files
    /// </summary>
    public class FileController : AContextController
    {
        /// <summary>
        /// Upload File from designated Form into the appropriate
        /// storage system, and send a json-encoded response back 
        /// to the Kartic-FileInput controll that is awaiting this
        /// asynchronous upload.
        /// </summary>
        /// <param name="id">Form Id</param>
        /// <returns>A json response intended for the use of the Kartic-FileInput control</returns>
        public async Task<ActionResult> UploadFiles(int id)
        {
            IFileUploader uploader = new LocalDirectoryUploader(Server);
            Form mForm = await _context.Forms.FindAsync(id);
            UploadableFileForm form = mForm as UploadableFileForm;
            try
            {
                if (form == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                int appId = mForm.ApplicationId;

                // ensure that application belongs to user
                Application application = this.GetCurrentApplicant().Applications.FirstOrDefault(x => x.Id == appId);
                if (application == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);

                // Initialize Root Folder on Uploading system
                await uploader.InitializeRootFolder($"{application.Id}-{application.DisplayName}");

                var requestFiles = HttpContext.Request.Files;

                // Initialize the Json Kartic File-Input View-Model that will be returned
                FileUploadJsonViewModel vm = FileUploadJsonViewModel.Create();
                // According to the Kartic FileInput API, this loop will always only have 1 file
                // if we are uploading async (which we are)
                for (int i = 0; i < requestFiles.Count; i++)
                {
                    var file = requestFiles[i];
                    if (file == null || file.ContentLength <= 0)
                        continue;
                    var now = DateTime.Now;

                    var fileProps = form.GetFileUploadInfo(requestFiles.AllKeys[i]);

                    // Don't upload anything more than the maximum file number
                    if (form.MaximumFileUploadsExceeded(fileProps))
                        throw new Exception("Maximum File Count Limit Exceeded");

                    var upload = new FileUploadInfo
                    {
                        FileKey = fileProps.UniqueKey,
                        Form = form,
                        FormId = form.Id
                    };
                    form.FileUploads.Add(upload);
                    await _context.SaveChangesAsync();

                    // Use FileProps & Upload ID to create unique name for naming convention
                    string fileName =
                        $"{appId}_{fileProps.StandardName}_{now:yyyyMMdd}_{upload.Id}{Path.GetExtension(file.FileName)}";

                    string absoluteUri = await uploader.StoreFile(file, fileProps.FolderPath, fileName);

                    // Set the remaining upload properties with generated information. Save one last time.
                    upload.Size = file.ContentLength;
                    upload.Type = file.ContentType;
                    upload.Url = absoluteUri;
                    await _context.SaveChangesAsync();

                    vm.AddFile(Url, upload.Id, form.Id, absoluteUri, upload.Type, upload.Size, fileProps.StandardName);
                }
                // Save the ViewModel into a Json String & return it as content. Kartic-File Input will know what to do with it
                return Json(vm);
            }
            catch (Exception ex)
            {
                // If anything goes wrong, we gotta let Kartic-File Input know through JSON.
                string json = JsonConvert.SerializeObject(new
                {
                    error = ex.Message
                });
                return Json(json);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFile(int id, int formId)
        {
            Form mForm = await _context.Forms.FindAsync(formId);
            UploadableFileForm form = mForm as UploadableFileForm;
            if (form == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var file = form.FileUploads.FirstOrDefault(x => x.Id == id);
            if (file == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.FileUploads.Remove(file);
            if (form.IsAssigned && form.FindEmptyFileProperty().HasValue)
                form.IsAssigned = false;
            await _context.SaveChangesAsync();
            // Delete Said File if you can
            new FileInfo(file.Url).Delete();
            return Json("");
        }

        public FileResult Download(string file)
        {
            if (!Server.FilePathExists(file))
                throw new HttpResponseException(HttpStatusCode.Gone);
            string appId = DirectoryHelper.GetApplicationIdFromFilePath(file);
            // for security purposes we have to ensure that the download link is coming from the right applicant
            if (GetCurrentApplicant().Applications.All(x => x.Id.ToString() != appId))
                throw new HttpResponseException(HttpStatusCode.BadGateway);
            // Read all fileBytes to memory & initiate download
            return File(System.IO.File.ReadAllBytes(Server.MapPath(file)),
                System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(file));
        }
    }
}