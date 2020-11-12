using AutoMapper;
using LocalizaLabs.Api.v1.Models.NumberProcessing;
using LocalizaLabs.Domain.Entities;
using System.Collections.Generic;

namespace LocalizaLabs.Api.v1.Configurations.Mappers
{
    public static class NumberProcessingMapper
    {
        internal static IMapper Mapper { get; }

        static NumberProcessingMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<NumberProcessingMapperProfile>())
                .CreateMapper();
        }

        public static NumberProcessingResponseModel ToModel(this NumberProcessingResult entity)
        {
            return entity == null ? null : Mapper.Map<NumberProcessingResponseModel>(entity);
        }
    }
}
