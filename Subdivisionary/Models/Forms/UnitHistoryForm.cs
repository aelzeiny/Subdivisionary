using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.Ajax.Utilities;
using Subdivisionary.Models.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Subdivisionary.Models.Applications;

namespace Subdivisionary.Models.Forms
{
    public class UnitHistoryForm : Form, ICollectionForm
    {
        public override string DisplayName => (ApartmentNumber == null) ? "Unit History" : $"Unit #{ApartmentNumber} History ";

        public static readonly string OCCUPANTS_KEY = "occupantId";
        public static readonly string SIX_YEAR_HISTORY_KEY = "sixYrId";
        public string[] Keys => new[] { OCCUPANTS_KEY, SIX_YEAR_HISTORY_KEY };

        [Required]
        [Display(Name = "Apartment No")]
        public string ApartmentNumber { get; set; }

        [Required]
        [Display(Name = "Occupancy Type")]
        public OccupancyType OccupancyType { get; set; }

        [Required]
        [Display(Name = "Number Of Bedrooms")]
        public int NumberOfBedrooms { get; set; }

        [Required]
        [Display(Name = "Square Feet")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal SquareFeet { get; set; }

        [RequiredIfOccupancyIsTenant]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Current Rental Rate")]
        public decimal CurrentRentalRate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Proposed Sales Price")]
        public decimal ProposedSalesPrice { get; set; }

        public UnitHistoryList Timeline { get; set; }
        
        public NamesList OccupiedNamesList { get; set; }

        public OccupancyRangeList OccupancyRangeOccupationHistoryList { get; set; }

        public UnitHistoryForm()
        {
            Timeline = new UnitHistoryList();
            OccupiedNamesList = new NamesList();
            OccupancyRangeOccupationHistoryList = new OccupancyRangeList();
        }

        public ICollectionAdd GetListCollection(string key)
        {
            if(key == OCCUPANTS_KEY)
                return OccupiedNamesList;
            return OccupancyRangeOccupationHistoryList;
        }

        public object GetEmptyItem(string key)
        {
            if (key == OCCUPANTS_KEY)
                return new OccupantNameInfo();
            return new OccupancyRangeInfo();
        }
    }

    public enum OccupancyType { Owner, Tenant, Vacant, Mixed}

    public class RequiredIfOccupancyIsTenant : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as UnitHistoryForm;
            if (obj.OccupancyType == OccupancyType.Tenant && ((string)value).IsNullOrWhiteSpace())
                return new ValidationResult("Please provide the unit's rental price for the current tenant(s)");
            return ValidationResult.Success;
        }
    }
}