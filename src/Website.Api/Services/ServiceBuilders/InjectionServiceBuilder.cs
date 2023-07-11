using Website.Bal.Bases.Interfaces;
using Website.Bal.Interfaces;
using Website.Bal.Managers;
using Website.Biz.Managers;
using Website.Dal.Bases.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Dal.Repositories;
using Website.Dal.UnitOfWorks;
using Website.Entity.Models;
using Website.Entity.Repositories;
using Website.Shared.Entities;
using Website.Shared.Models;

namespace Website.Api.Services.ServiceBuilders
{
    internal static class InjectionServiceBuilder
    {
        internal static void UseInjectionServiceBuilder(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<IFileManager, FileManager>();

            // class room
            services.AddTransient<IClassRoomManager, ClassRoomManager>();
            services.AddTransient<IClassRoomRepository, ClassRoomRepository>();

            // Facility
            services.AddTransient<IFacilityManager, FacilityManager>();
            services.AddTransient<IFacilityRepository, FacilityRepository>();

            // gallery
            services.AddTransient<IGalleryManager, GalleryManager>();
            services.AddTransient<IGalleryRepository, GalleryRepository>();

            // post
            services.AddTransient<IPostManager, PostManager>();
            services.AddTransient<IPostRepository, PostRepository>();

            // parent
            services.AddTransient<IParentManager, ParentManager>();
            services.AddTransient<IParentRepository, ParentRepository>();

            // teacher
            services.AddTransient<ITeacherManager, TeacherManager>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();

            // Specialized
            services.AddTransient<ISpecializedRepository, SpecializedRepository>();
            services.AddTransient<ISpecializedManager, SpecializedManager>();

            //// category
            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
