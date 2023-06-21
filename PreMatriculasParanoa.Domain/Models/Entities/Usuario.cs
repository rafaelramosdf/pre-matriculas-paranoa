using PreMatriculasParanoa.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace PreMatriculasParanoa.Domain.Models.Entities
{
    public class Usuario : Entity
    {
        public override int Id => IdUsuario;

        [Key]
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public string Perfil { get; set; }
        public bool Ativo { get; set; }
    }
}
