using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models
{
    /// <summary>
    /// Application Status Enum. Values are spaced out by factors of 10 such that new statuses can be added without disrupting the flow of the enum
    /// </summary>
    public enum EApplicationStatus
    {
        [Display(Name = "Freshly Made")]
        Fresh = 0,

        [Display(Name = "Submitted for Review")]
        Submitted = 10,

        [Display(Name = "Initial Payment Received")]
        InitialPaymentReceived = 20,

        [Display(Name = "Application Deemed Incomplete")]
        DeemedIncomplete = 30,

        [Display(Name = "Incomplete Fee Received")]
        IncompleteFeeReceived = 40,

        [Display(Name = "Application Resubmittal")]
        Resubmitted = 50,

        [Display(Name = "Application Deemed Submittable")]
        DeemedSubmittable = 60,

        [Display(Name = "Map Review Fee Received")]
        MapReviewFeeReceived = 70,

        [Display(Name = "Done")]
        Done = int.MaxValue
    }
}