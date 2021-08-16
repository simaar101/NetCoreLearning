using System;
using System.Collections.Generic;
using Commands.Entities;
using Commands.Dtos;
using System.Linq;

namespace Commands.Repository
{
    public class CommandSqlRepo : ICommandRepo
    {
        private readonly CommandContext _context;

        public CommandSqlRepo(CommandContext context)
        {
            _context = context;
        }
        public void CreateCommand(Command command)
        {
            if(command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            _context.Commands.Add(command);
        }

        public void DeleteCommand(Guid id)
        {
            var toBeDeleted = _context.Commands.Where(s => s.Id == id).SingleOrDefault();
            _context.Commands.Remove(toBeDeleted);
        }

        public Command GetCommandById(Guid id)
        {
            return _context.Commands.Where(s => s.Id == id).SingleOrDefault();
        }

        public IEnumerable<Command> GetCommands()
        {
           return _context.Commands.ToList();
        }

        public void UpdateCommand(Command command)
        {
            _context.Commands.Update(command);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

