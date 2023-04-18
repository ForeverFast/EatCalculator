using Client.Core.Shared.Api.HttpClient.Endpoints;
using Client.Core.Shared.Api.HttpClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Client.Core.Shared.Api.HttpClient
{
    internal static class Configure
    {
        public static ClientAppBuilder ConfigureHttpClient(this ClientAppBuilder app)
        {
            app.Services
                .Configure<HttpEndpointsClientSettings>(app.Configuration.GetSection(nameof(HttpEndpointsClientSettings)))
                .AddHttpClient(nameof(HttpEndpointsClient), (serviceProvider, httpClient) =>
                {
                    var options = serviceProvider.GetRequiredService<IOptions<HttpEndpointsClientSettings>>().Value;
                    httpClient.BaseAddress = new Uri(options.BaseAddress);
                });

            app.Services
                .AddSingleton((serviceProvider) =>
                {
                    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient(nameof(HttpEndpointsClient));
                    return new RestClient(httpClient);
                })
                .AddSingleton<AccountHttpEndpoints>()
                .AddSingleton<UserEatDataHttpEndpoints>()
                .AddSingleton<HttpEndpointsClient>();

            return app;
        }
    }
}
