using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.Models.EntitiesBase;
using FA.JustBlog.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FA.JustBlog.Core.Core.Infrastructures
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IBaseEntities
    {
        protected readonly BlogContext Context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(BlogContext context)
        {
            Context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Find(int entityId)
        {
            return _dbSet.Find(entityId);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public TEntity FindByUrlSlug(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity, bool isHardDeleted = false)
        {
            if (isHardDeleted == false)
            {
                entity.Status = Status.IsDeleted;
                Context.Entry(entity).State = EntityState.Modified;
                return;
            }

            _dbSet.Remove(entity);
        }

        public void Delete(int entityId, bool isHardDeleted = false)
        {
            var entity = Find(entityId);

            if (isHardDeleted == false)
            {
                entity.Status = Status.IsDeleted;
                Context.Entry(entity).State = EntityState.Modified;
                return;
            }

            _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}