using AutoMapper;
using Commands.Entities;
using Commands.Dtos;

namespace Commands.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandDto>();
        }
    }
}