using PreMatriculasParanoa.Domain.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        [ForeignKey("EscolaOrigem")]
        public int IdEscolaOrigem { get; set; }
        public Escola EscolaOrigem { get; set; }

        [ForeignKey("EscolaDestino")]
        public int IdEscolaDestino { get; set; }
        public Escola EscolaDestino { get; set; }

        public int TotalMatriculas { get; set; }
    }

    public class PlanejamentoMatriculaSequencialAgrupado : Entity
    {
        public IEnumerable<PlanejamentoMatriculaSequencial> MatriculasSequenciais { get; set; } = new List<PlanejamentoMatriculaSequencial>();

        public int AnoLetivo { get; set; }

        public int SerieAnoOrigem { get; set; }
        public int SerieAnoDestino => SerieAnoOrigem++;

        public int IdEscolaOrigem { get; set; }
        public string NomeEscolaOrigem => EscolaOrigem?.Nome;
        public Escola EscolaOrigem { get; set; }

        public int TotalGeralMatriculas => MatriculasSequenciais.Sum(s => s.TotalMatriculas);
    }
}
