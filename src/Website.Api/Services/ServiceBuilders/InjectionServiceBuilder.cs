using Website.Bal.Bases.Interfaces;
using Website.Bal.Interfaces;
using Website.Bal.Managers;
using Website.Biz.Managers;
using Website.Dal.Bases.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
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
            services.AddTransient<IBaseRepository<ClassRoom, int>, BaseRepository<ClassRoom, int>>();

            // gallery
            services.AddTransient<IGalleryManager, GalleryManager>();
            services.AddTransient<IBaseRepository<Gallery, int>, BaseRepository<Gallery, int>>();

            // post
            services.AddTransient<IPostManager, PostManager>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IBaseRepository<Post, int>, BaseRepository<Post, int>>();

            // parent
            services.AddTransient<IParentManager, ParentManager>();
            services.AddTransient<IBaseRepository<Parent, int>, BaseRepository<Parent, int>>();

            // teacher
            services.AddTransient<ITeacherManager, TeacherManager>();
            services.AddTransient<IBaseRepository<Teacher, int>, BaseRepository<Teacher, int>>();

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
