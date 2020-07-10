using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CommentForCreationDto
    {
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}
