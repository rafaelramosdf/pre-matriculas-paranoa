using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using System.Collections.Generic;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class SalaViewModel : ViewModel<Sala>
    {
        public override int Id => IdSala;

        public int IdSala { get; set; }
        public int Numero { get; set; }
        public string Bloco { get; set; }
        public decimal Metragem { get; set; }
        public decimal CapacidadeFisicaPadrao { get; set; }
        public int IdEscola { get; set; }
        public EscolaViewModel Escola { get; set; } = new EscolaViewModel();
        public List<PlanejamentoTurmaViewModel> Turmas { get; set; } = new List<PlanejamentoTurmaViewModel>();
    }
}
