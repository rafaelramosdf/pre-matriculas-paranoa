using PreMatriculasParanoa.Domain.Queries.Filters.Base;

namespace PreMatriculasParanoa.Domain.Queries.Filters
{
    public class SalaFilter : Filter
    {
        public int? IdSala { get; set; }
        public int? IdEscola { get; set; }
    }
}
