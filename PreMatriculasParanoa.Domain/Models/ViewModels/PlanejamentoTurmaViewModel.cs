using Newtonsoft.Json;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels.Validations;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class PlanejamentoTurmaViewModel : ViewModel<PlanejamentoTurma>
    {
        public override int Id => IdPlanejamentoTurma;

        public int IdPlanejamentoTurma { get; set; }

        [RequiredValidation("Sigla da turma")]
        public char SiglaTurma { get; set; }

        [RequiredValidation("Turno da turma")]
        public string TurnoPeriodo { get; set; }

        [RequiredValidation("Tipo atendimento da turma")]
        public string TipoAtendimento { get; set; }

        [RequiredValidation("Capacidade física acordada da turma")]
        public decimal CapacidadeFisicaAcordada { get; set; }

        [RequiredValidation("Sala da turma")]
        public int IdSala { get; set; }

        public int IdPlanejamentoSerieAno { get; set; }

        [JsonIgnore]
        public SalaViewModel Sala { get; set; }

        [JsonIgnore]
        public PlanejamentoSerieAnoViewModel PlanejamentoSerieAno { get; set; }

        public decimal MetragemSala => Sala?.Metragem ?? 0m;
        public decimal CapacidadeFisicaPadraoSala => Sala?.CapacidadeFisicaPadrao ?? 0m;
    }
}
