using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts;
using PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages;

namespace PreMatriculasParanoa.Web.Admin.Pages.Configuracoes.Usuarios
{
    public partial class UsuarioList : ListPageBase<Usuario, UsuarioFilter, UsuarioViewModel, IUsuarioApiContract>
    {
        protected override void OnInit()
        {
            State.TituloPagina = "Usuários do Sistema / Lista";
        }
    }
}
