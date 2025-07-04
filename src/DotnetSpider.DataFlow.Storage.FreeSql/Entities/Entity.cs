using DotnetSpider.DataFlow.Storage.FreeSql.Attributes;
using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DotnetSpider.DataFlow.Storage.FreeSql.Entities;

/// <summary>
/// 实体接口
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IEntity<TKey>
{
    /// <summary>
    /// 主键Id
    /// </summary>
    TKey Id { get; set; }
}

/// <summary>
/// 实体接口
/// </summary>
public interface IEntity : IEntity<long>
{
}

/// <summary>
/// 实体
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class Entity<TKey> : IEntity<TKey>
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Description("主键Id")]
    [Snowflake]
    [Column(Position = 1, IsIdentity = false, IsPrimary = true)]
    [JsonProperty(Order = -30)]
    [JsonPropertyOrder(-30)]
    public virtual TKey Id { get; set; }
}

/// <summary>
/// 实体
/// </summary>
public class Entity : Entity<long>
{
}
