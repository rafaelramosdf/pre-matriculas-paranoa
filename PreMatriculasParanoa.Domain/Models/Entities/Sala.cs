using PreMatriculasParanoa.Domain.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreMatriculasParanoa.Domain.Models.Entities
{
    public class Sala : Entity
    {
        public override int Id => IdSala;

        [Key]
        public int IdSala { get; set; }
        public string Numero { get; set; }
        public string Bloco { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Metragem { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CapacidadeFisicaPadrao { get; set; }

        [ForeignKey("Escola")]
        public int IdEscola { get; set; }
        public Escola Escola { get; set; }

        public ICollection<PlanejamentoTurma> Turmas { get; set; }
    }
}
