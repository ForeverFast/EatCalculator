using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Text.RegularExpressions;

namespace Server.EntryPoints.WebApi.Swagger.Filters
{
    public class OptionalRouteParameterOperationFilter : IOperationFilter
    {
        const string captureName = "routeParameter";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var httpMethodAttributes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute>();

            var httpMethodWithOptional = httpMethodAttributes?.FirstOrDefault(m => m.Template?.Contains('?') ?? false);
            if (httpMethodWithOptional == null)
                return;

            string regex = $"{{(?<{captureName}>\\w+)\\?}}";

            var matches = Regex.Matches(httpMethodWithOptional.Template!, regex);

            foreach (var match in matches.Cast<Match>())
            {
                var name = match.Groups[captureName].Value;

                var parameter = operation.Parameters.FirstOrDefault(p => p.In == ParameterLocation.Path && p.Name == name);
                if (parameter != null)
                {
                    parameter.AllowEmptyValue = true;
                    parameter.Description = "Must check \"Send empty value\" or Swagger passes a comma for empty values otherwise";
                    parameter.Required = false;
                    //parameter.Schema.Default = new OpenApiString(string.Empty);
                    parameter.Schema.Nullable = true;
                }
            }
        }
    }
}
