using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Applications;

namespace Subdivisionary.Dtos
{
    /**
     * Dto stands for Data Transfer Objects. 
     * They are responcible for Transfering Data into our model in our API.
     * They mirror our model classes, but without certain info for added security
     * Also, we use Auto-Mapper to map the properties between these classes
     */

    public class BasicProjectInfoDto
    {
        public int Id { get; internal set; }

        public bool IsAssigned { get; set; }

        public string Block { get; set; }
        public string Lot { get; set; }
        public AddressDto Address { get; set; }
        public ContactInfoDto PrimaryContactInfo { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public BasicProjectInfoDto()
        {
            this.Address = new AddressDto();
            this.PrimaryContactInfo = new ContactInfoDto();
        }
    }

    public class ExtendedProjectInfoDto : BasicProjectInfoDto
    {
        public int NumberOfUnits { get; set; }
        public ContactInfoDto FirmContactInfo { get; set; }

        public ExtendedProjectInfoDto()
        {
            FirmContactInfo = new ContactInfoDto();
        }
    }

    public class BypassInfoDto : ExtendedProjectInfoDto
    {
        public int TenantOccupiedUnits { get; set; }
    }
    
    public class EcpInfoDto : BypassInfoDto
    {
        public int ResidentialUnits { get; set; }
        public int CommercialUnits { get; set; }
    }

    public class DeveloperExtendedProjectInfoDto : ExtendedProjectInfoDto
    {
        public ContactInfoDto DeveloperContactInfo { get; set; }

        public DeveloperExtendedProjectInfoDto()
        {
            DeveloperContactInfo = new ContactInfoDto();
        }
    }
}