using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts;
using PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages;
using System.Linq;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Pages.Configuracoes.EscolasSalas
{
    public partial class EscolasSalasForm : FormPageBase<Escola, EscolaFilter, EscolaViewModel, IEscolaSalaApiContract>
    {
        protected override void OnInit()
        {
            State.TituloPagina = "Escolas e Salas / Formulário";
            BackRoute = "/configuracoes/escolas-salas";
            Model = new EscolaViewModel();
        }

        protected async Task AdicionarSala() 
        {
            if (Model.Salas.Count < 1) 
            {
                Model.Salas.Add(new SalaViewModel
                {
                    IdEscola = Model.IdEscola
                });
            }
            else 
            {
                Model.Salas.Add(new SalaViewModel
                {
                    IdEscola = Model.IdEscola
                });

                await OrdenarListaSalas();
            }
        }

        protected async Task ExcluirSala(SalaViewModel sala) 
        {
            bool? excluir = await Dialog.ShowMessageBox($"Excluir sala: {sala.Numero}", "Deseja mesmo excluir?",
                yesText: "Sim", cancelText: "Não");

            if (excluir == true)
            {
                Model.Salas.Remove(sala);
                await OrdenarListaSalas();
            }
        }

        protected async Task OrdenarListaSalas()
        {
            await Task.Run(() =>
            {
                Model.Salas = Model.Salas.OrderBy(o => o.Numero).ToList();
            });
        }
    }
}
