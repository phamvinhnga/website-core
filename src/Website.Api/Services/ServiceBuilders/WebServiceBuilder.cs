
using Website.Api.Filters;

namespace Website.Api.Services.ServiceBuilders
{
    internal static class WebServiceBuilder
    {
        internal static void UseWebServiceBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(x => x.AddPolicy("AllowAll", builders => builders.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            services.AddControllers(options =>
            {
                options.Filters.Add<BadRequestExceptionFilter>();
                options.Filters.Add<UnauthorizedExceptionFilter>();
                options.Filters.Add<ArgumentNullExceptionFilter>();
            });
        }
    }
}
