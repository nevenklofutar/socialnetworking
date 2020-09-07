using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(30, ErrorMessage = "Max title length is 30")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Body is required")]
        [MaxLength(255, ErrorMessage = "Max body length is 255")]
        public string Body { get; set; }
        
        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(User))]
        public string CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
