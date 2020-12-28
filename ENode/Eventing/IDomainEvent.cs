using ENode.Messageing;

namespace ENode.Eventing
{
    /// <summary>
    /// 表示域事件
    /// </summary>
    public interface IDomainEvent:IMessage
    {
        /// <summary>
        /// 表示生成域事件的命令的id
        /// </summary>
        string CommandId { get; set; }
        /// <summary>
        /// 表示序列消息的聚合根字符串id
        /// </summary>
        string AggregateRootStringId { get; set; }
        /// <summary>
        /// 表示序列消息的聚合根种类名称
        /// </summary>
        string AggregateRootTypeName { get; set; }
        /// <summary>
        /// 表示序列消息的主版本
        /// </summary>
        int Version { get; set; }
        /// <summary>
        /// 表示事件结构版本
        /// </summary>
        int SpecVersion { get; set; }
        /// <summary>
        /// 表示消息的当前主版本的子序列
        /// </summary>
        int Sequence { get; set; }
    }
    /// <summary>
    /// 表示具有聚合根id的泛型类型的域事件
    /// </summary>
    /// <typeparam name="TAggregateRootId"></typeparam>
    public interface IDomainEvent<TAggregateRootId> : IDomainEvent
    {
        TAggregateRootId AggregateRootId { get; set; }
    }
}
