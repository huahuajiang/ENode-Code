using ENode.Eventing;
using System;

namespace ENode.Domain
{
    public interface IAggregateRootInternalHandlerProvider
    {
        Action<IAggregateRoot, IDomainEvent> GetInternalEventHandler(Type aggregateRootType, Type eventType);
    }
}
