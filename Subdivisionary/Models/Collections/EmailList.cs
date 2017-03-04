using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    public class EmailList : SerializableList<EmailInfo>
    {
    }

    public class EmailInfo
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public EmailInfo()
        {
        }

        public EmailInfo(string eaddress)
        {
            EmailAddress = eaddress;
        }

        public override bool Equals(object obj)
        {
            var e = obj as EmailInfo;
            if (e == null)
                return false;
            return Equals(e);
        }

        protected bool Equals(EmailInfo other)
        {
            return string.Equals(EmailAddress, other.EmailAddress);
        }

        public override int GetHashCode()
        {
            return EmailAddress?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return EmailAddress;
        }
    }
}