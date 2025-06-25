using DotnetSpider.DataFlow;
using DotnetSpider.DataFlow.Storage.Entity;
using FreeSql;
using Microsoft.Extensions.Configuration;

namespace DotnetSpider.Sqlite;

/// <summary>
/// Sqlite 保存解析(实体)结果
/// </summary>
public class SqliteEntityStorage : DataFlow.Storage.FreeSql.RDEntityStorageBase
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="model">存储器类型</param>
    /// <param name="connectionString">连接字符串</param>
    public SqliteEntityStorage(StorageMode model, string connectionString)
        : base(model, connectionString, DataType.Sqlite)
    {
    }

    public static IDataFlow CreateFromOptions(IConfiguration configuration)
    {
        var options = new SqliteOptions(configuration);
        return new SqliteEntityStorage(options.Mode, options.ConnectionString)
        {
            UseTransaction = options.UseTransaction,
            IgnoreCase = options.IgnoreCase,
            RetryTimes = options.RetryTimes
        };
    }
    public override Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

}


