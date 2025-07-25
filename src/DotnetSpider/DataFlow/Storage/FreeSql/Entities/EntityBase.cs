namespace DotnetSpider.DataFlow.Storage.FreeSql.Entities;

/// <summary>
/// 实体基类
/// </summary>
public class EntityBase<TKey> : EntityDelete<TKey> where TKey : struct
{
}

/// <summary>
/// 实体基类
/// </summary>
public class EntityBase : EntityBase<long>
{
}
