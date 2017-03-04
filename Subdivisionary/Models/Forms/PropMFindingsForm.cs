using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class PropMFindingsForm : SignatureForm
    {
        public override string DisplayName => "Proposition M";

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime? PropMDate { get; set; }

        [Required]
        [Display(Name = "City Planning Case #")]
        public int CityPlanningCaseNo { get; set; }

        [Required]
        [Display(Name = "1.	That existing neighborhood-serving retail uses be preserved and enhanced and future opportunities for resident employment in and ownership of such business be enhanced")]
        public string PropMQuestion1 { get; set; }

        [Required]
        [Display(Name = "2.	That existing housing and neighborhood character be conserved and protected in order to preserve the cultural and economic diversity of our neighborhood")]
        public string PropMQuestion2 { get; set; }

        [Required]
        [Display(Name = "3.	That the City’s supply of affordable housing be preserved and enhanced")]
        public string PropMQuestion3 { get; set; }

        [Required]
        [Display(Name = "4.	That commuter traffic not impede Muni transit service or overburden our streets or neighborhood parking")]
        public string PropMQuestion4 { get; set; }

        [Required]
        [Display(Name = "5.	That a diverse economic base be maintained by protecting our industrial and service sectors from displacement due to commercial office development, and that future opportunities for resident employment and ownership in these sectors be enhanced")]
        public string PropMQuestion5 { get; set; }

        [Required]
        [Display(Name = "6.	That the City achieve the greatest possible preparedness to protect against injury and loss of life in an earthquake")]
        public string PropMQuestion6 { get; set; }

        [Required]
        [Display(Name = "7.	That landmarks and historic buildings be preserved")]
        public string PropMQuestion7 { get; set; }

        [Required]
        [Display(Name = "8.	That our parks and open space and their access to sunlight and vistas be protected from development.")]
        public string PropMQuestion8 { get; set; }
        
        public PropMFindingsForm()
        {
            SignatureUploadProperties = new SignatureList();
            PropMDate = DateTime.Now;
        }
    }
}