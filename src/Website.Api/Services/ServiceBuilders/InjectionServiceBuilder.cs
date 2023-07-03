using Website.Api.Services.ServiceBuilders;
using Website.Bal.Bases.Interfaces;
using Website.Bal.Interfaces;
using Website.Biz.Managers;
using Website.Dal.Bases.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Entity.Model;
using Website.Entity.Repositories;
using Website.Shared.Entities;
using static Mysqlx.Error.Types;

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
            services.AddTransient<IBaseManager<Specialized, SpecializedInputModel, SpecializedOutputModel, int>, BaseManager<Specialized, SpecializedInputModel, SpecializedOutputModel, int>>();
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
