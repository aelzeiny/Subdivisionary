using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// <summary>
        /// Override Get property Value to return an object that is a sub-type of the object stated in the
        /// propertyDescriptor field. In this case, we will take ProjectInfo of type BasicProjectInfo and have
        /// it return a sub-type of BasicProjectInfo. The default binder would unfortunately just make a BasicProjectInfo type.
        /// </summary>
        /// <returns>Sub-type of BasicProjectInfo</returns>
        protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            if (propertyDescriptor.Name == "ProjectInfo")
            {
                // The model binder, by default, will create a Basic Project object - which is not what we want
                // (1) Get the Request Form Context
                var form = controllerContext.HttpContext.Request.Form;
                // (2) define the type of the BasicProjectInfo that we actually need
                Type projectInfoType = Type.GetType(controllerContext.HttpContext.Request.Params["projectInfoType"]);
                // (3) find all the properties that are related to project info
                List<Tuple<string, string>> propertiesList = this.GetPropertiesTuples(form, "ProjectInfo");
                // (4) We parse the object (using System.Reflection) 
                return ParseObject(propertiesList, projectInfoType);
            }
            return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
        }
    }
}