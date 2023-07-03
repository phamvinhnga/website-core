using AutoMapper;
using Website.Entity.Model;
using Website.Shared.Entities;

namespace Website.Shared.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SpecializedInputModel, Specialized>();
        }
    }
}
