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

        protected const string SchemaName = "Penitenciario";


        #region ***** READER *****

        public virtual TEntity Get(Expression<Func<TEntity, bool>> query) => DbSet.Where(query).FirstOrDefault();

        public virtual TEntity Get(int id) => DbSet.Find(id);

        public virtual IQueryable<TEntity> List() => DbSet.AsNoTracking();

        public virtual IQueryable<TEntity> List(Expression<Func<TEntity, bool>> query) => DbSet.Where(query).AsNoTracking();

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

        public virtual void Attach(in TEntity obj) => DbSet.Attach(obj);
        public void Attach(in IEnumerable<TEntity> objList)
        {
            foreach (var obj in objList)
            {
                Attach(obj);
            }
        }

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