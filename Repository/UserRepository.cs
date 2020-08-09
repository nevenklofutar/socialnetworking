using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Repository {
    public class UserRepository : RepositoryBase<User>, IUserRepository {

        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) {
        }

        public Task<User> GetUserById(string userId) {
            return FindByCondition(user => user.Id == userId).SingleOrDefaultAsync<User>();
        }

        public Task<List<User>> GetUsersAsync(UserParameters userParameters, bool trackChanges) {
            
            var users = FindByCondition(user => user.FirstName.ToLower().Contains(userParameters.SearchTerm.ToLower()) ||
                    user.LastName.ToLower().Contains(userParameters.SearchTerm.ToLower()))
                .OrderBy(user => user.LastName)
                .ToListAsync<User>();

            return users;
        }
    }
}
