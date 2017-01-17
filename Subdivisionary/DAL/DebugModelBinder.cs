using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;
using Subdivisionary.ViewModels;

namespace Subdivisionary.DAL
{
    public class DebugModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Type desiredType = Type.GetType(controllerContext.HttpContext.Request.Params["classType"]);
            desiredType = typeof(NewApplicationViewModel<>).MakeGenericType(new Type[] {desiredType});
            bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, desiredType);
            var answer = base.BindModel(controllerContext, bindingContext);
            return answer as NewApplicationViewModel;
        }

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            object answer = base.CreateModel(controllerContext, bindingContext, modelType);
            return answer;
        }

        protected override PropertyDescriptorCollection GetModelProperties(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var answer = base.GetModelProperties(controllerContext, bindingContext);
            return answer;
        }

        protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            var answer = base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
            return answer;
        }

        protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor, object value)
        {
            base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
        }

        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            base.OnModelUpdated(controllerContext, bindingContext);
        }

        protected override void OnPropertyValidated(ControllerContext controllerContext, ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor, object value)
        {
            base.OnPropertyValidated(controllerContext, bindingContext, propertyDescriptor, value);
        }

        protected override bool OnModelUpdating(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var answer = base.OnModelUpdating(controllerContext, bindingContext);
            return answer;
        }

        protected override bool OnPropertyValidating(ControllerContext controllerContext, ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor, object value)
        {
            var answer = base.OnPropertyValidating(controllerContext, bindingContext, propertyDescriptor, value);
            return answer;
        }

        protected override ICustomTypeDescriptor GetTypeDescriptor(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

            var answer = base.GetTypeDescriptor(controllerContext, bindingContext);
            return answer;
        }

        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor)
        {
            System.Diagnostics.Debug.WriteLine(propertyDescriptor.Name);
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}