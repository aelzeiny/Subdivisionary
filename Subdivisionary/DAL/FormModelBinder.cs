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
    public class FormModelBinder : CustomModelBinder
    {

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var desiredType = Type.GetType(controllerContext.HttpContext.Request.Params[TypeStorage.GetBinderClassName()]);
            bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, desiredType);
            var answer = base.BindModel(controllerContext, bindingContext);
            if (answer is IUploadableFileForm)
                SyncFileForm(controllerContext, bindingContext, answer as IUploadableFileForm);
            return answer;
        }

        private void SyncFileForm(ControllerContext controllerContext, ModelBindingContext bindingContext, IUploadableFileForm answer)
        {
            for (int i = 0; true; i++)
            {
                string listPrefix = "Form.UploadList[" + i + "]";
                var tuples = GetPropertiesTuples(controllerContext.HttpContext.Request.Form, listPrefix);
                if (tuples.Count == 0)
                    break;
                var info = (CheckInfo)ParseObject(bindingContext, listPrefix, tuples, typeof(CheckInfo));
                answer.GetFileUploadList("");
                FileUploadList fileUploadList = (FileUploadList)answer.GetType().GetProperty("UploadList").GetValue(answer);
                fileUploadList.Add(info);
            }
        }
    }
}