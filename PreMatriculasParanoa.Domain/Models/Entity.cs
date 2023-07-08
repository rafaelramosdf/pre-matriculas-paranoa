using System.ComponentModel.DataAnnotations.Schema;

namespace PreMatriculasParanoa.Domain.Models.Base
{
    public class Entity
    {
        [NotMapped]
        public virtual int Id { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
