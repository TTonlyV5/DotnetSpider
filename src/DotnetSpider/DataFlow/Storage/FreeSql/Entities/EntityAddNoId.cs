using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DotnetSpider.DataFlow.Storage.FreeSql.Attributes;
using FreeSql.DataAnnotations;

namespace DotnetSpider.DataFlow.Storage.FreeSql.Entities;

/// <summary>
/// 实体创建无主键Id
/// </summary>
public class EntityAddNoId : IEntityAdd
{
    /// <summary>
    /// 创建者用户Id
    /// </summary>
    [Description("创建者用户Id")]
    [Column(Position = -23, CanUpdate = false)]
    public virtual long? CreatedUserId { get; set; }

    /// <summary>
    /// 创建者用户名
    /// </summary>
    [Description("创建者用户名")]
    [Column(Position = -22, CanUpdate = false), MaxLength(60)]
    public virtual string CreatedUserName { get; set; }

    /// <summary>
    /// 创建者姓名
    /// </summary>
    [Description("创建者姓名")]
    [Column(Position = -21, CanUpdate = false), MaxLength(60)]
    public virtual string CreatedUserRealName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [Description("创建时间")]
    [Column(Position = -20, CanUpdate = false)]
    [ServerTime]
    public virtual DateTime? CreatedTime { get; set; }
}
