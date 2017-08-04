using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.ProjectInfos
{
    /// <summary>
    /// For Condo-Conversion Bypass Applications
    /// </summary>
    public class CcBypassInfo : ExtendedProjectInfo, IUnitCount
    {
        [DisplayName("Total Number of Units")]
        [Range(2, 6, ErrorMessage = "Condo Conversions can only be between 2 to 6 Units.")]
        public virtual int NumberOfUnits { get; set; }

        public int UnitCount => NumberOfUnits;

        public override string DisplayName => "Project Information";

        public CcBypassInfo()
        {
            NumberOfUnits = 2;
        }

        public bool IsFinalMap()
        {
            return this.NumberOfUnits > Applications.Application.MAX_PARCEL_MAP_UNITS;
        }
    }
}