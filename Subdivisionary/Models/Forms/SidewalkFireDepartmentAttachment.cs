using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class SidewalkFireDepartmentAttachment : Form, ICollectionForm
    {
        public override string DisplayName => "FD Attachment";

        public static readonly string COLL_REVIEWER_KEY = "collReviewerId";
        public static readonly string COLL_HYDRANTS_KEY = "collHydrantsId";
        public static readonly string COLL_CLEAR_WIDTH_KEY = "collMinClearWidthId";
        public static readonly string COLL_BUILDING_HEIGHT_KEY = "collBuildingHeightId";

        [Required]
        [Display(Name = "1. Has the Fire Department reviewed your plans?")]
        public bool HasFdReviewedPlans { get; set; }
        public NameSectionDateList FdSidewalkReviewers { get; set; }

        [Required]
        [Display(Name = "2. Are there fire hydrants in the area of your proposed work?")]
        public bool HasFdFireHydrants { get; set; }
        public PressureLocationList FdPressureLocationList { get; set; }

        public LocationWidthList FdLocationWidthList { get; set; }

        public LocationHeightList FdLocationHeightList { get; set; }

        public SidewalkFireDepartmentAttachment()
        {
            FdSidewalkReviewers = new NameSectionDateList();
            FdPressureLocationList = new PressureLocationList();
            FdLocationWidthList = new LocationWidthList();
            FdLocationHeightList = new LocationHeightList();
        }

        public string[] Keys => new []
        {
            COLL_REVIEWER_KEY, COLL_HYDRANTS_KEY, COLL_CLEAR_WIDTH_KEY, COLL_BUILDING_HEIGHT_KEY
        };

        public ICollectionAdd GetListCollection(string key)
        {
            if (key == COLL_REVIEWER_KEY)
                return FdSidewalkReviewers;
            if (key == COLL_HYDRANTS_KEY)
                return FdPressureLocationList;
            if (key == COLL_CLEAR_WIDTH_KEY)
                return FdLocationWidthList;
            return FdLocationHeightList;
        }

        public object GetEmptyItem(string key)
        {
            if (key == COLL_REVIEWER_KEY)
                return new NameSectionDateInfo();
            if (key == COLL_HYDRANTS_KEY)
                return new PressureLocationInfo();
            if (key == COLL_CLEAR_WIDTH_KEY)
                return new LocationWidthInfo();
            return new LocationHeightInfo();
        }
    }
}