using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENode.Commanding
{
    public interface ICommandService
    {
        Task SendAsync(ICommand command);

        Task<CommandResult> ExecuteAsync(ICommand command);

        Task<CommandResult> ExecuteAsync(ICommand command, CommandReturnType commandReturnType);
    }
}
