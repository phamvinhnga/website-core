using Website.Bal.Interfaces;
using Website.Biz.Managers;
using Website.Dal.Bases.Interfaces;
using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Entity.Repositories;
using Website.Shared.Entities;

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
            services.AddTransient<ISpecializedManager, SpecializedManager>();
            services.AddTransient<ITeacherManager, TeacherManager>();
            #endregion End Manager

            #region Repository
            services.AddTransient<ISpecializedRepository, SpecializedRepository>();
            services.AddTransient<IBaseRepository<Specialized, int>, BaseRepository<Specialized, int>>();

            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IBaseRepository<Post, int>, BaseRepository<Post, int>>();

            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<IBaseRepository<Teacher, int>, BaseRepository<Teacher, int>>();
            #endregion End Repository
        }
    }
}
