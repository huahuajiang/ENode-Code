using ENode.Messageing;
using System;
using System.Collections.Generic;

namespace ENode.Commanding
{
    [Serializable]
    public abstract class Command : Message, ICommand
    {
        public string AggregateRootId { get; set; }

        public Command() : base() { }

        public Command(string aggregateRootId) : this(aggregateRootId,null)
        {

        }

        public Command(string aggregateRootId,IDictionary<string,string> items)
        {
            AggregateRootId = aggregateRootId ?? throw new AggregateException("aggregateRootId");
            Items = items;
        }
    }

    [Serializable]
    public abstract class Command<TAggregateRootId> : Message, ICommand
    {
        public TAggregateRootId AggregateRootId { get; set; }

        public Command() : base() { }

        public Command(TAggregateRootId aggregateRootId) : this(aggregateRootId, null)
        {

        }

        public Command(TAggregateRootId aggregateRootId,IDictionary<string,string> items)
        {
            if (aggregateRootId == null)
            {
                throw new AccessViolationException("aggregateRootId");
            }
            AggregateRootId = aggregateRootId;
            Items = items;
        }

        string ICommand.AggregateRootId
        {
            get
            {
                if (AggregateRootId != null)
                {
                    return AggregateRootId.ToString();
                }
                return null;
            }
        }
    }
}
