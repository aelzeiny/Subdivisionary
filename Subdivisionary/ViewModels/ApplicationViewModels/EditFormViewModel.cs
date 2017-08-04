using System.Data.Entity.Core.Objects;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// View Model for the _FormEditor Partial View. Also maps Form Models
    /// to their subsequent Partial Views
    /// </summary>
    public class EditFormViewModel
    {
        /// <summary>
        /// Form being edited
        /// </summary>
        public IForm Form { get; set; }
        /// <summary>
        /// ID of form being edited
        /// </summary>
        public int FormId { get; set; }
        /// <summary>
        /// Application ID
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Gets the partial view name of the form being edited. 
        /// By convention, this returns $"Forms/_{FormTypeName}Editor".
        /// Project Infos are the exception, but this could be changed by
        /// changing it here. Also, this is where you add other exceptions.
        /// </summary>
        /// <returns>String of the partialview that is reponsible for rendering the form model</returns>
        public string GetPartialViewEditor()
        {
            return GetPartialViewEditor(Form);
        }
        /// <summary>
        /// Static method that
        /// gets the partial view name of the form being edited. 
        /// By convention, this returns $"Forms/_{FormTypeName}Editor".
        /// Project Infos are the exception, but this could be changed by
        /// changing it here. Also, this is where you add other exceptions.
        /// </summary>
        /// <param name="form">Model Form type that has a complimentary view component</param>
        /// <returns>String of the partialview that is reponsible for rendering the form model</returns>
        public static string GetPartialViewEditor(IForm form)
        {
            if (form is BasicProjectInfo)
                return "Forms/_ProjectInfoEditor";
            return "Forms/_" + ObjectContext.GetObjectType(form.GetType()).Name + "Editor";
        }
    }
}