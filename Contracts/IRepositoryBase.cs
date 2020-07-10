using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        //We won’t be changing the mentioned interface and class. That’s because
        //we want to leave a possibility for the repository user classes to have
        //either sync or async method execution.Sometimes, the async code could
        //become slower than the sync one because EF Core’s async commands
        //take slightly longer to execute (due to extra code for handling the
        //threading), so leaving this option is always a good choice.
        //It is general advice to use async code wherever it is possible, but if we
        //notice that our async code runes slower, we should switch back to the
        //sync one.

        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
