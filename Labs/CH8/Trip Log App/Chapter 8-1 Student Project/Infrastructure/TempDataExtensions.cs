using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Chapter_8_1_Student_Project.Infrastructure;

public static class TempDataExtensions
{
    public static void Put<T>(this ITempDataDictionary tempData, string key, T value)
    {
        tempData[key] = JsonSerializer.Serialize(value);
    }

    public static T? Get<T>(this ITempDataDictionary tempData, string key)
    {
        if (!tempData.TryGetValue(key, out var o) || o is not string s || string.IsNullOrWhiteSpace(s))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(s);
    }

    public static T? Peek<T>(this ITempDataDictionary tempData, string key)
    {
        var s = tempData.Peek(key) as string;
        return string.IsNullOrWhiteSpace(s) ? default : JsonSerializer.Deserialize<T>(s);
    }
}
