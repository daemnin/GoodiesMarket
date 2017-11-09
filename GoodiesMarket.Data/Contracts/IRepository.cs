using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GoodiesMarket.Data.Contracts
{
    public interface IRepository<TEntity>
    {
        TEntity Create(TEntity entity);
        TEntity Delete(int id);
        IList<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        TEntity Read<TKey>(TKey id);
        TEntity Update(TEntity entity);
        bool Any(Expression<Func<TEntity, bool>> predicate);
    }
}
