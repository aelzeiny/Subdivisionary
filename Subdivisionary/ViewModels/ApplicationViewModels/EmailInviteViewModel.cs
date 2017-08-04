namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// Invitational Email ViewModel that holds all the properties of an invitational email.
    /// Used mostly by the controller to render a view
    /// </summary>
    public class EmailInviteViewModel
    {
        /// <summary>
        /// Who is this application addressed to
        /// </summary>
        public string ToName { get; set; }
        /// <summary>
        /// Who is this application from
        /// </summary>
        public string ApplicationDisplayName { get; set; }
        /// <summary>
        /// Share link of the application
        /// </summary>
        public string ApplicationUrl { get; set; }
        /// <summary>
        /// Register link for the website
        /// </summary>
        public string RegisterUrl { get; set; }
        /// <summary>
        /// Address of the Application in the invite
        /// </summary>
        public string Address { get; set; }
    }
}