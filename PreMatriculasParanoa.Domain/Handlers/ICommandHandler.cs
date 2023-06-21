using PreMatriculasParanoa.Domain.Models.Base;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Domain.Handlers;

public interface IDeleteCommandHandler
{
    CommandResult Execute(int id);
}
public interface IDeleteCommandHandlerAsync
{
    Task<CommandResult> Execute(int id);
}

public interface IEditCommandHandler<in TViewModel>
    where TViewModel : IViewModel
{
    CommandResult Execute(TViewModel vm);
}
public interface IEditCommandHandlerAsync<in TViewModel>
    where TViewModel : IViewModel
{
    Task<CommandResult> Execute(TViewModel vm);
}

public interface IRequestResponseCommandHandler<in TRequest, out TResponse>
{
    TResponse Execute(TRequest vm);
}
public interface IRequestResponseCommandHandlerAsync<TRequest, TResponse>
{
    Task<TResponse> Execute(TRequest vm);
}