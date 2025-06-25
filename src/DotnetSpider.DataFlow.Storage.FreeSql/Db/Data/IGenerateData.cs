using ZhonTai.Admin.Core.Configs;

namespace DotnetSpider.DataFlow.Storage.FreeSql.Db.Data;

/// <summary>
/// 生成数据接口
/// </summary>
public interface IGenerateData
{
    Task GenerateDataAsync(IFreeSql db, AppConfig appConfig);
}
