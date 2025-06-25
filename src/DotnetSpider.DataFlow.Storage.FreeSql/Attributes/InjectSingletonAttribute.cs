namespace DotnetSpider.DataFlow.Storage.FreeSql.Attributes;

/// <summary>
/// 单例注入
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class InjectSingletonAttribute : Attribute
{
}