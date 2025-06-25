using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DotnetSpider.DataFlow.Storage.FreeSql.Db.Data;

/// <summary>
/// 属性解析器
/// </summary>
public class PropsContractResolver : CamelCasePropertyNamesContractResolver
{
    private bool _ignore;
    private List<string> _propNames = null;

    public PropsContractResolver(List<string> propNames = null, bool ignore = true)
    {
        _propNames = propNames;
        _ignore = ignore;
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        if (_propNames != null && _propNames.Contains(member.Name))
        {
            return _ignore ? null : base.CreateProperty(member, memberSerialization);
        }

        return base.CreateProperty(member, memberSerialization);
    }
}
