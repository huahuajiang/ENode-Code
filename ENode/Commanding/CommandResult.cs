using System;

namespace ENode.Commanding
{
    [Serializable]
    public class CommandResult
    {
        public CommandStatus Status { get; private set; }

        public string CommandId { get; private set; }

        public string AggregateRootId { get; private set; }

        public string Result { get; private set; }

        public string ResultType { get; private set; }

        public CommandResult() { }

        public CommandResult(CommandStatus status,string commandId,string aggregateRootId,string result=null,string resultType = null)
        {
            Status = status;
            CommandId = commandId;
            AggregateRootId = aggregateRootId;
            Result = result;
            ResultType = resultType;
        }

        public override string ToString()
        {
            return string.Format("[CommandId={0},Status={1},AggregateRootId={2},Result={3},ResultType={4}]",
                CommandId,
                Status,
                AggregateRootId,
                Result,
                ResultType);
        }
    }

    public enum CommandStatus
    {
        None=0,
        Success=1,
        NothingChanged=2,
        Failed=3
    }
}
