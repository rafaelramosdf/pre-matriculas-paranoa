using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class EscolaViewModel : ViewModel<Escola>
    {
        public override int Id => IdEscola;
        public int IdEscola { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }

        public List<SalaViewModel> Salas { get; set; } = new List<SalaViewModel>();
        public List<PlanejamentoAnoLetivoViewModel> PlanejamentosAnosLetivos { get; set; } = new List<PlanejamentoAnoLetivoViewModel>();
    }
}
