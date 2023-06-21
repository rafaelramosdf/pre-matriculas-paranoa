namespace PreMatriculasParanoa.Domain.Models.Base;

public interface IViewModel
{
}

public abstract class ViewModel<TEntity> : IViewModel
    where TEntity : Entity
{
    public virtual int Id { get; set; }
}