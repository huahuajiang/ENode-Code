using ENode.Eventing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENode.Domain
{
    [Serializable]
    public abstract class AggregateRoot<TAggregateRootId> : IAggregateRoot
    {
        private static readonly IList<IDomainEvent> _emptyEvents = new List<IDomainEvent>();
        private static IAggregateRoot

        public string UniqueId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Version => throw new NotImplementedException();

        public void AcceptChanges()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDomainEvent> GetChanges()
        {
            throw new NotImplementedException();
        }

        public void ReplayEvents(IEnumerable<DomainEventStream> eventStreams)
        {
            throw new NotImplementedException();
        }
    }
}
