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
using Subdivisionary.Models.ProjectInfos;
using Subdivisionary.ViewModels;

namespace Subdivisionary.DAL
{
    public class ProjectInfoModelBinder : CustomModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Type desiredType = Type.GetType(controllerContext.HttpContext.Request.Params[TypeStorage.GetBinderClassName()]);
            desiredType = typeof(NewApplicationViewModel<>).MakeGenericType(new Type[] { desiredType });
            bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, desiredType);
            var answer = base.BindModel(controllerContext, bindingContext);
            return answer;
        }
    }
}