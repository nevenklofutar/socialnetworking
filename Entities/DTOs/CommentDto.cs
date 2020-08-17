using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CommentedById { get; set; }
        public string CommentedByName { get; set; }
    }
}
