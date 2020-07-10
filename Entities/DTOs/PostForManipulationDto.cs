using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTOs
{
    public abstract class PostForManipulationDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(30, ErrorMessage = "Max title length is 30")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Body is required")]
        [MaxLength(255, ErrorMessage = "Max body length is 255")]
        public string Body { get; set; }
    }
}
