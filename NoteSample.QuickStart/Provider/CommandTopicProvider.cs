using ENode.Commanding;
using ENode.EQueue;

namespace NoteSample.QuickStart.Provider
{
    public class CommandTopicProvider:AbstractTopicProvider<ICommand>
    {
        public override string GetTopic(ICommand command)
        {
            return Constants.CommandTopic;
        }
    }
}
