using System;

namespace DotnetSpider.DataFlow.Storage.FreeSql.Attributes;

/// <summary>
/// 作用域注入
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class InjectScopedAttribute : Attribute
{
}
