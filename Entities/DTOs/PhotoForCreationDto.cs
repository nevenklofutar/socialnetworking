using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Entities.DTOs {
    public class PhotoForCreationDto {

        // from front end
        public string PhotoName { get; set; }
        // from front end
        public string PhotoBase64String { get; set; }

        // from cloudinary
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        // from cloudinary
        public string PublicId { get; set; }
        public string UserId { get; set; }
        public int? PostId { get; set; }

        public PhotoForCreationDto() {
            DateAdded = DateTime.Now;
        }
    }
}
