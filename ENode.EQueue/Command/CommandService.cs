using ECommon.Components;
using ECommon.IO;
using ECommon.Logging;
using ECommon.Serializing;
using ECommon.Utilities;
using ENode.Commanding;
using ENode.Infrastructure;
using EQueue.Clients.Producers;
using System;
using System.Text;
using System.Threading.Tasks;
using EQueueMessage = EQueue.Protocols.Message;

namespace ENode.EQueue.Command
{
    public class CommandService : ICommandService
    {
        private ILogger _logger;
        private IJsonSerializer _jsonSerializer;
        private ITopicProvider<ICommand> _commandTopicProvider;
        private ITypeNameProvider _typeNameProvider;
        private SendQueueMessageService _sendMessageService;
        private CommandResultProcessor _commandResultProcessor;
        private IOHelper _ioHelper;

        public string CommandExecutedMessageTopic { get; private set; }

        public string DomainEventHandledMessageTopic { get; private set; }

        public Producer Producer { get; private set; }

        public CommandService InitializeENode()
        {
            _jsonSerializer = ObjectContainer.Resolve<IJsonSerializer>();
            _commandTopicProvider = ObjectContainer.Resolve<ITopicProvider<ICommand>>();
            _typeNameProvider = ObjectContainer.Resolve<ITypeNameProvider>();
            _sendMessageService = new SendQueueMessageService();
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().FullName);
            _ioHelper = ObjectContainer.Resolve<IOHelper>();
            return this;
        }

        public CommandService InitializeEQueue(CommandResultProcessor commandResultProcessor = null, ProducerSetting setting = null)
        {
            InitializeENode();
            _commandResultProcessor = commandResultProcessor;
            Producer = new Producer(setting, "CommandService");
            return this;
        }

        public CommandService Start()
        {
            if (_commandResultProcessor != null)
            {
                _commandResultProcessor.Start();
            }
            Producer.Start();
            return this;
        }

        public CommandService Shutdown()
        {
            Producer.Shutdown();
            if (_commandResultProcessor != null)
            {
                _commandResultProcessor.Shutdown();
            }
            return this;
        }

        public Task<CommandResult> ExecuteAsync(ICommand command)
        {
            throw new NotImplementedException();
        }

        public Task<CommandResult> ExecuteAsync(ICommand command, CommandReturnType commandReturnType)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(ICommand command)
        {
            throw new NotImplementedException();
        }

        private EQueueMessage BuildCommandMessage(ICommand command, out string messageBody, bool needReply = false)
        {
            Ensure.NotNull(command.AggregateRootId, "aggregateRootId");
            var commandData = _jsonSerializer.Serialize(command);
            var topic = _commandTopicProvider.GetTopic(command);
            var replyAddress=needReply&& _commandResultProcessor != null ? _commandResultProcessor.BindingAddress.ToString() : null;
            var messageData = _jsonSerializer.Serialize(new CommandMessage
            {
                CommandData = commandData,
                ReplyAddress = replyAddress
            });
            messageBody = messageData;
            return new EQueueMessage(
                topic,
                (int)EQueueMessageTypeCode.CommandMessage,
                Encoding.UTF8.GetBytes(messageData),
                _typeNameProvider.GetTypeName(command.GetType()));
        }
    }
}
