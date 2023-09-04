using PreMatriculasParanoa.Domain.Models.Enumerations;
using PreMatriculasParanoa.Domain.Queries.Filters.Base;

namespace PreMatriculasParanoa.Domain.Queries.Filters
{
    public class PlanejamentoMatriculaSequencialFilter : Filter
    {
        public EnumPeriodoMatriculaSequencial PeriodoMatriculaSequencial { get; set; }
        public EnumRegiao Regiao { get; set; }
        public int IdEscolaDestino { get; set; }
    }
}
