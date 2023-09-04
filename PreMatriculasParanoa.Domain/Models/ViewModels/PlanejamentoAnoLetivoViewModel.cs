using PreMatriculasParanoa.Domain.Extensions;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.Enumerations;
using PreMatriculasParanoa.Domain.Models.ViewModels.Validations;
using PreMatriculasParanoa.Domain.Utils.EqualityComparers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class PlanejamentoAnoLetivoViewModel : ViewModel<PlanejamentoAnoLetivo>
    {
        public override int Id => IdPlanejamentoAnoLetivo;

        public int IdPlanejamentoAnoLetivo { get; set; }

        [RequiredValidation("Ano letivo")]
        public int AnoLetivo { get; set; }

        public DateTime? DataInicioPlanejamento { get; set; } = DateTime.Now;
        public DateTime? DataTerminoPlanejamento { get; set; } = DateTime.Now.AddMonths(3);

        [RequiredValidation("Escola")]
        public int IdEscola { get; set; }
        public string NomeEscola => Escola?.Nome;
        public EnumModalidadeEducacao ModalidadeEducacaoEscola => Escola?.ModalidadeEnsino ?? EnumModalidadeEducacao.CrechePreEscola;

        public EscolaViewModel Escola { get; set; }

        [ValidateComplexType]
        public List<PlanejamentoSerieAnoViewModel> SeriesAnos { get; set; } = new List<PlanejamentoSerieAnoViewModel>();

        public int TotalSeriesAnosCadastrados => SeriesAnos?.Count ?? 0;
        public int TotalTurmasCadastradas => SeriesAnos?.SelectMany(s => s.Turmas)?.Count() ?? 0;

        public int TotalSequencialEntrando => SeriesAnos?.Sum(s => s.EntradaSequencial) ?? 0;
        public int TotalCentralMatriculasEntrando => SeriesAnos?.Sum(s => s.EntradaCentralMatricula) ?? 0;
        public int TotalRetidosEntrando => SeriesAnos?.Sum(s => s.EntradaRetidosSerieAnoAtual) ?? 0;
        public int TotalRemanejamentoEntrando => SeriesAnos?.Sum(s => s.EntradaRemanejamento) ?? 0;
        public int TotalAprovadosUltimoAnoSaindo => SeriesAnos?.Where(s => s.UltimaSerieAno)?.Sum(s => s.SaidaAprovadosAnoAtual) ?? 0;
        public int TotalRemanejamentoSaindo => SeriesAnos?.Sum(s => s.SaidaRemanejamento) ?? 0;

        public int TotalCapacidade => SeriesAnos?.Sum(s => s.TotalCapacidadeFisicaAcordada) ?? 0;
        public int TotalVagasDisponiveis => SeriesAnos?.Sum(s => s.TotalVagasDisponiveis) ?? 0;

        public override CommandResult ViewModelValidations()
        {
            var result = new CommandResult();
            var comparer = new PlanejamentoTurmaViewModelComparer();

            var todasAsTurmas = SeriesAnos.SelectMany(s => s.Turmas).ToList();
            if (todasAsTurmas.Distinct(comparer).Count() != todasAsTurmas.Count)
                result.Errors.Add($"Existem turmas duplicadas! Não podem existir mais de uma turma para o mesmo turno e mesma sala.");

            foreach (var serieAno in SeriesAnos)
            {
                if (serieAno.TotalValoresInformados > serieAno.TotalCapacidadeFisicaAcordada)
                    result.Errors.Add($"O total de matrículas informadas na série/ano \"{serieAno.EnumSerieAnoEscolar.EnumDescription()}\", " +
                        $"ultrapassou a CAPACIDADE física acordada! ");
            }
            return result;
        }
    }
}
