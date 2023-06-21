namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class DadosAutenticacaoUsuarioViewModel
    {
        public string IdUsuario { get; set; }
        public string IdGrupoPermissao { get; set; }
        public string Usuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Autenticado { get; set; }
        public string Permissoes { get; set; }
    }
}
