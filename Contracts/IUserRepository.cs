using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts {
    public interface IUserRepository {

        public Task<User> GetUserById(string userId);
        public Task<List<User>> GetUsersAsync(UserParameters userParameters, bool trackChanges);
    }
}
