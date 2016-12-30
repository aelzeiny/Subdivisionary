using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Web.Mvc;
using Subdivisionary.Models.Forms;
using Subdivisionary.ViewModels;

namespace Subdivisionary.DAL
{
    public class FormModelBinder : CustomModelBinder
    {

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            if (modelType == typeof(IForm))
            {
                modelType = Type.GetType(controllerContext.HttpContext.Request.Params["formType"]);
                var form = controllerContext.HttpContext.Request.Form;
                // find all the properties that are related to project info
                List<Tuple<string, string>> propertiesList = GetPropertiesTuples(form, "Form");
                // Parse object (using System.Reflection) 
                var answer = ParseObject(propertiesList, modelType);
                return answer;
            }
            return base.CreateModel(controllerContext, bindingContext, modelType);
        }

    }
}