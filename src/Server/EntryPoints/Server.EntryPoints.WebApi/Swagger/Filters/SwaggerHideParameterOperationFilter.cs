using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Server.EntryPoints.WebApi.Swagger.Filters
{
    public class SwaggerHideParameterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var target = operation.Parameters.FirstOrDefault(x => x.In == ParameterLocation.Header && x.Name == "AuthorizedUserId");
            if (target != null)
                operation.Parameters.Remove(target);
        }
    }
}
