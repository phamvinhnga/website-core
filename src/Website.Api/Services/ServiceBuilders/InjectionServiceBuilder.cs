using Website.Bal.Bases.Interfaces;
using Website.Bal.Interfaces;
using Website.Bal.Managers;
using Website.Biz.Managers;
using Website.Dal.Bases.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Entity.Model;
using Website.Entity.Repositories;
using Website.Shared.Entities;
using Website.Shared.Models;

namespace Website.Api.Services.ServiceBuilders
{
    internal static class InjectionServiceBuilder
    {
        internal static void UseInjectionServiceBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            #region Manager
            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<IPostManager, PostManager>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<ITeacherManager, TeacherManager>();
            services.AddTransient<IParentManager, ParentManager>();
            services.AddTransient<IClassRoomManager, ClassRoomManager>();
            services.AddTransient<IBaseManager<Specialized, SpecializedInputModel, SpecializedOutputModel, int>, BaseManager<Specialized, SpecializedInputModel, SpecializedOutputModel, int>>();
            services.AddTransient<IBaseManager<Category, CategoryInputModel, CategoryOutputModel, int>, BaseManager<Category, CategoryInputModel, CategoryOutputModel, int>>();

            #endregion End Manager

            #region Repository
            services.AddTransient<ISpecializedRepository, SpecializedRepository>();

            // post
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IBaseRepository<Post, int>, BaseRepository<Post, int>>();

            services.AddTransient<IBaseRepository<Parent, int>, BaseRepository<Parent, int>>();
            services.AddTransient<IBaseRepository<Teacher, int>, BaseRepository<Teacher, int>>();
            services.AddTransient<IBaseRepository<Specialized, int>, BaseRepository<Specialized, int>>();
            #endregion End Repository
        }
    }
}
