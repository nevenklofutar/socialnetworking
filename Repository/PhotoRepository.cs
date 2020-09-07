using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository {
    public class PhotoRepository :RepositoryBase<Photo>, IPhotoRepository {
        public PhotoRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) {
        }

        public void CreatePhoto(Photo photo) {
            photo.DateAdded = DateTime.Now;
            Create(photo);
        }
    }
}
