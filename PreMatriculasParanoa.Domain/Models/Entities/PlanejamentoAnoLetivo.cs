using PreMatriculasParanoa.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreMatriculasParanoa.Domain.Models.Entities
{
    public class PlanejamentoAnoLetivo : Entity
    {
        public override int Id => IdPlanejamentoAnoLetivo;

        [Key]
        public int IdPlanejamentoAnoLetivo { get; set; }
        public int AnoLetivo { get; set; }
        public DateTime DataInicioPlanejamento { get; set; }
        public DateTime DataTerminoPlanejamento { get; set; }

        [ForeignKey("Escola")]
        public int IdEscola { get; set; }
        public Escola Escola { get; set; }

        public ICollection<PlanejamentoSerieAno> SeriesAnos { get; set; }
    }
}
