using System;

namespace DotnetSpider.DataFlow.Storage.FreeSql.Attributes;

/// <summary>
/// 雪花算法特性
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SnowflakeAttribute : Attribute
{
    public bool Enable { get; set; } = true;
}
