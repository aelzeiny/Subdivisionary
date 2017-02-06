using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc.Html;
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

    public class NewApplicationViewModel<T> : NewApplicationViewModel where T : BasicProjectInfo
    {
        public T ProjectInfo { get; set; }

        /// <summary>
        /// ASP.NET Model Binding uses system.reflection to create an object.
        /// Thus it needs a constructor without any parameters
        /// </summary>
        public NewApplicationViewModel()
        {
        }

        public NewApplicationViewModel(int appTypeId) : this((ApplicationTypeViewModel)appTypeId)
        {
        }

        public NewApplicationViewModel(ApplicationTypeViewModel appType)
        {
            this.ApplicationType = appType;
            var application = CreateApplication();
            ProjectInfo = (T) application.ProjectInfo;
        }

        public override ICollectionForm GetListForm()
        {
            return ProjectInfo;
        }

        public override Application CreateApplication()
        {
            var answer = CreateApplication(ApplicationType);
            if (ProjectInfo != null)
                answer.ProjectInfo = ProjectInfo;
            return answer;
        }
    }

    public abstract class NewApplicationViewModel
    {
        [Display(Name = "Type")]
        public ApplicationTypeViewModel ApplicationType { get; set; }

        public static readonly string PROJECT_INFO_TYPE_KEY = "classType";


        public abstract ICollectionForm GetListForm();
        public abstract Application CreateApplication();

        /// <summary>
        /// Create Application given the Application Type Enum. 
        /// </summary>
        /// <param name="appType">Type of Application we wish to make</param>
        /// <returns>Freshly minted Application</returns>
        internal static Application CreateApplication(ApplicationTypeViewModel appType)
        {
            Application answer = null;
            if (appType == ApplicationTypeViewModel.RecordOfSurvey)
                answer = Application.FactoryCreate<RecordOfSurvey>();

            else if (appType == ApplicationTypeViewModel.CcBypass)
                answer = Application.FactoryCreate<CcBypass>();
            else if (appType == ApplicationTypeViewModel.CcEcp)
                answer = Application.FactoryCreate<CcEcp>();
            else if (appType == ApplicationTypeViewModel.NewConstruction)
                answer = Application.FactoryCreate<NewConstruction>();

            else if (appType == ApplicationTypeViewModel.LotLineAdjustment)
                answer = Application.FactoryCreate<LotLineAdjustment>();
            else if (appType == ApplicationTypeViewModel.CertificateOfCompliance)
                answer = Application.FactoryCreate<CertificateOfCompliance>();
            else if (appType == ApplicationTypeViewModel.LotMerger)
                answer = Application.FactoryCreate<LotMerger>();
            else if (appType == ApplicationTypeViewModel.LotSubdivision)
                answer = Application.FactoryCreate<LotSubdivision>();
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            return answer;
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