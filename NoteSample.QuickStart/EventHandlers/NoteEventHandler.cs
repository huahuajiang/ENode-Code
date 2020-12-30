using ECommon.Logging;
using ENode.Messageing;
using NoteSample.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteSample.QuickStart.EventHandlers
{
    public class NoteEventHandler : IMessageHandler<NoteCreated>, IMessageHandler<NoteTitleChanged>
    {
        private ILogger _logger;

        public NoteEventHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.Create(typeof(NoteEventHandler).Name);
        }
        public Task HandleAsync(NoteCreated evnt)
        {
            _logger.InfoFormat("Note created, title：{0}, Version: {1}", evnt.Title, evnt.Version);
            return Task.CompletedTask;
        }

        public Task HandleAsync(NoteTitleChanged evnt)
        {
            _logger.InfoFormat("Note title changed, title：{0}, Version: {1}", evnt.Title, evnt.Version);
            return Task.CompletedTask;
        }
    }
}
