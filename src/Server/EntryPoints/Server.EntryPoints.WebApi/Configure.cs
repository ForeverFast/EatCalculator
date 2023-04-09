using Server.Core.Interfaces;
using Server.EntryPoints.WebApi.Implementations;

namespace Server.EntryPoints.WebApi
{
    internal static class Configure
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IFileProvider, ServerWebApiFileProvider>();

            return services;
        }
    }
}
