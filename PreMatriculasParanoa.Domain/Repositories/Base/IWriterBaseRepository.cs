using Microsoft.EntityFrameworkCore;
using PreMatriculasParanoa.Domain.Models.Base;
using System.Collections.Generic;

namespace PreMatriculasParanoa.Domain.Repositories.Base
{
    public interface IWriterBaseRepository<TEntity> 
        where TEntity : Entity
    {
        void Add(in TEntity obj);
        void Add(in IEnumerable<TEntity> objList);

        /// <summary>
        /// Altera registros existentes e/ou insere registros novos.
        /// Obs.: Caso a entidade (obj) tenha objetos "filhos", os "filhos" também serão atualizados ou inseridos
        /// </summary>
        /// <param name="obj">Entidade</param>
        void Update(in TEntity obj);
        void Update(in IEnumerable<TEntity> objList);
        
        void Remove(in TEntity obj);
        void Remove(in IEnumerable<TEntity> objList);

        /// <summary>
        /// O método "Attach" altera dados de uma entidade do contexto, e só gera a instrução SQL após marcar o status da entidade como "Modified"
        /// Obs.: Usado somente para alteração, não gera inclusão de novos registros.
        /// </summary>
        /// <param name="obj">Entidade</param>
        void Attach(in TEntity obj);
        void Attach(in IEnumerable<TEntity> objList);
        void Attach(in TEntity obj, EntityState state);
        void Attach(in IEnumerable<TEntity> objList, EntityState state);
        EntityState GetEntityState(in TEntity obj);
    }
}
