using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Enumerations;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreMatriculasParanoa.Domain.Models.Entities
{
    public class PlanejamentoMatriculaSequencial : Entity
    {
        public override int Id => IdPlanejamentoMatriculaSequencial;

        [Key]
        public int IdPlanejamentoMatriculaSequencial { get; set; }
        public int AnoLetivo { get; set; }
        public EnumPeriodoMatriculaSequencial PeriodoMatriculaSequencial { get; set; }

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
        public int AnoLetivo { get; set; }
        public EnumPeriodoMatriculaSequencial PeriodoMatriculaSequencial { get; set; }

        public List<PlanejamentoMatriculaSequencial> MatriculasSequenciais { get; set; } =
            new List<PlanejamentoMatriculaSequencial>();

        public List<EscolaViewModel> EscolasOrigem { get; set; } = new List<EscolaViewModel>();
        public List<EscolaViewModel> EscolasDestino { get; set; } = new List<EscolaViewModel>();
    }
}
