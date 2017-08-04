namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// ViewModel for a voided invoice action
    /// </summary>
    public class VoidViewModel
    {
        /// <summary>
        /// ID of voided invoice
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        /// username of person voiding the invoice
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Stated Reason for invoice being voided.
        /// </summary>
        public string Reason { get; set; }
    }
}