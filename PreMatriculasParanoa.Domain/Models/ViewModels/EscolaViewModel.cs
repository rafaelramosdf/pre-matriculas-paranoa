using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.Enumerations;
using PreMatriculasParanoa.Domain.Models.ViewModels.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public EnumRegiao Regiao { get; set; }

        [ValidateComplexType]
        public List<SalaViewModel> Salas { get; set; } = new List<SalaViewModel>();

        public decimal CapacidadeFisica => Salas?.Sum(s => s.CapacidadeFisicaPadrao) ?? 0m;
    }
}
