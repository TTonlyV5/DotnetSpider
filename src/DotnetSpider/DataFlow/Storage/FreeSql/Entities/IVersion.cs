namespace DotnetSpider.DataFlow.Storage.FreeSql.Entities;

/// <summary>
/// 版本接口
/// </summary>
public interface IVersion
{
    /// <summary>
    /// 数据版本
    /// </summary>
    long Version { get; set; }
}