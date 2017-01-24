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
        protected override int ParamCount => 2;

        protected override PtrContactInfo ParseObject(string[] param)
        {
            return new PtrContactInfo()
            {
                ContactType = (PtrContactInfo.PtrContactType) int.Parse(param[0]),
                Email = param[1]
            };
        }

        protected override string[] SerializeObject(PtrContactInfo serialize)
        {
            return new string[]
            {
                ((int)(serialize.ContactType)).ToString(),
                serialize.Email
            };
        }
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
            [Display(Name = "Escro Officer")]
            EscrowOfficer,
            [Display(Name = "Title Officer")]
            TitleOfficer
        }
    }
}