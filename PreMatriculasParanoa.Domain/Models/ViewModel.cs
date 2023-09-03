namespace PreMatriculasParanoa.Domain.Models.Base;

public interface IViewModel
{
    CommandResult ViewModelValidations();
}

public abstract class ViewModel<TEntity> : IViewModel
    where TEntity : Entity
{
    public virtual int Id { get; set; }

    public virtual bool Ativo { get; set; } = true;

    public virtual CommandResult ViewModelValidations()
    {
        return new CommandResult();
    }
}