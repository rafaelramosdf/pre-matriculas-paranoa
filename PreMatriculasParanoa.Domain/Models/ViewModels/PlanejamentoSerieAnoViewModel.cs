using Newtonsoft.Json;
using PreMatriculasParanoa.Domain.Extensions;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class PlanejamentoSerieAnoViewModel : ViewModel<PlanejamentoSerieAno>
    {
        public override int Id => IdPlanejamentoSerieAno;

        public int IdPlanejamentoSerieAno { get; set; }
        public int SerieAno { get; set; }
        public EnumSerieAnoEscolar EnumSerieAnoEscolar 
        { 
            get => (EnumSerieAnoEscolar)SerieAno; 
            set => SerieAno = value.GetHashCode(); 
        }
        public bool PrimeiraSerieAno { get; set; }
        public bool UltimaSerieAno { get; set; }
        public int EntradaAprovadosSerieAnoAnterior { get; set; }
        public int EntradaRetidosSerieAnoAtual { get; set; }
        public int EntradaSequencial { get; set; }
        public int EntradaCentralMatricula { get; set; }
        public int EntradaRemanejamento { get; set; }
        public int SaidaRemanejamento { get; set; }
        public int SaidaAprovadosAnoAtual { get; set; }
        public int IdPlanejamentoAnoLetivo { get; set; }

        [JsonIgnore]
        public PlanejamentoAnoLetivoViewModel PlanejamentoAnoLetivo { get; set; }

        [ValidateComplexType]
        public List<PlanejamentoTurmaViewModel> Turmas { get; set; } = new List<PlanejamentoTurmaViewModel>();

        public int TotalTurmas => Turmas.Count;
        public int TotalCapacidadeFisicaPadrao => Turmas.Sum(t => Math.Round(t.Sala?.CapacidadeFisicaPadrao ?? 0m, 0, MidpointRounding.AwayFromZero).ToInt32());
        public int TotalCapacidadeFisicaAcordada => Turmas.Sum(t => Math.Round(t.CapacidadeFisicaAcordada, 0, MidpointRounding.AwayFromZero).ToInt32());
        public int TotalVagasDisponiveis =>
            TotalCapacidadeFisicaAcordada - 
                ((EntradaAprovadosSerieAnoAnterior + EntradaCentralMatricula + EntradaRemanejamento + EntradaRetidosSerieAnoAtual + EntradaSequencial) - 
                    (SaidaAprovadosAnoAtual + SaidaRemanejamento));

        public bool ExibirDetalhesTurmas { get; set; } = false;
    }
}
