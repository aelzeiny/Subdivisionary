using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models
{
    public abstract class Notification
    {
        /// <summary>
        /// This is an ID #
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Applicant only assigned once a product is registered
        /// </summary>
        public Applicant Applicant { get; set; }

        /// <summary>
        /// Foreign Key for Applicant
        /// </summary>
        public int ApplicantId { get; set; }
        public abstract string Display { get; }
    }


    public class ShareApplicationNotification : Notification
    {
        /// <summary>
        /// This could either be an application ID or a refferral ID
        /// </summary>
        public int ApplicationId { get; set; }

        public override string Display => $"Invitation to edit ID #{ApplicationId}";
    }
}