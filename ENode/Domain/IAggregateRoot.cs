using ENode.Eventing;
using System.Collections.Generic;

namespace ENode.Domain
{
    public interface IAggregateRoot
    {
        string UniqueId { get; }

        int Version { get; }

        IEnumerable<IDomainEvent> GetChanges();

        void AcceptChanges();

        void ReplayEvents(IEnumerable<DomainEventStream> eventStreams);
    }
}
