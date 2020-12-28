using ENode.Eventing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENode.Domain
{
    public interface IAggregateRootInternalHanderProvider
    {
        Action<IAggregateRoot, IDomainEvent> GetInternalEventHandler(Type aggregateRootType, Type eventType);
    }
}
