using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetSpider.DataFlow.Storage.FreeSql.Entities;
using DotnetSpider.Extensions;
using Microsoft.Extensions.Logging;

namespace DotnetSpider.DataFlow.Storage.FreeSql;

/// <summary>
/// 实体存储器
/// </summary>
public abstract class OrmEntityStorageBase : DataFlowBase
{
    private readonly Type _baseType = typeof(Entities.IEntity<>);

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
                Type GetTargetType(object key)
                {
                    // 1. 如果 Key 直接是 Type 类型
                    if (key is Type type)
                        return type;

                    // 2. 如果 Key 是类型全名字符串（如 "YourNamespace.News"）
                    if (key is string typeName)
                    {
                        // 尝试直接获取类型
                        Type typeByName = Type.GetType(typeName);

                        // 如果未找到，尝试在当前程序集和所有已加载程序集中查找
                        if (typeByName == null)
                        {
                            typeByName = AppDomain.CurrentDomain.GetAssemblies()
                                .Select(asm => asm.GetType(typeName))
                                .FirstOrDefault(t => t != null);
                        }

                        return typeByName;
                    }

                    // 3. 如果 Key 是对象实例，获取其运行时类型
                    return key?.GetType();
                }

                // 获取类型
                Type type = GetTargetType(kv.Key);

                // 检查是否实现 IEntity<> 或继承 EntityBase
                bool isValidEntity = type != null && (
                    typeof(IEntity<>).IsAssignableFromGeneric(type) ||  // 实现 IEntity<>
                    typeof(EntityBase).IsAssignableFrom(type)          // 继承 EntityBase
                );

                if (!isValidEntity)
                {
                    continue; // 跳过不符合的类型
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
    }

    /// <summary>
    /// 增强的类型检查方法
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private bool IsValidEntity(object obj)
    {
        if (obj == null) return false;

        // 1. 首先检查是否是_baseType的实例
        if (_baseType.IsInstanceOfType(obj))
            return true;

        // 2. 检查是否实现IEntity<>接口
        var objType = obj.GetType();
        return objType.GetInterfaces()
            .Any(i => i.IsGenericType &&
                     i.GetGenericTypeDefinition() == typeof(IEntity<>));
    }
    private void AddResult(IDictionary<Type, IList<dynamic>> dict, Type type, dynamic obj)
    {
        if (!IsValidEntity(obj))
        {
            Logger.LogDebug($"对象类型 {obj?.GetType().Name} 不符合实体要求");
            return;
        }

        if (!dict.ContainsKey(type))
        {
            dict.Add(type, new List<dynamic>());
        }

        dict[type].Add(obj);
    }
}
