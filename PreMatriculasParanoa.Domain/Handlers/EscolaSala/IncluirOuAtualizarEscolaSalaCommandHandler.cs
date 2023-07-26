using AutoMapper;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Repositories.Base;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using PreMatriculasParanoa.Domain.Models.Base;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Handlers.EscolaSala;

public interface IIncluirOuAtualizarEscolaSalaCommandHandler : IEditCommandHandler<EscolaViewModel>
{
}

public class IncluirOuAtualizarEscolaSalaCommandHandler : IIncluirOuAtualizarEscolaSalaCommandHandler
{
    private readonly ILogger<IncluirOuAtualizarEscolaSalaCommandHandler> logger;
    private readonly IEscolaRepository escolaRepository;
    private readonly ISalaRepository salaRepository;
    protected readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public IncluirOuAtualizarEscolaSalaCommandHandler(
        IEscolaRepository escolaRepository,
        ISalaRepository salaRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<IncluirOuAtualizarEscolaSalaCommandHandler> logger)
    {
        this.logger = logger;
        this.escolaRepository = escolaRepository;
        this.salaRepository = salaRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public CommandResult Execute(EscolaViewModel vm)
    {
        logger.LogInformation($"Iniciando handler IncluirOuAtualizarEscolaSalaCommandHandler");

        try
        {
            var novoRegistro = vm.IdEscola < 1;
            var escola = mapper.Map<Models.Entities.Escola>(vm);
            
            if(novoRegistro) 
            {
                escolaRepository.Update(escola);
                unitOfWork.Commit();
                return new CommandResult(StatusCodes.Status201Created, mapper.Map<EscolaViewModel>(escola));
            }
            else
            {
                unitOfWork.BeginTransaction();
                var idSalas = vm.Salas.Where(m => m.IdSala > 0).Select(s => s.IdSala);
                var salasExcluidas = salaRepository.GetQuery(m => m.IdEscola == vm.IdEscola && !idSalas.Contains(m.IdSala)).ToList();
                var salasAlteradas = escola.Salas.Where(m => m.IdSala > 0).ToList();
                var salasNovas = escola.Salas.Where(m => m.IdSala < 1).ToList();

                if(salasNovas?.Any() == true) 
                {
                    salaRepository.Add(salasNovas); 
                    unitOfWork.Commit();
                }

                if (salasAlteradas?.Any() == true) 
                {
                    salaRepository.Attach(salasAlteradas);
                    unitOfWork.Commit();
                }

                if (salasExcluidas?.Any() == true)
                {
                    salaRepository.Remove(salasExcluidas);
                    unitOfWork.Commit();
                }

                escolaRepository.Attach(escola);
                unitOfWork.Commit();

                unitOfWork.CommitTransaction();
                return new CommandResult(StatusCodes.Status200OK, mapper.Map<EscolaViewModel>(escola));
            }
        }
        catch (Exception ex)
        {
            return new CommandResult(ex, vm);
        }
    }
}
