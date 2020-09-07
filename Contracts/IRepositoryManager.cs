using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IPostRepository Post { get;  }
        ILikeRepository Like { get; }
        ICommentRepository Comment { get; }
        IUserRepository User { get; }
        IPhotoRepository Photo { get; }

        Task SaveAsync();
    }
}
