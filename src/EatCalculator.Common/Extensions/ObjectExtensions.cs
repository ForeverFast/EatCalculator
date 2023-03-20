using System.Text.Json;

namespace EatCalculator.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static T? Clone<T>(this T? source)
        {
            if (source == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(source));
        }
    }
}
