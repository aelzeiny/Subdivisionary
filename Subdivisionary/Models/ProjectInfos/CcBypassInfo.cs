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
    public class CcBypassInfo : ExtendedProjectInfo
    {
        [DisplayName("Total Number of Units")]
        public int NumberOfUnits { get; set; }

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