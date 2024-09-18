using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Employee
{
    public class UpdateEmployeeRequestDto
    {

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Description must be 5 characters.")]
        [MaxLength(280, ErrorMessage = "Description cannot over 280 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool Active { get; set; } = true;
    }
}