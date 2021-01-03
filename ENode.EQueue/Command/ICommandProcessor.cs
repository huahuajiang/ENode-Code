using System;
using System.Collections.Generic;
using System.Text;

namespace ENode.EQueue.Command
{
    public interface ICommandProcessor
    {
        void Process(ProcessingCommand)
    }
}
