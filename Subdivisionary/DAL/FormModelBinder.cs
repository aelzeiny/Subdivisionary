using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Web.Mvc;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;
using Subdivisionary.ViewModels;

namespace Subdivisionary.DAL
{
    public class FormModelBinder : CustomIFormModelBinder
    {
        public static readonly string TYPE_KEY = "formType";
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var desiredType = Type.GetType(controllerContext.HttpContext.Request.Params[TYPE_KEY]);
            bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, desiredType);
            var answer = base.BindModel(controllerContext, bindingContext);
            if (answer is ICollectionForm)
                SyncCollectionForm(controllerContext, bindingContext, (ICollectionForm)answer);
            if (answer is IUploadableFileForm)
                SyncFileForm(controllerContext, bindingContext, (IUploadableFileForm)answer);
            return answer;
        }
    }
}