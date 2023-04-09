using DALQueryChain.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Core.Context;
using Server.Core.Initializers;
using Server.Core.Interfaces.Services;
using Server.Core.Services;

namespace Server.Core
{
    public static class Configure
    {
        public static IServiceCollection AddServerCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ServerEatCalculatorDbContext>(opt =>
            {
                var connectionString = configuration.GetConnectionString("Default");
                opt.UseSqlite(connectionString);
            });

            services.AddQueryChain(typeof(Configure).Assembly);

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserDataService, UserDataService>();

            services.AddAsyncInitializer<DbInitializer>();

            return services;
        }

        public static WebApplication UseServerCore(this WebApplication app)
        {
            app.InitAsync();

            return app;
        }
    }
}
