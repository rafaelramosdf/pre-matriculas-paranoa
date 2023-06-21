using PreMatriculasParanoa.Domain.Models.Base;
using System;
using System.Collections.Generic;

namespace PreMatriculasParanoa.Domain.Repositories.Base
{
    public interface IWriterBaseRepository<TEntity> 
        where TEntity : Entity
    {
        void Add(in TEntity obj);
        void Add(in IEnumerable<TEntity> objList);
        void Update(in TEntity obj);
        void Update(in IEnumerable<TEntity> objList);
        void Remove(in TEntity obj);
        void Remove(in IEnumerable<TEntity> objList);
        void Attach(in TEntity obj);
        void Attach(in IEnumerable<TEntity> objList);
    }
}
