using System.ComponentModel.DataAnnotations;
using Subdivisionary.Models;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// View Model used for search queries
    /// </summary>
    public class SearchViewModel
    {
        /// <summary>
        /// Option Application ID filter
        /// </summary>
        [Display(Name = "App-ID")]
        public int? ApplicationId { get; set; }

        /// <summary>
        /// Optional Project type filter
        /// </summary>
        [Display(Name = "Project Type")]
        public EApplicationTypeViewModel? ProjectType { get; set; }

        /// <summary>
        /// Optional Block string Filter
        /// </summary>
        [Display(Name = "Block")]
        public string BlockQuery { get; set; }

        /// <summary>
        /// Optional Lot string Filter
        /// </summary>
        [Display(Name = "Lot")]
        public string LotQuery { get; set; }

        /// <summary>
        /// Optional Address string filter
        /// </summary>
        [Display(Name = "Address")]
        public string AddressQuery { get; set; }

        /// <summary>
        /// User Query string filter
        /// </summary>
        public string UserQuery { get; set; }
        
        /// <summary>
        /// Optional Application Status Filter
        /// </summary>
        public EApplicationStatus? Status { get; set; }
    }
}