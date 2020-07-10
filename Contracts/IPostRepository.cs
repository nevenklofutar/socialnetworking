using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPostRepository
    {
        //The Create and Delete method signatures are left synchronous.That’s
        //because in these methods, we are not making any changes in the
        //database.All we're doing is changing the state of the entity to Added and
        //Deleted.

        Task<PagedList<Post>> GetPostsAsync(PostParameters postParameters, bool trackChanges);
        Task<Post> GetPostAsync(int postId, bool trackChanges = false);
        void CreatePost(Post post);
        void DeletePost(Post post);

    }
}
