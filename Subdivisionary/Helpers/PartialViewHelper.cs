using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Subdivisionary.Helpers
{

    // SOURCE: http://stackoverflow.com/questions/29808573/getting-the-values-from-a-nested-complex-object-that-is-passed-to-a-partial-view
    public static class PartialViewHelper
    {
        public static MvcHtmlString PartialFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string partialViewName)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            string oldPrefix = helper.ViewData.TemplateInfo.HtmlFieldPrefix;
            if (oldPrefix != "")
                name = oldPrefix + "." + name;
            ModelMetadata meta = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            object model = meta.Model;
            var viewData = new ViewDataDictionary(helper.ViewData)
            {
                TemplateInfo = new TemplateInfo { HtmlFieldPrefix = name }
            };
            return helper.Partial(partialViewName, model, viewData);
        }

        public static MvcHtmlString PartialFor<TModel>(this HtmlHelper<TModel> helper, string propertyDirectory, object model, string partialViewName)
        {
            var viewData = new ViewDataDictionary(helper.ViewData)
            {
                TemplateInfo = new TemplateInfo { HtmlFieldPrefix = propertyDirectory }
            };
            return helper.Partial(partialViewName, model, viewData);
        }
    }
}