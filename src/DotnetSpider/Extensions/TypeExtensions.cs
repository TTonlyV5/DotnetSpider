using System;
using System.ComponentModel;
using System.Linq;
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


    public static bool IsAssignableFromGeneric(this Type genericType, Type type)
    {
        return type.GetInterfaces().Any(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == genericType
        ) || (
            type.IsGenericType &&
            type.GetGenericTypeDefinition() == genericType
        ) || (
            type.BaseType != null &&
            type.BaseType.IsGenericType &&
            type.BaseType.GetGenericTypeDefinition() == genericType
        );
    }


}
