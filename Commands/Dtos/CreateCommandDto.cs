using System;
using System.ComponentModel.DataAnnotations;

namespace Commands.Dtos
{
    public class CreateCommandDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
        [Required]
        public string CommandLine { get; set; }
        [Required]
        [MaxLength(250)]
        public string Platform { get; set; }
    }
}
