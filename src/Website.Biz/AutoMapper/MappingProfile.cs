using Website.Biz.Dto;
using Website.Entity.Model;
using Website.Entity.Entities;
using AutoMapper;
using Website.Shared.Extensions;

namespace Website.Biz.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region user
            CreateMap<UserSignUpInputDto, UserSignUpInputModel>();
            CreateMap<UserSignUpInputModel, User>()
                .ForMember(d => d.PasswordHash, o => o.Ignore());
            CreateMap<UserSignInOutputModel, UserSignInOutputDto>();
            CreateMap<User, CurrentUserOutputModel>();
            CreateMap<CurrentUserOutputModel, CurrentUserOutputDto>();
            CreateMap<StaffRegisterInputDto, StaffRegisterInputModel>();
            CreateMap<StaffRegisterInputModel, User>()
                .ForMember(d => d.PasswordHash, o => o.Ignore());
            CreateMap<User, StaffOutputModel>();
            CreateMap<StaffOutputModel, StaffOutputDto>();
            #endregion user

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

            CreateMap<Specialized, SpecializedOutputModel>().ReverseMap();
        }
    }
}
