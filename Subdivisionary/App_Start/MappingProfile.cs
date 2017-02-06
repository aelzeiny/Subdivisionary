using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Subdivisionary.Dtos;
using Subdivisionary.Models;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map Project Info & all classes that inherit from it FORWARDS
            Mapper.CreateMap<BasicProjectInfo, BasicProjectInfoDto>()
                .Include<CcBypassInfo, BypassInfoDto>()
                .Include<CocAndLlaProjectInfo, DeveloperExtendedProjectInfoDto>()
                .Include<CcEcpInfo, EcpInfoDto>()
                .Include<ExtendedProjectInfo, ExtendedProjectInfoDto>();

            // Map Project Info & all classes that inherit from it BACKWARDS
            Mapper.CreateMap<BasicProjectInfoDto, BasicProjectInfo>()
                .Include<BypassInfoDto, CcBypassInfo>()
                .Include<DeveloperExtendedProjectInfoDto, CocAndLlaProjectInfo>()
                .Include<EcpInfoDto, CcEcpInfo>()
                .Include<ExtendedProjectInfoDto, ExtendedProjectInfo>();

            // Map Remaining Subclasses forwards & backwards
            Mapper.CreateMap<CcBypassInfo, BypassInfoDto>();
            Mapper.CreateMap<BypassInfoDto, CcBypassInfo>();

            Mapper.CreateMap<CocAndLlaProjectInfo, DeveloperExtendedProjectInfoDto>();
            Mapper.CreateMap<DeveloperExtendedProjectInfoDto, CocAndLlaProjectInfo>();

            Mapper.CreateMap<CcEcpInfo, EcpInfoDto>();
            Mapper.CreateMap<EcpInfoDto, CcEcpInfo>();

            Mapper.CreateMap<ExtendedProjectInfo, ExtendedProjectInfoDto>();
            Mapper.CreateMap<ExtendedProjectInfoDto, ExtendedProjectInfo>();

            // Map Address & Contact Info classes forwards & backwards
            Mapper.CreateMap<Address, AddressDto>();
            Mapper.CreateMap<AddressDto, Address>();
            Mapper.CreateMap<ContactInfo, ContactInfoDto>();
            Mapper.CreateMap<ContactInfoDto, ContactInfo>();

            // DISABLED UNTIL WE FINALIZE API: Mapper.AssertConfigurationIsValid();
        }
    }
}