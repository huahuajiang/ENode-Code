using ECommon.Components;
using ECommon.Utilities;
using ENode.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ENode.Domain
{
    [Serializable]
    public abstract class AggregateRoot<TAggregateRootId> : IAggregateRoot
    {
        private static readonly IList<IDomainEvent> _emptyEvents = new List<IDomainEvent>();
        private static IAggregateRootInternalHandlerProvider _eventHandlerProvider;
        private Queue<IDomainEvent> _uncommittedEvents;
        private TAggregateRootId _id;
        protected int _version;

        public TAggregateRootId Id { get => _id; set => _id = value; }

        protected AggregateRoot()
        {
            _uncommittedEvents = new Queue<IDomainEvent>();
        }

        protected AggregateRoot(TAggregateRootId id) : this()
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            _id = id;
        }

        protected AggregateRoot(TAggregateRootId id,int version) : this(id)
        {
            if (version < 0)
            {
                throw new AggregateException(string.Format("Version cannot small than zero, aggregateRootId: {0}, version: {1}", id, version));
            }
            _version = version;
        }

        public TRole ActAs<TRole>() where TRole : class
        {
            if (!typeof(TRole).IsInterface)
            {
                throw new Exception(string.Format("'{0}' is not an interface type.", typeof(TRole).FullName));
            }

            var actor = this as TRole;

            if (actor == null)
            {
                throw new Exception(string.Format("'{0}' cannot act as role '{1}'.", GetType().FullName, typeof(TRole).FullName));
            }
            return actor;
        }

        protected void ApplyEvent(IDomainEvent<TAggregateRootId> domainEvent)
        {
            if (domainEvent == null)
            {
                throw new ArgumentNullException("domainEvent");
            }
            if (Equals(this._id, default(TAggregateRootId))){
                throw new Exception("Aggregate root id cannot be null.");
            }
            domainEvent.AggregateRootId = _id;
            domainEvent.Version = _version + 1;
            HandleEvent(domainEvent);
            AppendUncommittedEvent(domainEvent);
        }

        private void HandleEvent(IDomainEvent domainEvent)
        {
            if (_eventHandlerProvider == null)
            {
                _eventHandlerProvider = ObjectContainer.Resolve<IAggregateRootInternalHandlerProvider>();
            }
            var handler = _eventHandlerProvider.GetInternalEventHandler(GetType(), domainEvent.GetType());
            if (handler == null)
            {
                throw new Exception(string.Format("Could not find event handler for [{0}] of [{1}]", domainEvent.GetType().FullName, GetType().FullName));
            }
            if(Equals(this._id,default(TAggregateRootId))&& domainEvent.Version == 1)
            {
                this._id = TypeUtils.ConvertType<TAggregateRootId>(domainEvent.AggregateRootStringId);
            }
            handler(this, domainEvent);
        }

        private void AppendUncommittedEvent(IDomainEvent<TAggregateRootId> domainEvent)
        {
            if (_uncommittedEvents == null)
            {
                _uncommittedEvents = new Queue<IDomainEvent>();
            }
            if (_uncommittedEvents.Any(x => x.GetType() == domainEvent.GetType()))
            {
                throw new InvalidOperationException(string.Format("Cannot apply duplicated domain event type: {0}, current aggregateRoot type: {1}, id: {2}", domainEvent.GetType().FullName, this.GetType().FullName, _id));
            }
            _uncommittedEvents.Enqueue(domainEvent);
        }

        private void VerifyEvent(DomainEventStream eventStream)
        {
            var current = this as IAggregateRoot;
            if (eventStream.Version > 1 && eventStream.AggregateRootId != current.UniqueId)
            {
                throw new InvalidOperationException(string.Format("Invalid domain event stream, aggregateRootId:{0}, expected aggregateRootId:{1}, type:{2}", eventStream.AggregateRootId, current.UniqueId, current.GetType().FullName));
            }
            if (eventStream.Version != current.Version + 1)
            {
                throw new InvalidOperationException(string.Format("Invalid domain event stream, version:{0}, expected version:{1}, current aggregateRoot type:{2}, id:{3}", eventStream.Version, current.Version, this.GetType().FullName, current.UniqueId));
            }
        }

         string IAggregateRoot.UniqueId
        {
            get
            {
                if (Id != null)
                {
                    return Id.ToString();
                }
                return null;
            }
        }

        int IAggregateRoot.Version
        {
            get { return _version; }
        }

        void IAggregateRoot.AcceptChanges()
        {
            if (_uncommittedEvents != null && _uncommittedEvents.Any())
            {
                _version = _uncommittedEvents.First().Version;
                _uncommittedEvents.Clear();
            }
        }

        IEnumerable<IDomainEvent> IAggregateRoot.GetChanges()
        {
            if (_uncommittedEvents == null)
            {
                return _emptyEvents;
            }
            return _uncommittedEvents.ToArray();
        }

        void IAggregateRoot.ReplayEvents(IEnumerable<DomainEventStream> eventStreams)
        {
            if (eventStreams == null) return;

            foreach(var eventStream in eventStreams)
            {
                VerifyEvent(eventStream);
                foreach(var domainEvent in eventStream.Events)
                {
                    HandleEvent(domainEvent);
                }
                _version = eventStream.Version;
            }
        }
    }
}
