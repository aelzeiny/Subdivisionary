using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Subdivisionary.Models.Forms;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using Subdivisionary.Helpers;
using Subdivisionary.Models;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.DAL
{
    public class CustomIFormModelBinder : DefaultModelBinder
    {
        public static readonly string ENTRY_CLASS_KEY = "Entries[].";
        public static readonly string ENTRY_ID_KEY = ENTRY_CLASS_KEY.Replace('.','_').Replace('[','_').Replace(']','_');

        /// <summary>
        /// Get All property strings related to the object
        /// </summary>
        protected NameValueCollection FilterPropertyCollection(NameValueCollection form, string propName)
        {
            NameValueCollection propertiesList = new NameValueCollection();
            var keys = form.AllKeys;
            foreach (var key in keys)
                if (key.StartsWith(propName))
                    propertiesList.Add(key, form[key]);
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
        protected object ParseObject(ModelBindingContext bindingContext, string propName, NameValueCollection attributeList, Type infoType)
        {
            // Create instance of object based on type
            var answer = Activator.CreateInstance(infoType);
            // Foreach property in all the attributes
            foreach (var prop in attributeList.AllKeys)
            {
                var val = attributeList.Get(prop);
                string propertyName = prop.Substring(propName.Length + 1);
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
                    primitive = ParsePrimitive(val, propertyType.PropertyType);
                }
                catch (NotSupportedException ex)
                {
                    bindingContext.ModelState.AddModelError(prop, new ValidationException($"Invalid value for {propertyName}. {ex.Message}"));
                    continue;
                }
                catch (ArgumentNullException ex)
                {
                    bindingContext.ModelState.AddModelError(prop, new ValidationException($"Empty value for {propertyName}. {ex.Message}"));
                    continue;
                }
                propertyType.SetValue(containerValue, primitive);

                // And we can't forget about model binding validations, now can we...
                bindingContext.ModelState.Add(prop, new ModelState());
                bindingContext.ModelState.SetModelValue(prop,
                    new ValueProviderResult(new string[] { val }, val, CultureInfo.CurrentCulture));

                var vc = new ValidationContext(containerValue, null, null)
                {
                    MemberName = propertyName
                };
                var nameAttrs = propertyType.GetCustomAttributes(typeof(DisplayNameAttribute), false).Cast<DisplayNameAttribute>();
                string memberDisplayName = nameAttrs.Any() ? nameAttrs.Single().DisplayName : propertyName;
                try
                {
                    Validator.ValidateProperty(primitive, vc);
                }
                catch (ValidationException ex)
                {
                    bindingContext.ModelState.AddModelError(prop, ex);
                }
            }
            return answer;
        }

        /// <summary>
        /// Parse a primitive object dynamically using the System.Converter class
        /// </summary>
        protected object ParsePrimitive(string primitive, Type propType)
        {
            if (propType == typeof(Boolean) && primitive == "true,false")
                return true;
            var converter = TypeDescriptor.GetConverter(propType);
            return converter.ConvertFrom(primitive);
        }

        /// <summary>
        /// Sync Collection to Form
        /// </summary>
        protected void SyncCollectionForm(ControllerContext controllerContext, ModelBindingContext bindingContext, ICollectionForm answer)
        {
            var form = controllerContext.HttpContext.Request.Form;
            NameValueCollection collection = FilterPropertyCollection(form, ENTRY_CLASS_KEY);
            foreach (var nameKey in answer.Keys)
            {
                var entryType = answer.GetEmptyItem(nameKey).GetType();
                var mCollection = answer.GetListCollection(nameKey);
                for (int i = 0; true; i++)
                {
                    string key = ENTRY_CLASS_KEY + nameKey + i;
                    NameValueCollection entryCollection = FilterPropertyCollection(collection, key);
                    if (!entryCollection.HasKeys())
                        break;
                    var obj = ParseObject(bindingContext, key, entryCollection, entryType);
                    mCollection.AddObjectUntilIndex(i, obj, null);
                }
            }
        }

        /// <summary>
        /// Binds object into ModelStateDictionary
        /// </summary>
        protected void BindObjectIntoModelState(string key, object value, ModelStateDictionary modelState)
        {
            var type = value.GetType();
            foreach (var prop in type.GetProperties())
            {
                var attrNames = prop.GetCustomAttributes(false).OfType<DisplayNameAttribute>();
                var vc = new ValidationContext(value, null, null)
                {
                    MemberName = prop.Name,
                    DisplayName = attrNames.Any() ? attrNames.First().DisplayName : prop.Name
                };

                try
                {
                    Validator.ValidateProperty(prop.GetValue(value), vc);
                }
                catch (ValidationException ex)
                {
                    modelState.AddModelError(key, ex);
                }
            }
        }
    }
}