using AutoMapper;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Repositories.Base;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;

namespace PreMatriculasParanoa.Domain.Handlers.Usuario;

public interface IAutenticarUsuarioCommandHandler : IRequestResponseCommandHandler<AutenticacaoUsuarioViewModel, UsuarioViewModel>
{
}

public class AutenticarUsuarioCommandHandler : IAutenticarUsuarioCommandHandler
{
    private readonly ILogger<AutenticarUsuarioCommandHandler> Logger;
    private readonly IUsuarioRepository Repository;
    protected readonly IUnitOfWork UnitOfWork;
    private readonly IMapper Mapper;

    public AutenticarUsuarioCommandHandler(
        IUsuarioRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<AutenticarUsuarioCommandHandler> logger)
    {
        Logger = logger;
        Repository = repository;
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

    public UsuarioViewModel Execute(AutenticacaoUsuarioViewModel dto)
    {
        Logger.LogInformation($"Iniciando handler AutenticarUsuarioCommandHandler");

        try
        {
            //TODO: A implementar
            return new UsuarioViewModel();
        }
        catch
        {
            return null;
        }
    }
}
