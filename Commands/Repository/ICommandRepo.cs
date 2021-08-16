using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Commands.Entities;

namespace Commands.Repository
{
    public interface ICommandRepo
    {
        Task<IEnumerable<Command>> GetCommandsAsync();
        Task<Command> GetCommandByIdAsync(Guid id);
        Task UpdateCommandAsync(Command command);
        Task CreateCommandAsync(Command command);
        Task DeleteCommandAsync(Guid id);
    }
}