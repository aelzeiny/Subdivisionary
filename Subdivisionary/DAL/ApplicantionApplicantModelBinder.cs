using Subdivisionary.Models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Subdivisionary.DAL
{
    public class ApplicantionApplicantModelBinder : CustomIFormModelBinder
    {
        public static readonly string APPLICANT_KEY = "applicantRemoval";

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var form = controllerContext.HttpContext.Request.Form;
            return form.AllKeys.Where(x => x.StartsWith(APPLICANT_KEY)).Select(x => form.Get(x)).ToList();
        }
    }
}