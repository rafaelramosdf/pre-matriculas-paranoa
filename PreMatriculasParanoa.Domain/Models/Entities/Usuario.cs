using PreMatriculasParanoa.Domain.Models.Base;
using System;

namespace PreMatriculasParanoa.Domain.Models.Entities
{
    public partial class Usuario : Entity
    {
        public override int Id => IdUsuario;

        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int Uf { get; set; }
        public int IdGrupoDePermissao { get; set; }
        public int Ativo { get; set; }
        public DateTime? DataExpiracaoSenha { get; set; }
        public string CpfUsuario { get; set; }
    }
}
