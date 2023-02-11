using Newtonsoft.Json;

namespace Locacao.Api.Framework;

public static class Converter
{
    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
    
    public static T FromJson<T>(this string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}