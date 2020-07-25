using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTOs
{
    public class PostForCreationDto : PostForManipulationDto
    {
        public string CreatedById { get; set; }

    }
}
