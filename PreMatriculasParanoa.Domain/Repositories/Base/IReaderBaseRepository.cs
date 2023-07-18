using PreMatriculasParanoa.Domain.Models.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PreMatriculasParanoa.Domain.Repositories.Base
{
    public interface IReaderBaseRepository<TEntity>
        where TEntity : Entity
    {
        IQueryable<TEntity> GetQuery();
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> query);
        TEntity GetOne(Expression<Func<TEntity, bool>> query);
        TEntity GetOne(Expression<Func<TEntity, bool>> query, params Expression<Func<TEntity, object>>[] includes);
        TEntity GetOne(int id);
    }
}
