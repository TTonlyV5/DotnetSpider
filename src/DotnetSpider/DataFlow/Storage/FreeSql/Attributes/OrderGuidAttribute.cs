using System;

namespace DotnetSpider.DataFlow.Storage.FreeSql.Attributes;

/// <summary>
/// 排序Guid特性
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class OrderGuidAttribute : Attribute
{
    public bool Enable { get; set; } = true;
}
