using AzureWebApi.Models;
using Data.Entities;
using Mapster;

namespace AzureWebApi.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ReviewModel, Review>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.ReviewId);
        }
    }
}
