using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult<CommandDto>> CreateCommand(CreateCommandDto commandDto)
        {   
            Command command = new ()
            {
                Id = Guid.NewGuid(),
                Name= commandDto.Name,
                CommandLine = commandDto.CommandLine,
                Platform = commandDto.Platform,
                CreatedDate = DateTime.UtcNow
            };
            await _repo.CreateCommandAsync(command);

            var dto = _mapper.Map<Command,CommandDto>(command);

            return CreatedAtAction(nameof(GetCommandById), new {Id = command.Id}, dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommandDto>> GetCommandById(Guid id)
        {
            var command = await _repo.GetCommandByIdAsync(id);
            if(command is null)
            {
                return NotFound();
            }
            var commandDto = _mapper.Map<Command,CommandDto>(command);
            return Ok(commandDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommand(Guid id, UpdateCommandDto command)
        {
            var result = await _repo.GetCommandByIdAsync(id);
            if(result is null)
            {
                return NotFound();
            }
            result.Name = command.Name;
            result.Platform = command.Platform;
            result.CommandLine = command.CommandLine;
            await _repo.UpdateCommandAsync(result);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandDto>>> GetCommands()
        {
            var result = await _repo.GetCommandsAsync();
            if(result is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map< IEnumerable<Command>, IEnumerable<CommandDto> >(result) );
        }

    
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(Guid id)
        {
            var result = _repo.GetCommandByIdAsync(id);
            if(result is null)
            {
                return NotFound();
            }
            _repo.DeleteCommandAsync(id);
            return NoContent();
        }
    }
}
