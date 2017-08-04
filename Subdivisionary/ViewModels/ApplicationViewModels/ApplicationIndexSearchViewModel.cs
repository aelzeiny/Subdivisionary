using System.Collections.Generic;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// For Application searches on Index Page
    /// </summary>
    public class ApplicationIndexSearchViewModel
    {
        /// <summary>
        /// Queries filter for searches
        /// </summary>
        public SearchViewModel SearchQuery { get; set; }

        /// <summary>
        /// List of results that the query yeilded
        /// </summary>
        public IEnumerable<ApplicationIndexViewModel> Results { get; set; }
    }
}