using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// View Model for the adding & removing of forms
    /// </summary>
    public class SwapFormsViewModel
    {
        public int ApplicationId { get; set; }
        public IList<Form> Forms { get; set; }
        public IEnumerable<Type> Options { get; set; }

        /// <summary>
        /// Use System.Reflection to get all 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSelects()
        {
            return Options.Select(x=>new SelectListItem() {Text = x.Name, Value = x.FullName});
        }
    }
}