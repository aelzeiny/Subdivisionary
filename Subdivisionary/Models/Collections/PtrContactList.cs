using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class PtrContactList : SerializableList<PtrContactInfo>
    {
    }

    public class PtrContactInfo
    {
        [DisplayName("Contact Type")]
        [Required]
        public PtrContactType ContactType { get; set; }
        [Required]
        public string Email { get; set; }

        public enum PtrContactType
        {
            [Display(Name = "Escrow Officer")]
            EscrowOfficer,
            [Display(Name = "Title Officer")]
            TitleOfficer
        }
    }
}