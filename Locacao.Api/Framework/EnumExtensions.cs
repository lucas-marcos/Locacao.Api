using System.ComponentModel;

namespace Locacao.Api.Framework;

public static class EnumExtensions
{
    public static string GetDescription<T>(this T value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }

    public static T ToEnum<T>(this string value) where T : struct, Enum
    {
        if (Enum.TryParse<T>(value, out T result))
            return result;
        
        throw new ArgumentException($"'{value}' não é um valor válido para {typeof(T).Name}");
    }
}