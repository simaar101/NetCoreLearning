// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Commands.Entities;

// namespace Commands.Repository
// {
//     public class CommandRepo : ICommandRepo
//     {
//         private readonly List<Command> commands = new ()
//         {
//             new Command { Id = Guid.NewGuid(), Name = "Testing", CommandLine = "Testing", Platform = "Net Core", CreatedDate = DateTime.UtcNow},
//         };
//         public void CreateCommand(Command command)
//         {
//             commands.Add(command);
//         }

//         public void DeleteCommand(Guid id)
//         {
//             var index = commands.FindIndex(s => s.Id == id);
//             commands.RemoveAt(index);
//         }

//         public Command GetCommandById(Guid id)
//         {
//             return commands.Where(s => s.Id == id).SingleOrDefault();
//         }

//         public IEnumerable<Command> GetCommands()
//         {
//             return commands;
//         }

//         public void UpdateCommand(Command command)
//         {
//             var index = commands.FindIndex(s => s.Id == command.Id);
//             commands[index] = command;
//         }
//         public bool SaveChanges()
//         {
//             return false;
//         }
//     }
// }