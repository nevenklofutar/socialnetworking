using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository {
    public class PhotoRepository :RepositoryBase<Photo>, IPhotoRepository {
        public PhotoRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) {
        }

        public async Task<IEnumerable<Photo>> GetPhotosForPostAsync(int postId) =>
            await FindByCondition(l => l.PostId == postId)
                .ToListAsync<Photo>();

        public void CreatePhoto(Photo photo) {
            photo.DateAdded = DateTime.Now;
            Create(photo);
        }

        public void DeletePostPhotos(IEnumerable<Photo> photos) =>
            DeleteRange(photos);
    }
}
