using PreMatriculasParanoa.Domain.Models.Base;

namespace PreMatriculasParanoa.Domain.Repositories.Base
{
    public interface IBaseRepository<TEntity> : IWriterBaseRepository<TEntity>, IReaderBaseRepository<TEntity>
        where TEntity : Entity
    {
    }
}
