using AutoMapper;
using LocalizaLabs.Api.v1.Models.NumberProcessing;
using LocalizaLabs.Domain.Entities;

namespace LocalizaLabs.Api.v1.Configurations.Mappers
{
    public class NumberProcessingMapperProfile : Profile
    {
        public NumberProcessingMapperProfile()
        {
            CreateMap<NumberProcessingResult, NumberProcessingResponseModel>(MemberList.Destination)
                .ConstructUsing(src => new NumberProcessingResponseModel());
        }

    }
}
