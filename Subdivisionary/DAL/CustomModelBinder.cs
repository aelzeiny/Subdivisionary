using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Subdivisionary.DAL
{
    public class CustomModelBinder : DefaultModelBinder
    {
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
                    propertiesList.Add(new Tuple<string, string>(key.Substring(propName.Length), form[key]));
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
        protected object ParseObject(List<Tuple<string, string>> attributeList, Type infoType)
        {
            // Create instance of object based on type
            var answer = Activator.CreateInstance(infoType);
            // Foreach property in all the attributes
            foreach (var prop in attributeList)
            {
                string propertyName = prop.Item1;
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
                propertyType.SetValue(containerValue, ParsePrimitive(prop.Item2, propertyType.PropertyType));
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