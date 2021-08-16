using System;
using System.Collections.Generic;
using Commands.Entities;
using Commands.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Commands.Repository
{
    public class CommandSqlRepo : ICommandRepo
    {
        private readonly CommandContext _context;

        public CommandSqlRepo(CommandContext context)
        {
            _context = context;
        }
        public async Task CreateCommandAsync(Command command)
        {
            if(command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
           await _context.Commands.AddAsync(command);
           await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Command>> GetCommandsAsync()
        {
             return await _context.Commands.ToListAsync();
        }

        public async Task<Command> GetCommandByIdAsync(Guid id)
        {
            return await _context.Commands.Where(s => s.Id == id).SingleOrDefaultAsync();
        }

        public async Task UpdateCommandAsync(Command command)
        {
            if(command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            try
            {
                _context.Update(command);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task DeleteCommandAsync(Guid id)
        {
            var toBeDeleted = await _context.Commands.FindAsync(id);
            _context.Remove(toBeDeleted);
            await _context.SaveChangesAsync();
        }
    }
}

