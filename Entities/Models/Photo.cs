using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models {
    public class Photo {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public bool IsApproved { get; set; }


        #nullable enable
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Post))]
        public int? PostId { get; set; }
        public Post? Post { get; set; }
        #nullable disable

        public Photo() {
            DateAdded = DateTime.Now;
            Description = "";
        }
    }
}
