using Subdivisionary.Models;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// Application Index View Mode
    /// </summary>
    public class ApplicationIndexViewModel
    {
        /// <summary>
        /// Application ID
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// What to call the application
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Block & Lots of application in Block/Lot format (AKA: APN)
        /// </summary>
        public string BlockLots { get; set; }
        /// <summary>
        /// List of Addresses from the block/lot
        /// </summary>
        public string Addresses { get; set; }
        /// <summary>
        /// Current status of the application
        /// </summary>
        public EApplicationStatus ApplicationStatus { get; set; }
    }
}