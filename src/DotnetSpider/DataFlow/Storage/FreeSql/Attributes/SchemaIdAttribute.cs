using System;

namespace DotnetSpider.DataFlow.Storage.FreeSql.Attributes;

/// <summary>
/// SchemaId特性
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
public class SchemaIdAttribute : Attribute
{
    /// <summary>
    /// 前缀
    /// </summary>
    public string Prefix { get; set; }

    /// <summary>
    /// 后缀
    /// </summary>
    public string Suffix { get; set; }

    /// <summary>
    /// SchemaId
    /// </summary>
    public string SchemaId { get; set; }

    public SchemaIdAttribute()
    {
    }

    public SchemaIdAttribute(string schemaId)
    {
        SchemaId = schemaId;
    }
}
