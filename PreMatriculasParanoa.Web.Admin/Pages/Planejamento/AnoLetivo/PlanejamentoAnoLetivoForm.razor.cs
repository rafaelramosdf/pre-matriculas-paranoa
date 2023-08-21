using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
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
        [Inject] protected IPlanejamentoMatriculaSequencialApiContract planejamentoMatriculaSequencialApiContract { get; set; }

        protected override void OnInit()
        {
            State.TituloPagina = "Planejamento Ano Letivo / Formulário";
            BackRoute = "/planejamento/ano-letivo";
            Model = new PlanejamentoAnoLetivoViewModel();
        }

        protected override async Task GetModel()
        {
            await base.GetModel();
            await OrdenarListaSeriesAnos();
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
                    SerieAno = Model.SeriesAnos.OrderBy(o => o.SerieAno).Last().SerieAno + 1
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
            if (Model.SeriesAnos?.Any() == true)
            {
                Model.SeriesAnos = Model.SeriesAnos.OrderBy(o => o.SerieAno).ToList();
                foreach (var s in Model.SeriesAnos)
                {
                    s.PrimeiraSerieAno = false;
                    s.UltimaSerieAno = false;
                }
                Model.SeriesAnos.First().PrimeiraSerieAno = true;
                Model.SeriesAnos.Last().UltimaSerieAno = true;
                await AtualizarTotalMatriculasSequencial();
            }
        }

        protected async Task AtualizarTotalMatriculasSequencial()
        {
            try
            {
                var total = await planejamentoMatriculaSequencialApiContract.BuscarTotalPorEscolaEAnoLetivo(Model.IdEscola, Model.AnoLetivo);
                Model.SeriesAnos.Where(x => x.PrimeiraSerieAno).First().EntradaSequencial = total;
            }
            catch (System.Exception ex)
            {
                Logger.LogInformation("Erro ao tentar executar o metodo: BuscarTotalPorEscolaEAnoLetivo");
                Logger.LogError(ex, ex.Message);
            }
        }

        protected async Task AoSelecionarBotaoExibirTurmas(PlanejamentoSerieAnoViewModel serieAno)
        {
            await Task.Run(() =>
            {
                foreach (var s in Model.SeriesAnos.Where(m => m != serieAno))
                    s.ExibirDetalhesTurmas = false;

                serieAno.ExibirDetalhesTurmas = !serieAno.ExibirDetalhesTurmas;
            });
        }

        protected async Task SomarSequencialAprovadosAnoAnterior(PlanejamentoSerieAnoViewModel serieAno)
        {
            await Task.Run(() =>
            {
                if (serieAno.UltimaSerieAno == false)
                {
                    var indiceProximaSerieAno = Model.SeriesAnos.IndexOf(serieAno) + 1;
                    Model.SeriesAnos[indiceProximaSerieAno].EntradaAprovadosSerieAnoAnterior = serieAno.SaidaAprovadosAnoAtual;
                }
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
                        SiglaTurma = 'A',
                        Sala = new SalaViewModel()
                    });
                }
                else
                {
                    serieAno.Turmas.Add(new PlanejamentoTurmaViewModel
                    {
                        IdPlanejamentoSerieAno = serieAno.IdPlanejamentoSerieAno,
                        SiglaTurma = (char)(serieAno.Turmas.OrderBy(o => o.SiglaTurma).Last().SiglaTurma + 1),
                        Sala = new SalaViewModel()
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

        protected async Task ObterSalaSelecionada(SalaViewModel sala, PlanejamentoTurmaViewModel turma)
        {
            await Task.Run(() =>
            {
                turma.Sala = sala;
                turma.CapacidadeFisicaAcordada = turma.CapacidadeFisicaAcordada < 1m
                ? sala.CapacidadeFisicaPadrao : turma.CapacidadeFisicaAcordada;
            });
        }
    }
}
