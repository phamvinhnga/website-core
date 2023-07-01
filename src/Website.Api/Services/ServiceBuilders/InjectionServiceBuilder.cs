using Website.Bal.Interfaces;
using Website.Biz.Managers;
using Website.Dal.Interfaces;
using Website.Entity.Repositories;

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
            services.AddTransient<IParentManager, ParentManager>();
            #endregion End Manager

            #region Repository
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ISpecializedRepository, SpecializedRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<IParentRepository, ParentRepository>();
            #endregion End Repository
        }
    }
}
