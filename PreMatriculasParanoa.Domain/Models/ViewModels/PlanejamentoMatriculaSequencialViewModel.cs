using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class PlanejamentoMatriculaSequencialViewModel : ViewModel<PlanejamentoMatriculaSequencial>
    {
        public override int Id => IdPlanejamentoMatriculaSequencial;

        public int IdPlanejamentoMatriculaSequencial { get; set; }
        public int AnoLetivo { get; set; }
        public int SerieAnoOrigem { get; set; }
        public int SerieAnoDestino { get; set; }
        public int IdEscolaOrigem { get; set; }
        public int IdEscolaDestino { get; set; }
        public int TotalMatriculas { get; set; }
    }
}
