﻿using AutoMapper;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Repositories.Base;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using PreMatriculasParanoa.Domain.Resources;

namespace PreMatriculasParanoa.Domain.Handlers.Usuario;

public interface IExcluirUsuarioCommandHandler : IDeleteCommandHandler
{
}

public class ExcluirUsuarioCommandHandler : IExcluirUsuarioCommandHandler
{
    private readonly ILogger<ExcluirUsuarioCommandHandler> logger;
    private readonly IUsuarioRepository repository;
    protected readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ExcluirUsuarioCommandHandler(
        IUsuarioRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ExcluirUsuarioCommandHandler> logger)
    {
        this.logger = logger;
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public CommandResult Execute(int id)
    {
        logger.LogInformation($"Iniciando handler ExcluirUsuarioCommandHandler");

        try
        {
            repository.Remove(repository.GetOne(id));
            unitOfWork.Commit();
            return new CommandResult(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new CommandResult(StatusCodes.Status500InternalServerError, ErrorMessageResource.ErroExclusaoRegistro);
        }
    }
}
