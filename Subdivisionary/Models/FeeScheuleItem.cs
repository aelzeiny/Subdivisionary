using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Applications;

namespace Subdivisionary.Models
{
    public class FeeScheuleItem
    {
        public int Id { get; set; }

        /// <summary>
        /// This string MUST be equal to the class type's name. I.E "typeof(RecordOfSurvey).Name"
        /// </summary>
        public string ApplicationTypeName { get; set; }
        /// <summary>
        /// The Processing fee is the first fee applied to every application as
        /// a prerequisit for submittal. This is the base fee.
        /// </summary>
        public float BaseProcessingFee { get; set; }

        /// <summary>
        /// THe Map Review Fee is only charged once an application is deemed submittable.
        /// Comes after the processing fee, if at all.
        /// </summary>
        public float BaseMapReviewFee { get; set; }

        /// <summary>
        /// Linear increase of price based on the number of units a project requires
        ///  (for parcel Maps)
        /// </summary>
        public float BaseMapPerUnitFee { get; set; }

        /// <summary>
        /// Map Review fee for Final Maps. This is the same as the BaseMapReviewFee, but
        /// only for final maps. If project is the same price regardless of parcel/final map
        /// status, then please set this variable to be equal to the BaseMapFee
        /// </summary>
        public float FinalMapReviewFee { get; set; }

        /// <summary>
        /// Linear increase of price based on the number of units a project requires
        /// (for final maps). 
        /// </summary>
        public float FinalMapPerUnitFee { get; set; }

        /// <summary>
        /// Linear increase of price based on the number of units a project requires
        /// for the initial processing fee
        /// </summary>
        public float BaseProcessingPerUnitFee { get; set; }

        /// <summary>
        /// On the fee schedule of 2016/17, there is a line item that denotes 
        /// "Minimum fee; Additional fees may be assessed on time and materials basis."
        /// True means that this disclaimer is present, and false for this disclaimer is 
        /// not present
        /// </summary>
        public bool AdditionalFeesMayApplyDisclaimer { get; set; }

        public FeeScheuleItem(string name)
        {
            this.ApplicationTypeName = name;
        }

        /// <summary>
        /// Calculate the initial processing fee of an application. 
        /// </summary>
        /// <param name="numberOfUnits">Number of Units could contribute to a linear increase in price</param>
        /// <returns></returns>
        public float CalculateProcessingFee(int numberOfUnits = 0)
        {
            return BaseProcessingFee + BaseProcessingPerUnitFee*numberOfUnits;
        }

        /// <summary>
        /// This fee is due once an application is deemed submittable. Note that some applications do not have a
        /// secondary fee, so this may return '0'.
        /// </summary>
        /// <param name="numberOfUnits">Number of Units could contribute to a linear increase in price,
        ///  based on if the project is a parcel/final map</param>
        /// <returns></returns>
        public float CalculateMapReviewFee(int numberOfUnits = 0)
        {
            if (numberOfUnits > Application.MAX_PARCEL_MAP_UNITS)
                return BaseMapReviewFee + BaseMapPerUnitFee*numberOfUnits;
            return FinalMapReviewFee + FinalMapPerUnitFee*numberOfUnits;
        }
    }
}

/// <summary>
/// The intent of this pseduo-enum class is to encapsulate all possible FeeSchedule items within the database.
/// When a Fee Schedule Item is added/removed into the dbo.FeeScheduleItem table, it should be reflected here.
/// This practice will reduce the number of "magic strings" throughout our application
/// </summary>
public sealed class EFeeSchedule
{
    private readonly string _feeName;

    public static readonly EFeeSchedule Conversions = new EFeeSchedule("CC");
    public static readonly EFeeSchedule NewConstruction = new EFeeSchedule("NC");
    public static readonly EFeeSchedule LotSubdivisionOrMerger = new EFeeSchedule("LS/LM");
    public static readonly EFeeSchedule LotlineAdjustment = new EFeeSchedule("LLA");
    public static readonly EFeeSchedule AmendedMap = new EFeeSchedule("AM");
    public static readonly EFeeSchedule CertificateOfCompliance = new EFeeSchedule("CoC");
    public static readonly EFeeSchedule SidewalkLegislation = new EFeeSchedule("Sidewalk Legislation");
    public static readonly EFeeSchedule RecordOfSurvey = new EFeeSchedule("ROS");
    public static readonly EFeeSchedule CornerRecord = new EFeeSchedule("CR");

    public EFeeSchedule(string feeName)
    {
        this._feeName = feeName;
    }

    public override string ToString()
    {
        return _feeName;
    }
}