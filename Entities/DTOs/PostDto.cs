using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string CreatedById { get; set; }
        public LikeDto Likes { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
        public IEnumerable<PhotoDto> Photos { get; set; }
    }
}
