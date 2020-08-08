using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IPostRepository _postRepository;
        private ILikeRepository _likeRepository;
        private ICommentRepository _commentRepository;
        private IUserRepository _userRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IPostRepository Post 
        {
            get 
            {
                if (_postRepository == null)
                    _postRepository = new PostRepository(_repositoryContext);

                return _postRepository;
            }
        }

        public ILikeRepository Like
        {
            get
            {
                if (_likeRepository == null)
                    _likeRepository = new LikeRepository(_repositoryContext);

                return _likeRepository;
            }
        }

        public ICommentRepository Comment
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_repositoryContext);

                return _commentRepository;
            }
        }

        public IUserRepository User {
            get {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);

                return _userRepository;
            }
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
