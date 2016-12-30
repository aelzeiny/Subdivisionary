using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc.Html;
using Subdivisionary.Dtos;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.ViewModels
{
    public class EditApplicationViewModel
    {
        public int EditId { get; set; }
        public AApplication Application { get; set; }
    }

    public class EditFormViewModel
    {
        public IForm Form { get; set; }
        public int ApplicationId { get; set; }
    }

    public class NewApplicationViewModel
    {
        [Display(Name = "Type")]
        public ApplicationTypeViewModel ApplicationType { get; set; }
        public BasicProjectInfo ProjectInfo { get; set; }

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
            ProjectInfo = application.ProjectInfo;
        }

        public AApplication CreateApplication()
        {
            var answer = CreateApplication(ApplicationType);

            // We do this because EF can call default constructors blindly.
            // So we ensure that this is always the case
            if (ProjectInfo != null)
                answer.ProjectInfo = ProjectInfo;

            /*foreach (var prop in ProjectInfo.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var val = prop.GetValue(ProjectInfo);
                if (prop.GetSetMethod() != null)
                    prop.SetValue(answer.ProjectInfo, val);
            }*/
            // Assign a current project info to the existing project
            // if it has already been initialized
            return answer;
        }

        internal static AApplication CreateApplication(ApplicationTypeViewModel appType)
        {
            AApplication answer = null;
            if (appType == ApplicationTypeViewModel.RecordOfSurvey)
                answer = RecordOfSurvey.Create();
            else if (appType == ApplicationTypeViewModel.CertificateOfCompliance)
                answer = CertificateOfCompliance.Create();
            /*else if (appType == ApplicationTypeViewModel.CcBypass)
                answer = new CcBypass(); 
            else if (appType == ApplicationTypeViewModel.LotLineAdjustment)
                answer = new LotLineAdjustment();
            else if (appType == ApplicationTypeViewModel.NewConstruction)
                answer = new NewConstruction();
            else if (appType == ApplicationTypeViewModel.CcEcp)
                answer = new CcEcp();*/
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            return answer;
        }
    }

    public static class ApplicationTypeViewModelExtension
    {
        public static Type GetApplicationType(this ApplicationTypeViewModel appType)
        {
            return NewApplicationViewModel.CreateApplication(appType).GetType();
        }
        public static Type GetProjectInfoType(this ApplicationTypeViewModel appType)
        {
            return NewApplicationViewModel.
                CreateApplication(appType).
                ProjectInfo.GetType();
        }
    }

    public enum ApplicationTypeViewModel
    {
        [Display(Name = "Record of Survey")]
        RecordOfSurvey,
        [Display(Name = "New Construction Condominium")]
        NewConstruction,
        [Display(Name = "Certificate of Compliance")]
        CertificateOfCompliance,
        [Display(Name = "Lot Line Adjustment")]
        LotLineAdjustment,
        [Display(Name = "Condo Conversion - Bypass")]
        CcBypass,
        [Display(Name = "Condo Conversion - ECP")]
        CcEcp
    }
}