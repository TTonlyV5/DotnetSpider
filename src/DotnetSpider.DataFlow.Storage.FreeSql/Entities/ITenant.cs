namespace DotnetSpider.DataFlow.Storage.FreeSql.Entities;

/// <summary>
/// 租户接口
/// </summary>
public interface ITenant
{
    /// <summary>
    /// 租户Id
    /// </summary>
    long? TenantId { get; set; }
}