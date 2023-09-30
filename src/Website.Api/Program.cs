using Website.Api.Services.ServiceBuilders;
using Website.Shared.Bases.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add log
builder.Host.UseSwaggerSerilogBuilder();

// Add services to the container.
builder.Services.AddOptions<DbContextConnectionSettingOptions>()
    .Bind(builder.Configuration
    .GetSection(DbContextConnectionSettingOptions.Position))
    .ValidateDataAnnotations();
builder.Services.AddOptions<FileUploadSettingOptions>()
    .Bind(builder.Configuration
    .GetSection(FileUploadSettingOptions.Position))
    .ValidateDataAnnotations();
builder.Services.AddOptions<JWTSettingOptions>()
    .Bind(builder.Configuration
    .GetSection(JWTSettingOptions.Position))
    .ValidateDataAnnotations();
builder.Services.UseSwaggerServiceBuilder(configuration);
builder.Services.UseSqlServiceBuilder(configuration);
builder.Services.UseMigrationServiceBuilder(configuration);
builder.Services.UseAutoMapperServiceBuilder(configuration);
builder.Services.UseInjectionServiceBuilder(configuration);
builder.Services.UseWebServiceBuilder(configuration);
builder.Services.UseAuthServiceBuilder(configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsStaging() || app.Environment.IsDevelopment())
{
    app.UseSwaggerApplicationBuilder(configuration);
}
app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
