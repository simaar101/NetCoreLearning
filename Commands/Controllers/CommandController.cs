using System;
using System.Collections.Generic;
using Commands.Entities;
using Commands.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    [ApiController]
    [Route("Commands")]
    public class CommandController:ControllerBase
    {
        private readonly ICommandRepo _repo;
        public CommandController(ICommandRepo repo)
        {
            _repo = repo;
        }
        [HttpPost]
        public ActionResult<Command> CreateCommand(Command command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedDate = DateTime.UtcNow;
            
            _repo.CreateCommand(command);
            return CreatedAtAction(nameof(GetCommand), new {Id = command.Id}, command);
        }

        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Command>> UpdateCommand(Guid id, Command command)
        {
            var result = _repo.GetCommandById(id);
            if(result is null)
            {
                return NotFound();
            }
            result.Name = command.Name;
            result.Platform = command.Platform;
            result.CommandLine = command.CommandLine;
            _repo.UpdateCommand(result);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            var result = _repo.GetCommands();
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommand(Guid id)
        {
            var result = _repo.GetCommandById(id);
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(Guid id)
        {
            var result = _repo.GetCommandById(id);
            if(result is null)
            {
                return NotFound();
            }
            _repo.DeleteCommand(id);
            return NoContent();
        }
    }
}