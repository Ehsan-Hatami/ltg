using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using LiveTixGroup.Extension;
using LiveTixGroup.Service.Mappers;

namespace LiveTixGroup;

[ExcludeFromCodeCoverage]
public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddServices();
        services.AddMemoryCache();
        services.AddCustomSwagger();

        var mapperConfiguration = new MapperConfiguration(x =>
        {
             x.AddProfile(new LtgEntitiesMapping());
        });

        mapperConfiguration.AssertConfigurationIsValid();
        services.AddSingleton(mapperConfiguration.CreateMapper());
        services.AddHttpClients(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/TechTestApi/swagger.json", ""));
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}