using System.Net.Http.Headers;
using LiveTixGroup.Service;
using Microsoft.OpenApi.Models;

namespace LiveTixGroup.Extension;

public static class ServiceRegistration
{
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddTransient<IPhotoAlbumGetter, PhotoAlbumGetter>();
		services.AddTransient<IAggregator, Aggregator>();
		services.AddTransient<IHttpCallHandler, HttpCallHandler>();
		return services;
	}

	public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient("placeholderEndpoint", (x =>
		{
			x.BaseAddress = new Uri(configuration["HttpClients:PlaceholderEndpoint"]);
			x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}));
		
		return services;
	}

	public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("TechTestApi", new OpenApiInfo
			{
				Title = "Live Tix Group Tech Test API",
			});
			
			c.CustomSchemaIds(x => x.ToString());
			c.EnableAnnotations();
		});

		return services;
	}

}