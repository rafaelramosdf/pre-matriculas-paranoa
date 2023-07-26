using Microsoft.EntityFrameworkCore;
using System;
using PreMatriculasParanoa.Infra.Context;
using PreMatriculasParanoa.Domain.Models.Base;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using PreMatriculasParanoa.Domain.Repositories.Base;

namespace PreMatriculasParanoa.Infra.Repository.Base
{
    public class BaseRepository<TEntity> : IWriterBaseRepository<TEntity>, IReaderBaseRepository<TEntity>
        where TEntity : Entity
    {
        protected readonly EntityContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(in EntityContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        #region ***** READER *****

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> query) => DbSet.Where(query).FirstOrDefault();
        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> query, params Expression<Func<TEntity, object>>[] includes)
        {
            var queryable = DbSet.Where(query);
            foreach (var include in includes)
            {
                queryable = queryable.Include(include);
            }
            return queryable.FirstOrDefault();
        }

        public virtual TEntity GetOne(int id) => DbSet.Find(id);

        public virtual IQueryable<TEntity> GetQuery() => DbSet.AsNoTracking();

        public virtual IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> query) => DbSet.Where(query).AsNoTracking();

        #endregion


        #region ***** WRITER *****
        public virtual void Add(in TEntity obj) => DbSet.Add(obj);
        public void Add(in IEnumerable<TEntity> objList)
        {
            foreach (var obj in objList)
            {
                Add(obj);
            }
        }

        /// <summary>
        /// O método "Attach" altera dados de uma entidade do contexto, e só gera a instrução SQL após marcar o status da entidade como "Modified"
        /// Obs.: Usado somente para alteração, não gera inclusão de novos registros.
        /// </summary>
        /// <param name="obj">Entidade</param>
        public virtual void Attach(in TEntity obj)
        {
            DbSet.Attach(obj);
            DbSet.Entry(obj).State = EntityState.Modified;
        }
        public void Attach(in IEnumerable<TEntity> objList)
        {
            foreach (var obj in objList)
            {
                Attach(obj);
            }
        }
        public virtual void Attach(in TEntity obj, EntityState state)
        {
            DbSet.Attach(obj);
            DbSet.Entry(obj).State = state;
        }
        public void Attach(in IEnumerable<TEntity> objList, EntityState state)
        {
            foreach (var obj in objList)
            {
                Attach(obj, state);
            }
        }

        public virtual EntityState GetEntityState(in TEntity obj) 
        {
            return DbSet.Entry(obj).State;
        }

        /// <summary>
        /// Altera registros existentes e/ou insere registros novos.
        /// Obs.: Caso a entidade (obj) tenha objetos "filhos", os "filhos" também serão atualizados ou inseridos
        /// </summary>
        /// <param name="obj">Entidade</param>
        public virtual void Update(in TEntity obj) => DbSet.Update(obj);
        public void Update(in IEnumerable<TEntity> objList)
        {
            foreach (var obj in objList)
            {
                Update(obj);
            }
        }

        public virtual void Remove(in TEntity obj) => DbSet.Remove(obj);
        public void Remove(in IEnumerable<TEntity> objList)
        {
            foreach (var obj in objList)
            {
                Remove(obj);
            }
        }
        #endregion
    }
}