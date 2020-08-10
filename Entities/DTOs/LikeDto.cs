using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs {
    public class LikeDto {
        public int Id { get; set; }
        public string LikerId { get; set; }
        public int PostId { get; set; }

    }
}
