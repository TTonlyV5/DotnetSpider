namespace DotnetSpider.DataFlow.Storage.FreeSql.Attributes;

/// <summary>
/// 瞬时注入
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class InjectTransientAttribute : Attribute
{
}