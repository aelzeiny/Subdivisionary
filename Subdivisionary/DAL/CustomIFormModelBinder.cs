using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.DAL
{
    public abstract class CustomIFormModelBinder : DefaultModelBinder
    {
        public static readonly string ENTRY_CLASS_KEY = "Form.Entries[]";
        public static readonly string ENTRY_ID_KEY = "Form_Entries__";
        /// <summary>
        /// Adds IListForm Data to form
        /// </summary>
        protected void SyncFileForm(ControllerContext controllerContext, ModelBindingContext bindingContext, ICollectionForm answer)
        {
            for (int i = 0; true; i++)
            {
                string listPrefix = ENTRY_CLASS_KEY + i;
                var tuples = GetPropertiesTuples(controllerContext.HttpContext.Request.Form, listPrefix);
                if (tuples.Count == 0)
                    break;
                var info = ParseObject(bindingContext, listPrefix, tuples, answer.GetEmptyItem().GetType());
                answer.ModifyCollection(i, info);
            }
        }
        
        /// <summary>
        /// Get All property strings related to the object
        /// </summary>
        protected List<Tuple<string, string>> GetPropertiesTuples(NameValueCollection form, string propName)
        {
            if (!propName.EndsWith("."))
                propName += '.';
            List<Tuple<string, string>> propertiesList = new List<Tuple<string, string>>();
            foreach (var key in form.AllKeys)
                if (key.StartsWith(propName))
                    propertiesList.Add(new Tuple<string, string>(key, form[key]));
            return propertiesList;
        }

        /// <summary>
        /// Parses an object that has a series of properties that are '.' delimited.
        /// This parser assumes that all subtypes are declared in a default constructor.
        /// This parser also uses system.reflection, which always sacrifices performance for functionality
        /// </summary>
        /// <param name="attributeList">List of properties defined in (NAME, VALUE) format</param>
        /// <param name="infoType">Type of object that is ready to parse</param>
        /// <returns>Parsed Object</returns>
        protected object ParseObject(ModelBindingContext bindingContext, string propName, List<Tuple<string, string>> attributeList, Type infoType)
        {
            // Create instance of object based on type
            var answer = Activator.CreateInstance(infoType);
            // Foreach property in all the attributes
            foreach (var prop in attributeList)
            {
                string propertyName = prop.Item1.Substring(propName.Length+1);
                // parse properties based on '.' delmiter. Example "ContactInfo.City"
                string[] depth = propertyName.Split('.');
                Type containerType = infoType;
                object containerValue = answer;
                for (int i = 0; i < depth.Length - 1; i++)
                {
                    var currentProperty = infoType.GetProperty(depth[i]);
                    containerType = currentProperty.PropertyType;
                    containerValue = currentProperty.GetValue(containerValue);
                }
                // Get the last property name
                if (depth.Length > 1)
                    propertyName = propertyName.Substring(propertyName.LastIndexOf('.') + 1);

                // Get Property Type from container & property name
                var propertyType = containerType.GetProperty(propertyName);
                // Use property type to convert primitive & set value of parsed property
                object primitive = null;
                try
                {
                    primitive = ParsePrimitive(prop.Item2, propertyType.PropertyType);
                }
                catch (Exception ex)
                {
                    bindingContext.ModelState.AddModelError(prop.Item1, new ValidationException("Invalid value for " + propertyName));
                    continue;
                }
                propertyType.SetValue(containerValue, primitive);

                // And we can't forget about model binding validations, now can we...
                bindingContext.ModelState.Add(prop.Item1, new ModelState());
                bindingContext.ModelState.SetModelValue(prop.Item1, 
                    new ValueProviderResult(new string[] {prop.Item2}, prop.Item2, CultureInfo.CurrentCulture));

                var vc = new ValidationContext(containerValue, null, null);
                var validationRslt = new List<ValidationResult>();
                var validationAttr = propertyType.GetCustomAttributes(false).OfType<ValidationAttribute>();
                vc.MemberName = propertyName;

                if (!Validator.TryValidateProperty(primitive, vc, validationRslt))
                {
                    foreach (var rslt in validationRslt)
                    {
                        bindingContext.ModelState.AddModelError(prop.Item1,
                            new ValidationException(rslt, validationAttr.First(), primitive));
                    }
                }
            }
            return answer;
        }

        /// <summary>
        /// Parse a primitive object dynamically using the System.Converter class
        /// </summary>
        protected object ParsePrimitive(string primitive, Type propType)
        {
            var converter = TypeDescriptor.GetConverter(propType);
            return converter.ConvertFrom(primitive);
        }
        
    }
}