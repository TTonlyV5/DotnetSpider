using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DotnetSpider.Extensions;

namespace DotnetSpider.Extensions;

public static class EnumExtension
{
    public static string ToDescription(this Enum item, bool useName = true)
    {
        var name = item.ToString();
        var desc = item.GetType().GetField(name)?.GetCustomAttribute<DescriptionAttribute>(false);
        return useName ? desc?.Description ?? name : desc?.Description;
    }

    public static string ToNameWithDescription(this Enum item)
    {
        var name = item.ToString();
        var desc = item.GetType().GetField(name)?.GetCustomAttribute<DescriptionAttribute>(false);
        return $"{name}{(desc == null || desc.Description.IsNull() ? "" : $"({desc?.Description})")}";
    }

    public static long ToInt64(this Enum item)
    {
        return Convert.ToInt64(item);
    }

    public static List<Dictionary<string, object>> ToList(this Enum value, bool ignoreNull = false)
    {
        var enumType = value.GetType();

        if (!enumType.IsEnum)
            return null;

        return Enum.GetValues(enumType).Cast<Enum>()
            .Where(m => !ignoreNull || !m.ToString().Equals("Null")).Select(x => new Dictionary<string, object>
            {
                ["Label"] = x.ToDescription(),
                ["Value"] = x
            }).ToList();
    }

    public static List<Dictionary<string, object>> ToList<T>(bool ignoreNull = false)
    {
        var enumType = typeof(T);

        if (!enumType.IsEnum)
            return null;

        return Enum.GetValues(enumType).Cast<Enum>()
             .Where(m => !ignoreNull || !m.ToString().Equals("Null")).Select(x => new Dictionary<string, object>
             {
                 ["Label"] = x.ToDescription(),
                 ["Value"] = x
             }).ToList();
    }
}
