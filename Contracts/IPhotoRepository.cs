﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts {
    public interface IPhotoRepository {
        
        void CreatePhoto(Photo photo);
    }
}
