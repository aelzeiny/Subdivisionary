using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.DAL;
using Subdivisionary.Models.Applications;

namespace Subdivisionary.Models
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<Application> Applications { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
        public Applicant()
        {
            Applications = new LinkedList<Application>();
        }
    }
}