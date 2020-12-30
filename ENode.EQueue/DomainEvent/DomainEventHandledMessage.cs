using System;

namespace ENode.EQueue.DomainEvent
{
    [Serializable]
    public class DomainEventHandledMessage
    {
        public string CommandId { get; set; }
        public string AggregateRootId { get; set; }
        public string CommandResult { get; set; }
    }
}
