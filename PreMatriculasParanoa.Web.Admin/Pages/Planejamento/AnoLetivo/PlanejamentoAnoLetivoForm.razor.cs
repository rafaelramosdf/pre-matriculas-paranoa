using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts;
using PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages;
using System.Collections.Generic;

namespace PreMatriculasParanoa.Web.Admin.Pages.Planejamento.AnoLetivo
{
    public partial class PlanejamentoAnoLetivoForm : FormPageBase<PlanejamentoAnoLetivo, PlanejamentoAnoLetivoFilter, PlanejamentoAnoLetivoViewModel, IPlanejamentoAnoLetivoApiContract>
    {
        protected override void OnInit()
        {
            State.TituloPagina = "Planejamento Ano Letivo / Formulário";
            BackRoute = "/planejamento/ano-letivo";

            Model = new PlanejamentoAnoLetivoViewModel 
            {
                AnoLetivo = 2024,
                SeriesAnos = new List<PlanejamentoSerieAnoViewModel> 
                {
                    new PlanejamentoSerieAnoViewModel
                    {
                        Turmas = new List<PlanejamentoTurmaViewModel> 
                        {
                            new PlanejamentoTurmaViewModel 
                            {
                            },
                            new PlanejamentoTurmaViewModel
                            {
                            }
                        }
                    },
                    new PlanejamentoSerieAnoViewModel
                    {
                        Turmas = new List<PlanejamentoTurmaViewModel>
                        {
                            new PlanejamentoTurmaViewModel
                            {
                            },
                            new PlanejamentoTurmaViewModel
                            {
                            }
                        }
                    }
                }
            };
        }
    }
}
