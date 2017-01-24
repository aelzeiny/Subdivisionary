using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AutoMapper.Internal;
using Newtonsoft.Json.Linq;
using Subdivisionary.Models.Forms;
using Subdivisionary.ViewModels;

namespace Subdivisionary.DAL
{
    public class ProjectInfoModelBinder : CustomIFormModelBinder
    {
        public static readonly string TYPE_KEY = "projectInfoType";
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Type desiredType = Type.GetType(controllerContext.HttpContext.Request.Params[TYPE_KEY]);
            desiredType = typeof(NewApplicationViewModel<>).MakeGenericType(desiredType);
            bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, desiredType);
            var answer = base.BindModel(controllerContext, bindingContext);
            SyncFileForm(controllerContext, bindingContext, ((NewApplicationViewModel)answer).GetListForm());
            return answer;
        }
    }
}