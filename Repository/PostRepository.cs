using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Post>> GetPostsForUserAsync(PostParameters postParameters, bool trackChanges) {
            return await FindAll(trackChanges)
                .SearchUserPosts(postParameters.CreatedByUserId)
                .SortPosts(postParameters.OrderBy)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .ThenInclude(c => c.CommentedBy)
                .ToListAsync();
        }

        public async Task<Post> GetPostAsync(int postId, bool trackChanges = false) =>
            await FindByCondition(p => p.Id == postId, trackChanges)
                .SingleOrDefaultAsync();

        public void CreatePost(Post post)
        {
            post.CreatedOn = DateTime.Now;
            Create(post);
        }

        public void DeletePost(Post post) { 
            Delete(post);
        }

        public void UpdatePost(Post post) {
            Update(post);
        }


    }
}
