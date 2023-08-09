using PreMatriculasParanoa.Domain.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreMatriculasParanoa.Domain.Models.Entities
{
    public class PlanejamentoSerieAno : Entity
    {
        public override int Id => IdPlanejamentoSerieAno;

        [Key]
        public int IdPlanejamentoSerieAno { get; set; }
        public int SerieAno { get; set; }
        public bool PrimeiraSerieAno { get; set; }
        public bool UltimaSerieAno { get; set; }
        public int EntradaAprovadosSerieAnoAnterior { get; set; }
        public int EntradaRetidosSerieAnoAtual { get; set; }
        public int EntradaSequencial { get; set; }
        public int EntradaCentralMatricula { get; set; }
        public int EntradaRemanejamento { get; set; }
        public int SaidaRemanejamento { get; set; }
        public int SaidaAprovadosAnoAtual { get; set; }

        [ForeignKey("PlanejamentoAnoLetivo")]
        public int IdPlanejamentoAnoLetivo { get; set; }
        public PlanejamentoAnoLetivo PlanejamentoAnoLetivo { get; set; }

        public ICollection<PlanejamentoTurma> Turmas { get; set; }
    }
}
