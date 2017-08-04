using System.ComponentModel.DataAnnotations;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// Viewable Applications by type. You can add another application
    /// from the pull-down menu by adding a new enum-type here.
    /// </summary>
    public enum EApplicationTypeViewModel
    {
        /// <summary>
        /// ROS
        /// </summary>
        [Display(Name = "Record of Survey")]
        RecordOfSurvey,
        /// <summary>
        /// 2CC-Bypass
        /// </summary>
        [Display(Name = "Condo Conversion - Bypass")]
        CcBypass,
        /// <summary>
        /// CC-ECP
        /// </summary>
        [Display(Name = "Condo Conversion - ECP")]
        CcEcp,
        /// <summary>
        /// NC
        /// </summary>
        [Display(Name = "New Construction Condominium")]
        NewConstruction,
        /// <summary>
        /// CoC
        /// </summary>
        [Display(Name = "Certificate of Compliance")]
        CertificateOfCompliance,
        /// <summary>
        /// LLA
        /// </summary>
        [Display(Name = "Lot Line Adjustment")]
        LotLineAdjustment,
        /// <summary>
        /// LM/LS/VS
        /// </summary>
        [Display(Name = "Parcel/Final Maps (Lot Mergers, Subdivisions, and Vertical Subdivision)")]
        ParcelFinalMap,
        /// <summary>
        /// SIL
        /// </summary>
        [Display(Name = "Sidewalk Legislation")]
        SidewalkLegislation
    }
}