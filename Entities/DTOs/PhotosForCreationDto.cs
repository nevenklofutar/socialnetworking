using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs {
    public class PhotosForCreationDto {
        public ICollection<PhotoForCreationDto> Photos { get; set; }
    }
}
