using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs {
    public class LikeDto {
        public int LikesCount { get; set; }
        public bool CurrentUserLiked { get; set; }

    }
}
