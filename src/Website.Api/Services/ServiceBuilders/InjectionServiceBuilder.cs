using Website.Bal.Bases.Interfaces;
using Website.Bal.Interfaces;
using Website.Bal.Managers;
using Website.Biz.Managers;
using Website.Dal.Bases.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Dal.Repositories;
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
            services.AddTransient<IBaseManager<Specialized, SpecializedInputModel, SpecializedOutputModel, int>, BaseManager<Specialized, SpecializedInputModel, SpecializedOutputModel, int>>();
            services.AddTransient<IBaseRepository<Specialized, int>, BaseRepository<Specialized, int>>();
            services.AddTransient<ISpecializedRepository, SpecializedRepository>();

            // category
            services.AddTransient<IBaseManager<Category, CategoryInputModel, CategoryOutputModel, int>, BaseManager<Category, CategoryInputModel, CategoryOutputModel, int>>();
            services.AddTransient<IBaseRepository<Category, int>, BaseRepository<Category, int>>();
        }
    }
}
