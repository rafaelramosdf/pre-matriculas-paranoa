using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PreMatriculasParanoa.Domain.Models.Entities
{
    public class Escola : Entity
    {
        public override int Id => IdEscola;

        [Key]
        public int IdEscola { get; set; }

        [Required]
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public EnumRegiao Regiao { get; set; }

        public ICollection<Sala> Salas { get; set; }
        public ICollection<PlanejamentoAnoLetivo> PlanejamentosAnosLetivos { get; set; }
        public ICollection<PlanejamentoMatriculaSequencial> PlanejamentosMatriculasSequenciaisOrigens { get; set; }
        public ICollection<PlanejamentoMatriculaSequencial> PlanejamentosMatriculasSequenciaisDestinos { get; set; }
    }
}
