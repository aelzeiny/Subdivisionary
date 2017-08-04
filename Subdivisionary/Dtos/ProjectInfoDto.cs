using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Dtos
{
    /**
     * Dto stands for Data Transfer Objects. 
     * They are responcible for Transfering Data into our model in our API.
     * They mirror our model classes, but without certain info for added security
     * Also, we use Auto-Mapper to map the properties between these classes
     */

    /// <summary>
    /// Base class for all Project Info Types. Specifically for Record of Surveys & Sidewalk legislation apps, which
    /// have the least basic requirements in comparison to all apps.
    /// </summary>
    public class BasicProjectInfoDto
    {
        /// <summary>
        /// List of Block/Lot/APN addresses
        /// </summary>
        public AddressList AddressList { get; set; }

        /// <summary>
        /// Contact Information of Primary Applicant. Might be an attorney, or a firm.
        /// </summary>
        [Required]
        public ContactInfoDto PrimaryContactInfo { get; set; }

        /// <summary>
        /// Optional Contact Information of Owner Applicant. 
        /// </summary>
        public ContactInfoDto OwnerContactInfo { get; set; }

        /// <summary>
        /// If Primary Contact and Owner Contact are the same, then assign this to true
        /// </summary>
        public bool OwnerAndPrimaryContactAreSame { get; set; }
    }

    /// <summary>
    /// Extended Project Information is like BasicProjectInfo, but with an additional
    /// Contact Field for a Land Firm
    /// </summary>
    public class ExtendedProjectInfoDto : BasicProjectInfoDto
    {
        /// <summary>
        /// Land Firm Contact Info
        /// </summary>
        [Required]
        public ContactInfo LandFirmContactInfo { get; set; }
    }

    /// <summary>
    /// CcBypass Project Information
    /// </summary>
    public class CcBypassInfoDto : ExtendedProjectInfoDto
    {
        /// <summary>
        /// Number of Units in Condo
        /// PreReq: must be between 2 - 6
        /// </summary>
        [Range(2, 6, ErrorMessage = "Condo Conversions can only be between 2 to 6 Units.")]
        public virtual int NumberOfUnits { get; set; }
    }

    /// <summary>
    /// For, you guessed it, Condo-Conversion ECP Projects
    /// </summary>
    public class CcEcpInfoDto : CcBypassInfoDto
    {
        /// <summary>
        /// Number of Units that are residential
        /// </summary>
        [Required]
        public int ResidentialUnits { get; set; }

        /// <summary>
        /// Number of Units that are commercial
        /// </summary>
        [Required]
        public int CommercialUnits { get; set; }
    }

    /// <summary>
    /// Projects for New Construction Condominiums
    /// </summary>
    public class NewConstructionInfoDto : CcEcpInfoDto
    {
        /// <summary>
        /// Does thi project creates a vertical subdivision?
        /// </summary>
        public bool CreatesVerticalSubdivision { get; set; }

        /// <summary>
        /// Does this subdivision includes an existing building/dwelling? (Will be shown on map)
        /// </summary>
        [Required]
        public bool HasExistingBuilding { get; set; }

        /// <summary>
        /// Number of New Construction Units within this project.
        /// PreReq: Must be greater than or equal to 1.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "New Construction Projects must be greater than 0 units.")]
        public override int NumberOfUnits { get; set; }
    }
    
    /// <summary>
    /// For Coc & Lla Projects
    /// </summary>
    public class CocAndLlaProjectInfoDto : ExtendedProjectInfoDto
    {
        /// <summary>
        /// Developer Contact Information
        /// </summary>
        public ContactInfo DeveloperContactInfo { get; set; }

        /// <summary>
        /// Number of lots existing/prior to the subdivision
        /// </summary>
        [Required]
        public int NumOfExisitingLots { get; set; }

        /// <summary>
        /// Number of Owner & Land Development Contacts
        /// </summary>
        public bool OwnerAndLandDevContactAreSame { get; set; }
    }

    /// <summary>
    /// Like Coc & Lla Projects, but with Lot Merger & Subdivision Information
    /// </summary>
    public class LotMergerAndSubdivisionInfoDto : CocAndLlaProjectInfoDto
    {
        /// <summary>
        /// Number of Lots the subdivision will result in
        /// </summary>
        [Required]
        public int NumOfProposedLots { get; set; }
    }
}