using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Models.Entities;

namespace Cened.Penitenciario.Api.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Escola, EscolaViewModel>().ReverseMap();
            CreateMap<Sala, SalaViewModel>().ReverseMap();
            CreateMap<PlanejamentoAnoLetivo, PlanejamentoAnoLetivoViewModel>().ReverseMap();
            CreateMap<PlanejamentoSerieAno, PlanejamentoSerieAnoViewModel>().ReverseMap();
            CreateMap<PlanejamentoTurma, PlanejamentoTurmaViewModel>().ReverseMap();
            CreateMap<PlanejamentoMatriculaSequencial, PlanejamentoMatriculaSequencialViewModel>().ReverseMap();
            CreateMap<PlanejamentoMatriculaSequencialAgrupado, PlanejamentoMatriculaSequencialAgrupadoViewModel>().ReverseMap();
        }
    }
}
