using System.Collections.Generic;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// Edit Application View Model
    /// to pass into the Application/Details View
    /// </summary>
    public class EditApplicationViewModel
    {
        /// <summary>
        /// Form that is being edited
        /// </summary>
        public IForm Form { get; set; }
        /// <summary>
        /// Edited Form ID
        /// </summary>
        public int FormId { get; set; }
        /// <summary>
        /// Remaining Forms
        /// </summary>
        public IList<IForm> Forms { get; set; }
        /// <summary>
        /// Application being Edited
        /// </summary>
        public Application Application { get; set; }
    }
}