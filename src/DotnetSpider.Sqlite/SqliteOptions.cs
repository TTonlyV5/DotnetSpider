using DotnetSpider.DataFlow.Storage.Entity; // Assuming StorageMode is here
using Microsoft.Extensions.Configuration;

namespace DotnetSpider.Sqlite;

public class SqliteOptions
{
    private readonly IConfiguration _configuration;

    public SqliteOptions(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public StorageMode Mode
    {
        get
        {
            var modeStr = _configuration["Sqlite:Mode"]; // Changed from "Postgre:Mode"
            return string.IsNullOrWhiteSpace(modeStr)
                ? StorageMode.Insert
                : (StorageMode)Enum.Parse(typeof(StorageMode), modeStr, true); // Added ignoreCase for Enum.Parse
        }
    }

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public string ConnectionString => _configuration["Sqlite:ConnectionString"]; // Changed from "Postgre:ConnectionString"

    /// <summary>
    /// 数据库操作重试次数
    /// </summary>
    public int RetryTimes
    {
        get
        {
            var retryTimesStr = _configuration["Sqlite:RetryTimes"]; // Changed from "Postgre:RetryTimes"
            return string.IsNullOrWhiteSpace(retryTimesStr)
                ? 600
                : int.Parse(retryTimesStr);
        }
    }

    /// <summary>
    /// 是否使用事务操作。默认不使用。
    /// </summary>
    public bool UseTransaction
    {
        get
        {
            var useTransactionStr = _configuration["Sqlite:UseTransaction"]; // Changed from "Postgre:UseTransaction"
            // It's generally safer to default to false if the config is missing or malformed
            return !string.IsNullOrWhiteSpace(useTransactionStr) &&
                   bool.TryParse(useTransactionStr, out var result) && result;
        }
    }

    /// <summary>
    /// 数据库忽略大小写
    /// </summary>
    public bool IgnoreCase
    {
        get
        {
            var ignoreCaseStr = _configuration["Sqlite:IgnoreCase"]; // Changed from "Postgre:IgnoreCase"
            // Defaulting to a sensible value (e.g., true or false based on typical SQLite behavior or preference)
            // SQLite identifiers are typically case-insensitive by default.
            return !string.IsNullOrWhiteSpace(ignoreCaseStr) &&
                   bool.TryParse(ignoreCaseStr, out var result) ? result : true; // Default to true if not specified or invalid
        }
    }
}
