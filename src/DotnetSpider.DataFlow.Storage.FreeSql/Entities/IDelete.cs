namespace DotnetSpider.DataFlow.Storage.FreeSql.Entities;

/// <summary>
/// 删除接口
/// </summary>
public interface IDelete
{
    /// <summary>
    /// 是否删除
    /// </summary>
    bool IsDeleted { get; set; }
}