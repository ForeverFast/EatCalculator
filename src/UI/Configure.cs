using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UI
{
    public static class Configure
    {
        public static IServiceCollection AddValidators(this IServiceCollection services, params Assembly[] assemblies)
        {
            var types = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x
                    => !x.IsAbstract
                    && (x.BaseType?.IsGenericType ?? false)
                    && x.BaseType?.GetGenericTypeDefinition() == (typeof(BaseValidator<>)))
                .ToList();
            types.ForEach(item => services.AddScoped(item.BaseType!, item));

            return services;
        }
    }
}
