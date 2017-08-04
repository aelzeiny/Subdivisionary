using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Subdivisionary.Helpers
{
    public static class CustomHtmlHelper
    {
        public static string RenderListAsHtmlString<T>(this HtmlHelper helper, IEnumerable<T> list)
        {
            // return new MvcHtmlString(JsonConvert.SerializeObject(list));
            return "[" + string.Join(",", list.Select(x => "\"" + x.ToString() + "\"")) + "]";
        }

        public static string EncodeJson(object obj)
        {
            return (JsonConvert.SerializeObject(obj, Formatting.None)); //.Replace("\"", "\'"));
        }

        public static IEnumerable<Type> GetAllSubtypes(Type t)
        {
            Stack<Type> answer = new Stack<Type>();
            var finalO = typeof(object);
            while (t != finalO)
            {
                answer.Push(t);
                t = t.BaseType;
            }
            return answer;
        }
    }
}