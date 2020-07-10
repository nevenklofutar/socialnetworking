using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICommentRepository
    {
        public Task<Comment> GetCommentAsync(int commentId);
        public Task<IEnumerable<Comment>> GetCommentsForPostAsync(int postId);

        public void CreateComment(Comment comment);
        public void DeleteComment(Comment comment);
    }
}
