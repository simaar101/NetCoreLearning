using System;
namespace Commands.Entities
{
    public class Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CommandLine { get; set; }
        public string Platform { get; set; }
        public DateTime CreatedDate {   get; set;}
    }
}
