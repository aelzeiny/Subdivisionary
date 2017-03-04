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
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
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
        public IList<IForm> Forms { get; set; }
        public IForm Form { get; set; }
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
                return "Forms/_ProjectInfoEditor";
            return "Forms/_" + ObjectContext.GetObjectType(form.GetType()).Name + "Editor";
        }
    }

    public struct FileUploadJsonViewModel
    {
        public bool append { get; set; }
        public List<InitialPreivewConfigViewModel> initialPreviewConfig { get; set; }
        public string initialPreview { get; set; }
        public string initialPreviewFileType { get; set; }

        public static FileUploadJsonViewModel Create()
        {
            return new FileUploadJsonViewModel()
            {
                append = true,
                initialPreviewConfig = new List<InitialPreivewConfigViewModel>(1),
            };
        }
        public struct InitialPreivewConfigViewModel
        {
            public string caption { get; set; }
            public string type { get; set; }
            public long size { get; set; }
            public bool previewAsData { get; set; }
            /// <summary>
            /// Deletion Url
            /// </summary>
            public string url { get; set; }
        }

        public struct InitialPreviewThumbTags
        {
            public string sampleTag { get; set; }
        }

        public void AddFile(UrlHelper url, int mfileId, int mformId, string absoluteUri, string contentType, long contentSize, string mcaption)
        {
            initialPreview = absoluteUri;//"<div class='file-preview-text'><h1><i class='fa fa-check-circle'></i></h1></div>";//.Add(absoluteUri);
            this.initialPreviewConfig.Add(new InitialPreivewConfigViewModel()
            {
                caption = "<i class='fa fa-check'></i> " + mcaption,
                type = ConvertType(contentType),
                size = contentSize,
                previewAsData = true,
                url = url.Action("DeleteFile","Applications", new
                {
                    id = mfileId,
                    formId = mformId
                })
            });
        }

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

    public class EmailInviteViewModel
    {
        public string ToName { get; set; }
        public string ApplicationDisplayName { get; set; }
        public string ApplicationUrl { get; set; }
        public string RegisterUrl { get; set; }
        public string Address { get; set; }
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

    public class FileUploadViewModelCollection
    {
        private FileUploadProperty[] properties;
        private IEnumerable<FileUploadInfo>[] files;
        public FileUploadViewModelCollection(UploadableFileForm form)
        {
            properties = form.FileUploadProperties;
            files = new IEnumerable<FileUploadInfo>[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                var prop = properties[i];
                files[i] = form.FileUploads.Where(x => x.FileKey == prop.UniqueKey);
            }
        }

        public FileUploadViewModel GetViewModel(string key, string label)
        {
            for(int i=0;i<properties.Length;i++)
                if (properties[i].UniqueKey == key)
                    return new FileUploadViewModel() {UploadList = files[i], UploadProperty = properties[i], LabelMessage = label};
            return null;
        }
    }

    public class FileUploadViewModel
    {
        public string LabelMessage { get; set; }

        public FileUploadProperty UploadProperty { get; set; }
        public IEnumerable<FileUploadInfo> UploadList { get; set; }


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
        public IEnumerable<string> ConvertTypes()
        {
            return UploadList.Select(upload=> FileUploadJsonViewModel.ConvertType(upload.Type));
        }
    }

    public class ShareApplicationViewModel : ICollectionForm
    {
        public string UserEmail { get; set; }
        public AddableCollectionEmailList ApplicantEmails { get; set; }
        public EmailList ShareRequests { get; set; }
        public string DisplayName { get; set; }
        public string ProjectInfoDisplay { get; set; }
        public object ApplicationId { get; set; }

        public string[] Keys => new [] { "emailListId" };

        public ShareApplicationViewModel(Application application, Applicant applicant)
        {
            ApplicationId = application.Id;
            ApplicantEmails = new AddableCollectionEmailList(application.Applicants.Select(x => new EmailInfo() {EmailAddress = x.User.Email}));
            DisplayName = application.DisplayName;
            ProjectInfoDisplay = application.ProjectInfo.ToString();
            ShareRequests = application.SharedRequests;
            UserEmail = applicant.User.Email;
        }

        public ICollectionAdd GetListCollection(string key)
        {
            return ApplicantEmails;
        }

        public object GetEmptyItem(string key)
        {
            return new EmailInfo();
        }

        public class AddableCollectionEmailList : List<EmailInfo>, ICollectionAdd
        {
            public AddableCollectionEmailList(IEnumerable<EmailInfo> info)
            {
                this.AddRange(info);
            }
            public void AddObject(object o)
            {
                this.Add((EmailInfo)o);
            }

            public void AddObjectUntilIndex(int index, object value, object blankItem)
            {
                if (index >= this.Count)
                    for (int i = this.Count - 1; i != index; i++)
                        this.Add((EmailInfo)blankItem);
                this[index] = (EmailInfo)value;
            }
        }
    }

    public class SignatureViewModel
    {
        public string SignerName { get; set; }

        public DateTime DateStamp { get; set; }
        public string UserStamp { get; set; }

        public string SerializationType { get; set; }
        public string SignatureData { get; set; }
    }

    public class SignatureCollectionViewModel
    {
        public int FormId { get; set; }
        public SignatureList Properties { get; set; }
        public ICollection<SignatureUploadInfo> Infos { get; set; }

        public SignatureCollectionViewModel(int formId, SignatureList props, ICollection<SignatureUploadInfo> infos)
        {
            this.FormId = formId;
            this.Properties = props;
            this.Infos = infos;
        }

        public SignatureViewModel[] GenerateSignatureViewModels()
        {
            SignatureViewModel[] answers = new SignatureViewModel[Properties.Count];
            for (int i = 0; i < answers.Length; i++)
            {
                var prop = Properties[i];
                answers[i] = new SignatureViewModel()
                {
                    SignerName = prop.SignerName
                };
                if (Infos == null)
                    continue;
                var sig = Infos.FirstOrDefault(x => x.SignerName == prop.SignerName);
                if (sig != null)
                {
                    answers[i].SerializationType = sig.DataFormat;
                    answers[i].SignatureData  = sig.Data;
                    answers[i].DateStamp = sig.DateStamp;
                    answers[i].UserStamp = sig.UserStamp;

                }
            }
            return answers;
        }
    }


    public class ListItemEditorViewModel
    {
        public object Data { get; set; }

        public string Key { get; set; }

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

        public string Key { get; set; }

        public abstract int Count();
        public abstract IEnumerable GetList();
    }

    public class ListEditorViewModel<T> : ListEditorViewModel
    {
        public ListEditorViewModel(string key, SerializableList<T> mList, T empty, bool addRemoveButton = false)
        {
            this.Key = key;
            this.List = mList;
            this.EmptyDataDefault = empty;
            this.AddRemoveButton = addRemoveButton;
        }
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