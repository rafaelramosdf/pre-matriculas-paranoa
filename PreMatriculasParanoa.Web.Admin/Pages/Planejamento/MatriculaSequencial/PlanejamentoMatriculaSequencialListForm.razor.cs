using Microsoft.AspNetCore.Components;
using MudBlazor;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts;
using PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages;
using System.Linq;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Pages.Planejamento.MatriculaSequencial
{
    public partial class PlanejamentoMatriculaSequencialListForm : PageBase
    {
        [Inject] protected IPlanejamentoMatriculaSequencialApiContract ApiService { get; set; }
        [Inject] protected IDialogService Dialog { get; set; }


        protected PlanejamentoMatriculaSequencialFilter Filtro = new PlanejamentoMatriculaSequencialFilter();
        protected PlanejamentoMatriculaSequencialAgrupadoViewModel Model = new PlanejamentoMatriculaSequencialAgrupadoViewModel();

        protected override void OnInit()
        {
            State.TituloPagina = "Planejamento Matrícula Sequencial";
            Filtro.Year = State.AnoLetivo;
        }

        protected async Task Buscar()
        {
            State.Carregando = true;

            if (ValidarFiltro()) 
            {
                Model = await ApiService.BuscarSequencialAgrupado(Filtro);
            }
            else
            {
                Alert(Severity.Error, "Preencha os campos: \"Período\" e \"Região\"!");
            }
            
            State.Carregando = false;
        }

        protected bool ValidarFiltro()
        {
            if (Filtro.Year >= 1000 && Filtro.Regiao != Domain.Models.Enumerations.EnumRegiao.Indefinido) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected async Task Salvar()
        {
            var apiResponse = await ApiService.Incluir(Model);

            var commandResultErrors = GetCommandResultErrors(apiResponse);
            if (commandResultErrors?.Any() == true)
            {
                Alert(Severity.Error, commandResultErrors);
                return;
            }

            Alert(Severity.Success, "Dados salvos com sucesso!");
            StateHasChanged();
        }
    }
}
