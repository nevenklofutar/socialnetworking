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
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Comment> GetCommentAsync(int commentId) =>
            await FindByCondition(c => c.Id == commentId)
                .Include(c => c.CommentedBy)
                .SingleOrDefaultAsync<Comment>();

        public async Task<IEnumerable<Comment>> GetCommentsForPostAsync(int postId) =>
            await FindByCondition(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedOn)
                .Include(c => c.CommentedBy)
                .ToListAsync();
        
        public void CreateComment(Comment comment)
        {
            comment.CreatedOn = DateTime.Now;
            Create(comment);
        }

        public void DeleteComment(Comment comment) =>
            Delete(comment);
        
    }
}
