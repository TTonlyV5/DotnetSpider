using ZhonTai.Admin.Core.Configs;

namespace DotnetSpider.DataFlow.Storage.FreeSql.Db.Data;

/// <summary>
/// 同步数据接口
/// </summary>
public interface ISyncData
{
    Task SyncDataAsync(IFreeSql db, DbConfig dbConfig = null, AppConfig appConfig = null);
}
