using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Like
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string LikerId { get; set; }
        public User Liker { get; set; }
        
        public DateTime LikedOn { get; set; }

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
