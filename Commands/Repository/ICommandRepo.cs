using System;
using System.Collections.Generic;
using Commands.Entities;

namespace Commands.Repository
{
    public interface ICommandRepo
    {
        IEnumerable<Command> GetCommands();
        Command GetCommandById(Guid id);
        void UpdateCommand(Command command);
        void CreateCommand(Command command);
        void DeleteCommand(Guid id);
        bool SaveChanges();
    }
}