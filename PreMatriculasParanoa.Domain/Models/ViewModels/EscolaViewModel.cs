using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels.Validations;
using System.Collections.Generic;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class EscolaViewModel : ViewModel<Escola>
    {
        public override int Id => IdEscola;
        public int IdEscola { get; set; }

        [RequiredValidation("Nome da escola")]
        public string Nome { get; set; }
        
        public string Cidade { get; set; }
        
        public string Bairro { get; set; }

        public List<SalaViewModel> Salas { get; set; } = new List<SalaViewModel>();
    }
}
