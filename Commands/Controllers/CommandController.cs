using System;
using System.Collections.Generic;
using AutoMapper;
using Commands.Dtos;
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
        private readonly IMapper _mapper;
        public CommandController(ICommandRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        
        [HttpPost]
        public ActionResult<CommandDto> CreateCommand(CreateCommandDto commandDto)
        {   
            Command command = new ()
            {
                Id = Guid.NewGuid(),
                Name= commandDto.Name,
                CommandLine = commandDto.CommandLine,
                Platform = commandDto.Platform,
                CreatedDate = DateTime.UtcNow
            };
            _repo.CreateCommand(command);

            var dto = _mapper.Map<Command,CommandDto>(command);

            return CreatedAtAction(nameof(GetCommandById), new {Id = command.Id}, dto);
        }

        [HttpGet("{id}")]
        public ActionResult<CommandDto> GetCommandById(Guid id)
        {
            var command = _repo.GetCommandById(id);
            if(command is null)
            {
                return NotFound();
            }
            var commandDto = _mapper.Map<Command,CommandDto>(command);
            return Ok(commandDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(Guid id, UpdateCommandDto command)
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
        public ActionResult<IEnumerable<CommandDto>> GetCommands()
        {
            var result = _repo.GetCommands();
            if(result is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map< IEnumerable<Command>, IEnumerable<CommandDto> >(result) );
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