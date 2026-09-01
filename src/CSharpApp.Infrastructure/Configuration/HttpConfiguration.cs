using CSharpApp.Infrastructure.Clients;
using Microsoft.Extensions.Options;

namespace CSharpApp.Infrastructure.Configuration;

public static class HttpConfiguration
{
    public static IServiceCollection AddHttpConfiguration(this IServiceCollection services)
    {
        services.AddHttpClient<IProductsApiClient, ProductsApiClient>((serviceProvider, client) =>
        {
            var restApiSettings = serviceProvider.GetRequiredService<IOptions<RestApiSettings>>().Value;

            client.BaseAddress = new Uri(restApiSettings.BaseUrl!);
        });

        return services;
    }
}