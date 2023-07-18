using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts;
using PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages;
using System.Linq;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Pages.Planejamento.AnoLetivo
{
    public partial class PlanejamentoAnoLetivoForm : FormPageBase<PlanejamentoAnoLetivo, PlanejamentoAnoLetivoFilter, PlanejamentoAnoLetivoViewModel, IPlanejamentoAnoLetivoApiContract>
    {
        protected override void OnInit()
        {
            State.TituloPagina = "Planejamento Ano Letivo / Formulário";
            BackRoute = "/planejamento/ano-letivo";
            Model = new PlanejamentoAnoLetivoViewModel();
        }

        protected async Task AdicionarSerieAno() 
        {
            if (Model.SeriesAnos.Count < 1) 
            {
                Model.SeriesAnos.Add(new PlanejamentoSerieAnoViewModel
                {
                    IdPlanejamentoAnoLetivo = Model.IdPlanejamentoAnoLetivo,
                    SerieAno = 1
                });
            }
            else 
            {
                Model.SeriesAnos.Add(new PlanejamentoSerieAnoViewModel
                {
                    IdPlanejamentoAnoLetivo = Model.IdPlanejamentoAnoLetivo,
                    SerieAno = Model.SeriesAnos.OrderByDescending(o => o.SerieAno).First().SerieAno + 1
                });

                await OrdenarListaSeriesAnos();
            }
        }

        protected async Task ExcluirSerieAno(PlanejamentoSerieAnoViewModel serieAno) 
        {
            bool? excluir = await Dialog.ShowMessageBox($"Excluir série/ano: {serieAno.SerieAno}",
                $"Ao excluir esta série/ano, todas as {serieAno.TotalTurmas} turmas também serão excluídas. " +
                $"Deseja mesmo excluir?",
                yesText: "Sim", cancelText: "Não");

            if (excluir == true)
            {
                Model.SeriesAnos.Remove(serieAno);
                await OrdenarListaSeriesAnos();
            }
        }

        protected async Task OrdenarListaSeriesAnos()
        {
            await Task.Run(() =>
            {
                Model.SeriesAnos = Model.SeriesAnos.OrderByDescending(o => o.SerieAno).ToList();
            });
        }

        protected async Task AdicionarTurma(PlanejamentoSerieAnoViewModel serieAno) 
        {
            await Task.Run(() => 
            {
                if (serieAno.Turmas.Count < 1)
                {
                    serieAno.Turmas.Add(new PlanejamentoTurmaViewModel
                    {
                        IdPlanejamentoSerieAno = serieAno.IdPlanejamentoSerieAno,
                        SiglaTurma = 'A'
                    });
                }
                else
                {
                    serieAno.Turmas.Add(new PlanejamentoTurmaViewModel
                    {
                        IdPlanejamentoSerieAno = serieAno.IdPlanejamentoSerieAno,
                        SiglaTurma = (char)(serieAno.Turmas.OrderBy(o => o.SiglaTurma).Last().SiglaTurma + 1)
                    });
                }
            });
        }

        protected async Task ExcluirTurma(PlanejamentoSerieAnoViewModel serieAno, PlanejamentoTurmaViewModel turma)
        {
            bool? excluir = await Dialog.ShowMessageBox($"Excluir turma: '{turma.SiglaTurma}'", "Deseja mesmo excluir a turma?",
                yesText: "Sim", cancelText: "Não");

            if (excluir == true)
            {
                serieAno.Turmas.Remove(turma);
            }
        }
    }
}
