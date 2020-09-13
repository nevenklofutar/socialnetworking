using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts {
    public interface IPhotoRepository {


        Task<IEnumerable<Photo>> GetPhotosForPostAsync(int postId);
        public void DeletePostPhotos(IEnumerable<Photo> photos);
        void CreatePhoto(Photo photo);
    }
}
