using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public partial class UsuarioViewModel : ViewModel<Usuario>
    {
        public override int Id => IdUsuario;
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public string Perfil { get; set; }
    }
}
