using System.Collections.Generic;
using System.Linq;
using Subdivisionary.Models;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// Submitted Application View Model
    /// </summary>
    public class ApplicationSubmittedViewModel
    {
        /// <summary>
        /// App ID
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Some applications have specific instructions on what to do next
        /// </summary>
        public IEnumerable<string> NextSteps { get; set; }

        /// <summary>
        /// Designated Fee for application
        /// </summary>
        public float Fee { get; set; }

        /// <summary>
        /// All the invoices for the application
        /// </summary>
        public List<InvoiceInfo> Invoices { get; set; }

        /// <summary>
        /// All the statuses for the application
        /// </summary>
        public List<ApplicationStatusLogItem> Statuses { get; set; }

        /// <summary>
        /// A determination whether the application can be edited
        /// </summary>
        public bool ApplicationCanEdit { get; set; }

        /// <summary>
        /// Current Status of the Application
        /// </summary>
        /// <returns>Last Item in status history</returns>
        public EApplicationStatus CurrentStatus()
        {
            return Statuses.Last().Status;
        }

        /// <summary>
        /// Determines whether an application has a status
        /// </summary>
        /// <param name="status">status of the application</param>
        /// <returns>boolean perdicting whether the application has a status</returns>
        public bool HasStatus(EApplicationStatus status)
        {
            return Statuses.Any(x => x.Status == status);
        }
    }
}