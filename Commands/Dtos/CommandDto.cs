using System;
namespace Commands.Dtos
{
    public class CommandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CommandLine { get; set; }
        public string Platform { get; set; }
        public DateTime CreatedDate {   get; set;}
    }
}
