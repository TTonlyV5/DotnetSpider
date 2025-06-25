namespace DotnetSpider.DataFlow.Storage.FreeSql.Entities;

/// <summary>
/// 会员接口
/// </summary>
public interface IMember
{
    /// <summary>
    /// 顾客Id
    /// </summary>
    long? MemberId { get; set; }
}