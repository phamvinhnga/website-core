using AutoMapper;
using Website.Entity.Models;
using Website.Shared.Entities;
using Website.Shared.Extensions;
using Website.Shared.Models;

namespace Website.Shared.AutoMapper
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Specialized
            CreateMap<SpecializedInputModel, Specialized>();
            CreateMap<Specialized, SpecializedOutputModel>();
            #endregion Specialized

            #region Category
            CreateMap<CategoryInputModel, Category>();
            CreateMap<Category, CategoryOutputModel>();
            #endregion Category

            #region post
            CreateMap<PostInputModel, Post>()
                .ForMember(d => d.Thumbnail, o => o.Ignore());
            CreateMap<Post, PostOutputModel>()
                .ForMember(d => d.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail.ConvertFromJson<FileModel>()));
            #endregion post

            #region teacher
            CreateMap<TeacherInputModel, Teacher>()
                .ForMember(d => d.Thumbnail, o => o.Ignore());
            CreateMap<Teacher, TeacherOutputModel>()
                .ForMember(d => d.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail.ConvertFromJson<FileModel>()));
            #endregion teacher

            #region parent
            CreateMap<ParentInputModel, Parent>()
                .ForMember(d => d.Thumbnail, o => o.Ignore());
            CreateMap<Parent, ParentOutputModel>()
                .ForMember(d => d.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail.ConvertFromJson<FileModel>()));
            #endregion parent

            #region Gallery
            CreateMap<GalleryInputModel, Gallery>()
                .ForMember(d => d.Thumbnail, o => o.Ignore());
            CreateMap<Gallery, GalleryOutputModel>()
                .ForMember(d => d.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail.ConvertFromJson<FileModel>()));
            #endregion Gallery

            #region Facility
            CreateMap<FacilityInputModel, Facility>()
                .ForMember(d => d.Thumbnail, o => o.Ignore());
            CreateMap<Facility, FacilityOutputModel>()
                .ForMember(d => d.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail.ConvertFromJson<FileModel>()));
            #endregion Facility

            #region ClassRoom
            CreateMap<ClassRoomInputModel, ClassRoom>()
                .ForMember(d => d.Thumbnail, o => o.Ignore());
            CreateMap<ClassRoom, ClassRoomOutputModel>()
                .ForMember(d => d.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail.ConvertFromJson<FileModel>()));
            #endregion ClassRoom
        }
    }
}
