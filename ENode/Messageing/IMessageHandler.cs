using System;
using System.Collections.Generic;
using System.Text;

namespace ENode.Messageing
{
    public interface IMessageHandler
    {
    }

    public interface IMessageHandler<in T>:IMessageHandler where T : class, IMessage
    {

    }
}
