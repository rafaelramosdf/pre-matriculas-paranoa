using PreMatriculasParanoa.Domain.Models.Enumerations;
using PreMatriculasParanoa.Domain.Queries.Filters.Base;

namespace PreMatriculasParanoa.Domain.Queries.Filters
{
    public class PlanejamentoMatriculaSequencialFilter : Filter
    {
        public int AnoLetivo { get; set; }
        public EnumPeriodoMatriculaSequencial PeriodoMatriculaSequencial { get; set; }
        public EnumRegiao Regiao { get; set; }
    }
}
