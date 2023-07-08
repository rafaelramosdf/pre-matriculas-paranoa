using PreMatriculasParanoa.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreMatriculasParanoa.Domain.Models.Entities
{
    public class PlanejamentoTurma : Entity
    {
        public override int Id => IdPlanejamentoTurma;

        [Key]
        public int IdPlanejamentoTurma { get; set; }
        public char SiglaTurma { get; set; }
        public string TurnoPeriodo { get; set; }
        public string TipoAtendimento { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CapacidadeFisicaAcordada { get; set; }

        [ForeignKey("Sala")]
        public int IdSala { get; set; }
        public Sala Sala { get; set; }

        [ForeignKey("PlanejamentoSerieAno")]
        public int IdPlanejamentoSerieAno { get; set; }
        public PlanejamentoSerieAno PlanejamentoSerieAno { get; set; }
    }
}
