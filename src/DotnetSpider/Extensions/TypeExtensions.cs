using System;
using System.ComponentModel;
using System.Reflection;

namespace DotnetSpider.Extensions;

/// <summary>
/// 类型扩展
/// </summary>
public static class TypeExtensions
{
    public static string ToDescription(this Type type)
    {
        var desc = type?.GetCustomAttribute<DescriptionAttribute>(false);
        return desc?.Description;
    }
}
