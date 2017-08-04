using AutoMapper;
using Subdivisionary.Dtos;
using Subdivisionary.Models;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary
{
    /// <summary>
    /// Dto stands for Data Transfer Objects. 
    /// They are responcible for Transfering Data into our model in our API.
    /// They mirror our model classes, but without certain info for added security.
    /// Another advantage of DTOs is that they provide a contract with the end-user
    /// such that the api versions itself at a slower pace than our api, allowing for
    /// better consistancy over time.
    /// We use Auto-Mapper to map the properties between these classes
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Configure Mapping Profile with System.reflection
        /// </summary>
        public MappingProfile()
        {
            // Map Project Info & all classes that inherit from it FORWARDS
            CreateMap<BasicProjectInfo, BasicProjectInfoDto>()
                .Include<ExtendedProjectInfo, ExtendedProjectInfoDto>()
                .Include<CcBypassInfo, CcBypassInfoDto>()
                .Include<CcEcpInfo, CcEcpInfoDto>()
                .Include<NewConstructionInfo, NewConstructionInfoDto>()
                .Include<CocAndLlaProjectInfo, CocAndLlaProjectInfoDto>()
                .Include<LotMergerAndSubdivisionInfo, LotMergerAndSubdivisionInfoDto>();
            CreateMap<ExtendedProjectInfo, ExtendedProjectInfoDto>();
            CreateMap<CcBypassInfo, CcBypassInfoDto>();
            CreateMap<CcEcpInfo, CcEcpInfoDto>();
            CreateMap<NewConstructionInfo, NewConstructionInfoDto>();
            CreateMap<CocAndLlaProjectInfo, CocAndLlaProjectInfoDto>();
            CreateMap<LotMergerAndSubdivisionInfo, LotMergerAndSubdivisionInfoDto>();

            // Map Project Info & all classes that inherit from it BACKWARDS
            CreateMap<BasicProjectInfoDto, BasicProjectInfo>()
                .Include<ExtendedProjectInfoDto, ExtendedProjectInfo>()
                .Include<CcBypassInfoDto, CcBypassInfo>()
                .Include<CcEcpInfoDto, CcEcpInfo>()
                .Include<NewConstructionInfoDto, NewConstructionInfo>()
                .Include<CocAndLlaProjectInfoDto, CocAndLlaProjectInfo>()
                .Include<LotMergerAndSubdivisionInfoDto, LotMergerAndSubdivisionInfo>();
            CreateMap<ExtendedProjectInfoDto, ExtendedProjectInfo>();
            CreateMap<CcBypassInfoDto, CcBypassInfo>();
            CreateMap<CcEcpInfoDto, CcEcpInfo>();
            CreateMap<NewConstructionInfoDto, NewConstructionInfo>();
            CreateMap<CocAndLlaProjectInfoDto, CocAndLlaProjectInfo>();
            CreateMap<LotMergerAndSubdivisionInfoDto, LotMergerAndSubdivisionInfo>();

            CreateMap<ApplicationStatusLogItemDtoSet, ApplicationStatusLogItem>();
            CreateMap<ApplicationStatusLogItem, ApplicationStatusLogItemDtoGet>();
            CreateMap<ContactInfo, ContactInfoDto>();
            CreateMap<ContactInfoDto, ContactInfo>();
            // DISABLED UNTIL WE FINALIZE API: Mapper.AssertConfigurationIsValid();
        }
    }
}