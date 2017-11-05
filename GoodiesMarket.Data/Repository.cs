using GoodiesMarket.Data.Contracts;
using GoodiesMarket.Data.Extensions;
using GoodiesMarket.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GoodiesMarket.Data
{
    public class Repository<TEntity> : IRepository<TEntity>
            where TEntity : class
    {
        protected IDbSet<TEntity> Entities { get; set; }
        protected IContext CurrentContext { get; private set; }

        public Repository(IContext context)
        {
            CurrentContext = context;
            Entities = CurrentContext.Set<TEntity>();
        }

        public virtual TEntity Create(TEntity entity)
        {
            var response = Entities.Add(entity);
            return response;
        }

        public virtual TEntity Read(int id)
        {
            var response = Entities.Find(id);
            return response;
        }

        public virtual IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Entities;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                query = query.IncludeMultiple(includes);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            var response = Entities.Attach(entity);
            CurrentContext.Entry(entity).State = EntityState.Modified;
            return response;
        }

        public virtual TEntity Delete(int id)
        {
            var entity = Entities.Find(id);
            Entities.Remove(entity);
            return entity;
        }

        public virtual IList<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return Entities.IncludeMultiple(includes).Where(predicate).ToList();
        }
    }
}
