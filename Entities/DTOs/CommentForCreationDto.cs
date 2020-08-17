using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CommentForCreationDto : CommentForManipulationDto
    {
        public int PostId { get; set; }
        public string CommentedById { get; set; }
    }
}
