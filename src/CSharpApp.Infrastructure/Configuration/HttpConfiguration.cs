using CSharpApp.Infrastructure.Clients;
using Microsoft.Extensions.Options;
using CSharpApp.Infrastructure.Authentication;
using CSharpApp.Core.Interfaces.Authentication;

namespace CSharpApp.Infrastructure.Configuration;

public static class HttpConfiguration
{
    public static IServiceCollection AddHttpConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<IAccessTokenProvider, AccessTokenProvider>();
        services.AddTransient<BearerTokenHandler>();

        services.AddHttpClient<IProductsApiClient, ProductsApiClient>((serviceProvider, client) =>
        {
            var restApiSettings = serviceProvider.GetRequiredService<IOptions<RestApiSettings>>().Value;

            client.BaseAddress = new Uri(restApiSettings.BaseUrl!);
        })
        .AddHttpMessageHandler<BearerTokenHandler>();

         services.AddHttpClient<ICategoriesApiClient, CategoriesApiClient>((serviceProvider, client) =>
        {
            var restApiSettings = serviceProvider.GetRequiredService<IOptions<RestApiSettings>>().Value;

            client.BaseAddress = new Uri(restApiSettings.BaseUrl!);
        })
        .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IAuthApiClient, AuthApiClient>((serviceProvider, client) => 
        {
            var restApiSettings = serviceProvider.GetRequiredService<IOptions<RestApiSettings>>().Value;

            client.BaseAddress = new Uri(restApiSettings.BaseUrl!);
        });
        
        return services;
    }
}