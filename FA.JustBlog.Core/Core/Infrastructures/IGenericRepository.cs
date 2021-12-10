using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FA.JustBlog.Core.Models.EntitiesBase;

namespace FA.JustBlog.Core.Core.Infrastructures
{
    public interface IGenericRepository<TEntity> where TEntity : class, IBaseEntities
    {
        //Find
        TEntity Find(int entityId);
        TEntity FindByUrlSlug(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        //Add
        void Add(TEntity entity);

        //Modified
        void Update(TEntity entity);

        //Delete
        void Delete(TEntity entity, bool isHardDeleted = false);
        void Delete(int entityId, bool isHardDeleted = false);

        //Get All
        IEnumerable<TEntity> GetAll();
    }
}