using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LikeRepository : RepositoryBase<Like>, ILikeRepository
    {
        public LikeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<int> GetLikesCountForPostAsync(int postId) =>
            await FindByCondition(l => l.PostId == postId)
                .CountAsync();

        public async Task ProcessLikeAsync(int postId, string userId)
        {
            var like = await FindByCondition(like => like.PostId == postId && like.Liker.Id == userId)
                .SingleOrDefaultAsync<Like>();

            if (like == null)
                Create(new Like { LikedOn = DateTime.Now, LikerId = userId, PostId = postId });
            else
                Delete(like);
        }

    }
}
