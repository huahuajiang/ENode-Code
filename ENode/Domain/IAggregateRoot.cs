using ENode.Eventing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENode.Domain
{
    public interface IAggregateRoot
    {
        string UniqueId { get; set; }

        int Version { get; }

        IEnumerable<IDomainEvent> GetChanges();

        void AcceptChanges();

        void ReplayEvents(IEnumerable<DomainEventStream> eventStreams);
    }
}
