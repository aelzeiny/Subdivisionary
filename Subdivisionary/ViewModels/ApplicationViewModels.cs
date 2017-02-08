using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Microsoft.WindowsAzure.Storage.Blob;
using Subdivisionary.Dtos;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.ViewModels
{
    public class EditApplicationViewModel
    {
        public int EditId { get; set; }
        public Application Application { get; set; }
    }

    public class EditFormViewModel
    {
        public IForm Form { get; set; }
        public int ApplicationId { get; set; }
        public int EditId { get; set; }

        public string GetPartialViewEditor()
        {
            return GetPartialViewEditor(Form);
        }

        public static string GetPartialViewEditor(IForm form)
        {
            if (form is BasicProjectInfo)
                return "_ProjectInfoEditor";
            return "_" + ObjectContext.GetObjectType(form.GetType()).Name + "Editor";
        }
    }

    public struct FileUploadJsonViewModel
    {
        public bool append { get; set; }
        public List<InitialPreivewConfigViewModel> initialPreviewConfig { get; set; }
        public string initialPreview { get; set; }

        public static FileUploadJsonViewModel Create()
        {
            return new FileUploadJsonViewModel()
            {
                append = true,
                initialPreviewConfig = new List<InitialPreivewConfigViewModel>(),
                initialPreview = ""
            };
        }
        public struct InitialPreivewConfigViewModel
        {
            public string caption { get; set; }
            public string filetype { get; set; }
            public long size { get; set; }
            public bool previewAsData { get; set; }
            /// <summary>
            /// Deletion Url
            /// </summary>
            public string url { get; set; }
        }

        public void AddFile(UrlHelper url, string absoluteUri, BlobProperties blob, FileUploadProperty props)
        {
            initialPreview = "<div class='file-preview-text'><h1><i class='fa fa-check-circle'></i></h1></div>";
            this.initialPreviewConfig.Add(new InitialPreivewConfigViewModel()
            {
                caption = $"<i class='fa fa-check'></i> {props.StandardName}",
                filetype = blob.ContentType,
                size = blob.Length,
                previewAsData = false,
                url = url.Action("DeleteFile","Applications", new
                {
                    formId = props.FormId,
                    uniqueKey = props.UniqueKey,
                    fileUrl = Path.GetFileName(absoluteUri)
                })

            });
        }
    }

    public class NewApplicationViewModel
    {
        public BasicProjectInfo ProjectInfo { get; set; }
        public ApplicationTypeViewModel ApplicationType { get; set; }

        /// <summary>
        /// ASP.NET Model Binding uses system.reflection to create an object.
        /// Thus it needs a constructor without any parameters
        /// </summary>
        public NewApplicationViewModel()
        {
        }

        public NewApplicationViewModel(int appTypeId, BasicProjectInfo projInfo) : this((ApplicationTypeViewModel)appTypeId, projInfo)
        {
        }

        public NewApplicationViewModel(ApplicationTypeViewModel appType, BasicProjectInfo projInfo)
        {
            this.ApplicationType = appType;
            ProjectInfo = projInfo;
        }
    }
    public class FileUploadViewModel
    {
        public string LabelMessage { get; set; }

        public FileUploadProperty UploadProperty { get; set; }
        public FileUploadList UploadList { get; set; }
    }

    public class ShareApplicationViewModel : ICollectionForm
    {
        public string UserEmail { get; set; }
        public List<EmailInfo> ApplicantEmails { get; set; }
        public EmailList ShareRequests { get; set; }
        public string DisplayName { get; set; }
        public string ProjectInfoDisplay { get; set; }
        public object ApplicationId { get; set; }

        public ShareApplicationViewModel(Application application, Applicant applicant)
        {
            ApplicationId = application.Id;
            ApplicantEmails = application.Applicants.Select(x => new EmailInfo() {EmailAddress = x.User.Email}).ToList();
            DisplayName = application.DisplayName;
            ProjectInfoDisplay = application.ProjectInfo.ToString();
            ShareRequests = application.SharedRequests;
            UserEmail = applicant.User.Email;
        }

        public ICollection GetListCollection()
        {
            return ApplicantEmails;
        }

        public object GetEmptyItem()
        {
            return new EmailInfo();
        }

        public void ModifyCollection(int index, object newValue)
        {
        }
    }


    public class ListItemEditorViewModel
    {
        public object Data { get; set; }

        public bool AddRemoveButton { get; set; } = false;

        public string GetPartialViewEditor()
        {
            return "CollectionEditors/_" + Data.GetType().Name + "Editor";
        }
    }

    public abstract class ListEditorViewModel
    {
        public bool AddRemoveButton { get; set; } = false;
        public object EmptyDataDefault { get; set; }

        public abstract int Count();
        public abstract IEnumerable GetList();
    }

    public class ListEditorViewModel<T> : ListEditorViewModel
    {
        public SerializableList<T> List { get; set; }

        public override IEnumerable GetList()
        {
            return List;
        }

        public override int Count()
        {
            return List.Count;
        }
    }

    public enum ApplicationTypeViewModel
    {
        [Display(Name = "Record of Survey")]
        RecordOfSurvey,
        [Display(Name = "Condo Conversion - Bypass")]
        CcBypass,
        [Display(Name = "Condo Conversion - ECP")]
        CcEcp,
        [Display(Name = "New Construction Condominium")]
        NewConstruction,
        [Display(Name = "Certificate of Compliance")]
        CertificateOfCompliance,
        [Display(Name = "Lot Line Adjustment")]
        LotLineAdjustment,
        [Display(Name = "Lot Merger")]
        LotMerger,
        [Display(Name = "Lot Subdivsion")]
        LotSubdivision,
    }
}