using System.Threading.Tasks;

namespace ENode.Commanding
{
    public interface ICommandHandler<in TCommand> where TCommand:class, ICommand
    {
        Task HandleAsync(ICommandContext context, TCommand command);
    }
}
