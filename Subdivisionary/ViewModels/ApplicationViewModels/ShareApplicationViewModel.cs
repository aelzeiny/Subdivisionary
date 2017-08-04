using System.Collections.Generic;
using System.Linq;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// Share Application View Model. Since this form has a 
    /// collection, we give it the ICollectionForm interface for convience
    /// in implementation.
    /// </summary>
    public class ShareApplicationViewModel : ICollectionForm
    {
        /// <summary>
        /// User Email
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// Registered User Emails
        /// </summary>
        public AddableCollectionEmailList ApplicantEmails { get; set; }
        /// <summary>
        /// Share Request emails
        /// </summary>
        public EmailList ShareRequests { get; set; }
        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Project Info Display
        /// </summary>
        public string ProjectInfoDisplay { get; set; }
        /// <summary>
        /// Application ID
        /// </summary>
        public object ApplicationId { get; set; }
        /// <summary>
        /// ICollection Key
        /// </summary>
        public string[] Keys => new [] { "emailListId" };

        /// <summary>
        /// Creates Share Application View model given application & applicant
        /// </summary>
        /// <param name="application">Application</param>
        /// <param name="applicant">Applicant</param>
        public ShareApplicationViewModel(Application application, Applicant applicant)
        {
            ApplicationId = application.Id;
            ApplicantEmails = new AddableCollectionEmailList(application.Applicants.Select(x => new EmailInfo() {EmailAddress = x.User.Email}));
            DisplayName = application.DisplayName;
            ProjectInfoDisplay = application.ProjectInfo.ToString();
            ShareRequests = application.SharedRequests;
            UserEmail = applicant.User.Email;
        }

        /// <summary>
        /// Gets Applicant Email List
        /// </summary>
        /// <param name="key">Will always be "emailListId"</param>
        /// <returns>ApplicantEmails</returns>
        public ICollectionAdd GetListCollection(string key)
        {
            return ApplicantEmails;
        }

        /// <summary>
        /// Return Empty Item
        /// </summary>
        /// <param name="key">Always the smae "emailListId"</param>
        /// <returns>string key</returns>
        public object GetEmptyItem(string key)
        {
            return new EmailInfo();
        }

        /// <summary>
        /// AddableCollection Email List
        /// </summary>
        public class AddableCollectionEmailList : List<EmailInfo>, ICollectionAdd
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="info">List of Email Info</param>
            public AddableCollectionEmailList(IEnumerable<EmailInfo> info)
            {
                this.AddRange(info);
            }
            /// <summary>
            /// Add new Email
            /// </summary>
            /// <param name="o">EmailInfo o</param>
            public void AddObject(object o)
            {
                this.Add((EmailInfo)o);
            }
            /// <summary>
            /// Add Object Until Specified index
            /// </summary>
            /// <param name="index">add empty objects at 0 till index. object goes at index</param>
            /// <param name="value">object to be added, must be email Info</param>
            /// <param name="blankItem">new EmailInfo()</param>
            public void AddObjectUntilIndex(int index, object value, object blankItem)
            {
                if (index >= this.Count)
                    for (int i = this.Count - 1; i != index; i++)
                        this.Add((EmailInfo)blankItem);
                this[index] = (EmailInfo)value;
            }
        }
    }
}