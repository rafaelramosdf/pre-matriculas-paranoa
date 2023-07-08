using PreMatriculasParanoa.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace PreMatriculasParanoa.Domain.Models.Entities
{
    public class PlanejamentoMatriculaSequencial : Entity
    {
        public override int Id => IdPlanejamentoMatriculaSequencial;

        [Key]
        public int IdPlanejamentoMatriculaSequencial { get; set; }
        public int AnoLetivo { get; set; }
        public int SerieAnoOrigem { get; set; }
        public int SerieAnoDestino { get; set; }
        public int IdEscolaOrigem { get; set; }
        public int IdEscolaDestino { get; set; }
        public int TotalMatriculas { get; set; }
    }
}
