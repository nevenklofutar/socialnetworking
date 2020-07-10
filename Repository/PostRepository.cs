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

        public async Task<PagedList<Post>> GetPostsAsync(PostParameters postParameters, bool trackChanges)
        {
            var posts = await FindAll(trackChanges)
                .FilterPosts(postParameters.Title, postParameters.Body)
                .SearchPosts(postParameters.SearchTerm)
                .SortPosts(postParameters.OrderBy)
                .ToListAsync();

            return PagedList<Post>.ToPagedList(posts, postParameters.PageNumber, postParameters.PageSize);
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
    }
}
