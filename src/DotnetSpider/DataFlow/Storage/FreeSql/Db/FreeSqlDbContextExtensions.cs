//using FreeScheduler;

//namespace DotnetSpider.DataFlow.Storage.FreeSql.Db;

///// <summary>
///// FreeSqlDbContext扩展类
///// </summary>
//public static class FreeSqlDbContextExtensions
//{
//    /// <summary>
//    /// 返回默认仓库类
//    /// </summary>
//    /// <typeparam name="TEntity"></typeparam>
//    /// <typeparam name="TKey"></typeparam>
//    /// <param name="that"></param>
//    /// <returns></returns>
//    public static IRepositoryBase<TEntity, TKey> GetRepositoryBase<TEntity, TKey>(this IFreeSql that) where TEntity : class
//    {
//        return new RepositoryBase<TEntity, TKey>(that);
//    }

//    /// <summary>
//    /// 返回默认仓库类，适用联合主键的仓储类
//    /// </summary>
//    /// <typeparam name="TEntity"></typeparam>
//    /// <param name="that"></param>
//    /// <returns></returns>
//    public static IRepositoryBase<TEntity, long> GetRepositoryBase<TEntity>(this IFreeSql that) where TEntity : class
//    {
//        return new RepositoryBase<TEntity, long>(that);
//    }

//    /// <summary>
//    /// 同步调度结构
//    /// </summary>
//    /// <param name="that"></param>
//    /// <param name="dbConfig"></param>
//    /// <param name="configureFreeSql"></param>
//    public static void SyncSchedulerStructure(this IFreeSql that, DbConfig dbConfig, Action<IFreeSql> configureFreeSql = null)
//    {
//        that.CodeFirst
//        .ConfigEntity<TaskInfo>(a =>
//        {
//            a.Name("base_task");
//            a.Property(b => b.Id).IsPrimary(true);
//            a.Property(b => b.Body).StringLength(-1);
//            a.Property(b => b.Interval).MapType(typeof(int));
//            a.Property(b => b.IntervalArgument).StringLength(1024);
//            a.Property(b => b.Status).MapType(typeof(int));
//            a.Property(b => b.CreateTime).ServerTime(DateTimeKind.Local);
//            a.Property(b => b.LastRunTime).ServerTime(DateTimeKind.Local);
//        })
//        .ConfigEntity<TaskLog>(a =>
//        {
//            a.Name("base_task_log");
//            a.Property(b => b.Exception).StringLength(-1);
//            a.Property(b => b.Remark).StringLength(-1);
//            a.Property(b => b.CreateTime).ServerTime(DateTimeKind.Local);
//        })
//        .ConfigEntity<TaskInfoExt>(a =>
//        {
//            a.Name("base_task_ext");
//            a.Property(b => b.TaskId).IsPrimary(true);
//            a.Property(b => b.CreatedTime).CanUpdate(false).ServerTime(DateTimeKind.Local);
//            a.Property(b => b.ModifiedTime).CanInsert(false).ServerTime(DateTimeKind.Local);
//        });

//        configureFreeSql?.Invoke(that);

//        if (dbConfig.SyncStructure)
//        {
//            that.CodeFirst.SyncStructure<TaskInfo>();
//            that.CodeFirst.SyncStructure<TaskLog>();
//            that.CodeFirst.SyncStructure<TaskInfoExt>();
//        }

//    }
//}
