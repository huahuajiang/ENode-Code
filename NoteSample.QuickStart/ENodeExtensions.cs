using ENode.EQueue.Command;
using EQueue.Broker;
using EQueue.NameServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteSample.QuickStart
{
    public static class ENodeExtensions
    {
        private static NameServerController nameServerController;
        private static BrokerController _broker;
        private static CommandService _commandService;
    }
}
