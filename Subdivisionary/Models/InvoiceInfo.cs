using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Models
{
    public class InvoiceInfo
    {
        public int Id { get; set; }

        public int InvoiceNo { get; set; }

        public int PaymentFormId { get; set; }
        public PaymentForm PaymentForm { get; set; }

        public string PayUrl { get; set; }
        public string PrintUrl { get; set; }

        public bool Paid { get; set; }
        public bool Void { get; set; }
        public DateTime Created { get; set; }
        
        public string Amount { get; set; }

        public EInvoicePurpose InvoicePurpose { get; set; }
    }

    public enum EInvoicePurpose
    {
        [Display(Name = "Application Processing Fee")] InitialPayment,

        [Display(Name = "Incomplete Fee")] IncompleteFee,

        [Display(Name = "Map Review Fee")] MapReviewFee
    }
}