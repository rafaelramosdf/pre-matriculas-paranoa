using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts;
using PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Pages.Planejamento.MatriculaSequencial
{
    public partial class PlanejamentoMatriculaSequencialForm : FormPageBase
        <PlanejamentoMatriculaSequencialAgrupado,
        PlanejamentoMatriculaSequencialFilter,
        PlanejamentoMatriculaSequencialAgrupadoViewModel,
        IPlanejamentoMatriculaSequencialApiContract>
    {
        protected override void OnInit()
        {
            State.TituloPagina = "Planejamento Matrícula Sequencial / Formulário";
            BackRoute = "/planejamento/matricula-sequencial";
            Model = new PlanejamentoMatriculaSequencialAgrupadoViewModel
            {
                MatriculasSequenciais = new List<PlanejamentoMatriculaSequencialViewModel>()
            };
        }

        protected override async Task GetModel()
        {
            await base.GetModel();
            await OrdenarListaMatriculasSequenciais();
        }

        protected async Task AdicionarMatriculaSequencial()
        {
            if (Model.IdEscolaOrigem >= 1 && Model.AnoLetivo >= 1) 
            {
                Model.MatriculasSequenciais.Add(new PlanejamentoMatriculaSequencialViewModel
                {
                    IdEscolaOrigem = Model.IdEscolaOrigem,
                    EscolaOrigem = Model.EscolaOrigem,
                    AnoLetivo = Model.AnoLetivo,
                    SerieAnoOrigem = Model.SerieAnoOrigem
                });

                await OrdenarListaMatriculasSequenciais();
            }
            else 
            {
                await Dialog.ShowMessageBox("Atenção!", "Preencha os campos \"Escola origem\" e \"Ano letivo\",  primeiro!");
            }
        }

        protected async Task ExcluirMatriculaSequencial(PlanejamentoMatriculaSequencialViewModel matriculaSequencialViewModel)
        {
            bool? excluir = await Dialog.ShowMessageBox($"Excluir", "Deseja mesmo excluir?", 
                yesText: "Sim", cancelText: "Não");

            if (excluir == true)
            {
                Model.MatriculasSequenciais.Remove(matriculaSequencialViewModel);
                await OrdenarListaMatriculasSequenciais();
            }
        }

        protected async Task OrdenarListaMatriculasSequenciais()
        {
            await Task.Run(() =>
            {
                Model.MatriculasSequenciais = Model.MatriculasSequenciais.OrderBy(o => o.NomeEscolaOrigem).ToList();
            });
        }
    }
}
