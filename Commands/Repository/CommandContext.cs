
using Microsoft.EntityFrameworkCore;
using Commands.Entities;

namespace Commands.Repository
{
    public class CommandContext: DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options):base(options)
        {

        }
        public DbSet<Command> Command { get; set;}
    }
}