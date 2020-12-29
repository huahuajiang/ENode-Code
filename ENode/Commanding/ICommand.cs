using ENode.Messageing;

namespace ENode.Commanding
{
    public interface ICommand:IMessage
    {
        string AggregateRootId { get; }
    }
}
