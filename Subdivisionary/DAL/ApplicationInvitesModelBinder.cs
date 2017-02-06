using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Subdivisionary.Models.Collections;
using ModelBindingContext = System.Web.Http.ModelBinding.ModelBindingContext;

namespace Subdivisionary.DAL
{
    public class ApplicationInvitesModelBinder : CustomIFormModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            List<EmailInfo> answer = new List<EmailInfo>();
            var form = controllerContext.HttpContext.Request.Form;
            var keys = form.AllKeys;
            for (int i = 0; i<keys.Length; i++)
            {
                if (keys[i].StartsWith(ENTRY_CLASS_KEY))
                {
                    var email = new EmailInfo(form.Get(keys[i]));
                    BindObjectIntoModelState(keys[i], email, bindingContext.ModelState);
                    answer.Add(email);
                }
            }
            return answer;
        }


    }
}