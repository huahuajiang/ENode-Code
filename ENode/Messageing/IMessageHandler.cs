using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENode.Messageing
{
    public interface IMessageHandler
    {
    }

    public interface IMessageHandler<in T>:IMessageHandler where T : class, IMessage
    {
        Task HandleAsync(T message);
    }

    public interface IMessageHandler<in T1,in T2>:IMessageHandler 
        where T1:class,IMessage 
        where T2 : class, IMessage
    {
        Task HandlerAsync(T1 message1, T2 message2);
    }

    public interface IMessageHandler<in T1,in T2,in T3>:IMessageHandler
        where T1:class,IMessage
        where T2:class,IMessage
        where T3:class, IMessage
    {
        Task HandleAsync(T1 message1, T2 message2, T3 message3);
    }
}
