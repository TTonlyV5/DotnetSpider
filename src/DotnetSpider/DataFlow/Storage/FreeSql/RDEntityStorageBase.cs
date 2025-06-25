using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetSpider.DataFlow.Storage.Entity;
using DotnetSpider.Infrastructure;
using FreeSql;
using Microsoft.Extensions.Logging;

namespace DotnetSpider.DataFlow.Storage.FreeSql;

/// <summary>
/// 关系型数据库保存实体解析结果
/// </summary>
public abstract class RDEntityStorageBase : DataFlowBase
{
    protected readonly IFreeSql _freeSql;
    private readonly Type _baseType = typeof(FreeSql.Entities.IEntity);

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="model">存储器类型</param>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="dataType">数据库类型</param>
    protected RDEntityStorageBase(StorageMode model, string connectionString, DataType dataType)
    {
        connectionString.NotNullOrWhiteSpace(nameof(connectionString));


        _freeSql = new FreeSqlBuilder()
                .UseConnectionString(dataType, connectionString)
                .UseAdoConnectionPool(true)
                .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))
                .UseAutoSyncStructure(true) //自动同步实体结构到数据库，只有CRUD时才会生成表
                .Build();



        Mode = model;
    }

    /// <summary>
    /// 存储器类型
    /// </summary>
    public StorageMode Mode { get; set; }

    /// <summary>
    /// 数据库操作重试次数
    /// </summary>
    public int RetryTimes { get; set; } = 600;

    /// <summary>
    /// 是否使用事务操作。默认不使用。
    /// </summary>
    public bool UseTransaction { get; set; }

    /// <summary>
    /// 数据库忽略大小写
    /// </summary>
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    ///
    /// </summary>
    /// <param name="context">数据流上下文</param>
    /// <param name="entities">数据解析结果 (数据类型, List(数据对象))</param>
    /// <returns></returns>
    protected abstract Task HandleAsync(DataFlowContext context, IDictionary<Type, IList<dynamic>> entities);

    public override async Task HandleAsync(DataFlowContext context, ResponseDelegate next)
    {
        if (IsNullOrEmpty(context))
        {
            Logger.LogWarning("数据流上下文不包含实体解析结果");
        }
        else
        {
            var data = context.Data;
            var result = new Dictionary<Type, IList<dynamic>>();
            foreach (var kv in data)
            {
                if (kv.Key is not Type type || !_baseType.IsAssignableFrom(type))
                {
                    continue;
                }

                if (kv.Value is IEnumerable list)
                {
                    foreach (var obj in list)
                    {
                        AddResult(result, type, obj);
                    }
                }
                else
                {
                    AddResult(result, type, kv.Value);
                }
            }

            await HandleAsync(context, result);
        }

        await next(context);
        //foreach (var kv in entities)
        //{
        //    var list = kv.Value;
        //    var firstItem = list.ElementAtOrDefault(0) as Entities.IEntity;
        //    if (firstItem == null)
        //    {
        //        continue;
        //    }


        //    for (var i = 0; i < RetryTimes; ++i)
        //    {
        //        try
        //        {
        //            if (UseTransaction)
        //            {

        //                using (var uow = _freeSql.CreateUnitOfWork()) // 创建显式事务
        //                {
        //                    try
        //                    {
        //                        await HandleEntities(list);
        //                        uow.Commit(); // 提交事务
        //                    }
        //                    catch
        //                    {
        //                        uow.Rollback(); // 回滚
        //                        throw;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                await HandleEntities(list);
        //            }

        //            break;
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger?.LogError(ex, "尝试插入数据失败");
        //            // 网络异常需要重试，并且不需要 Rollback
        //            if (!(ex.InnerException is System.IO.EndOfStreamException))
        //            {
        //                break;
        //            }
        //        }
        //    }


    }
    private void AddResult(IDictionary<Type, IList<dynamic>> dict, Type type, dynamic obj)
    {
        if (!_baseType.IsInstanceOfType(obj))
        {
            return;
        }

        if (!dict.ContainsKey(type))
        {
            dict.Add(type, new List<dynamic>());
        }

        dict[type].Add(obj);
    }
    private async Task HandleEntities(IList<dynamic> list)
    {
        switch (Mode)
        {
            case StorageMode.Insert:
                await _freeSql.Insert(list).ExecuteAffrowsAsync();
                break;
            case StorageMode.InsertIgnoreDuplicate:
                await _freeSql.Insert(list).NoneParameter().ExecuteAffrowsAsync(); // 非参数化提升性能[6](@ref)
                break;
            case StorageMode.Update:
                // 批量更新：单次操作替代循环
                await _freeSql.Update<object>().SetSource(list).ExecuteAffrowsAsync(); // [9](@ref)
                break;
            case StorageMode.InsertAndUpdate:
                // 批量 Upsert（FreeSql 内部优化为 MERGE 或 ON DUPLICATE KEY UPDATE）
                await _freeSql.InsertOrUpdate<object>().SetSource(list).ExecuteAffrowsAsync(); // [1,9](@ref)
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
