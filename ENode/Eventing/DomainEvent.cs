using ENode.Messageing;

namespace ENode.Eventing
{
    /// <summary>
    /// 表示抽象泛型域事件
    /// </summary>
    /// <typeparam name="TAggregateRootId"></typeparam>
    public abstract class DomainEvent<TAggregateRootId>:Message,IDomainEvent<TAggregateRootId>
    {
        public TAggregateRootId _aggregateRootId;

        public string CommandId { get; set; }
        public TAggregateRootId AggregateRootId { get { return _aggregateRootId; } set { _aggregateRootId = value;AggregateRootStringId = value?.ToString(); } }
        public string AggregateRootStringId { get; set; }
        public string AggregateRootTypeName { get; set; }
        public int Version { get; set; }
        public int SpecVersion { get; set; }
        public int Sequence { get; set; }

        public DomainEvent() : base()
        {
            Version = 1;
            SpecVersion = 1;
            Sequence = 1;
        }
    }
}
