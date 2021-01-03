using ECommon.Serializing;
using ENode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using IQueueMessageHandler = EQueue.Clients.Consumers.IMessageHandler;

namespace ENode.EQueue.Command
{
    public class CommandConsumer: IQueueMessageHandler
    {
        private const string DefaultCommandConsumerGroup = "CommandConsumerGroup";
        private SendReplyService _sendRepllyService;
        private IJsonSerializer _jsonSerializer;
        private ITypeNameProvider _typeNameProvider;
        private ICommandProcessor
    }
}
