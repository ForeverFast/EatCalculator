using Common.Wrappers;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Extensions
{
    public static class ResultExtensions
    {
        public static async Task<IResult<T>> ExecuteWithResultAsync<T>(this RestClient restClient, RestRequest restRequest, CancellationToken ctn = default)
        {
            var restResponse = await restClient.ExecuteAsync(restRequest, ctn);

            return restResponse.ToResult<T>();
        }

        public static IResult<T> ToResult<T>(this RestResponse response)
        {
            var responseAsString = response.Content!;
            var responseObject = JsonSerializer.Deserialize<Result<T>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return responseObject!;
        }

        public static IResult ToResult(this RestResponse response)
        {
            var responseAsString = response.Content!;
            var responseObject = JsonSerializer.Deserialize<Result>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return responseObject!;
        }

        //internal static async Task<PaginatedResult<T>> ToPaginatedResult<T>(this RestResponse response)
        //{
        //    var responseAsString = await response.Content.ReadAsStringAsync();
        //    var responseObject = JsonSerializer.Deserialize<PaginatedResult<T>>(responseAsString, new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    });
        //    return responseObject;
        //}
    }
}
