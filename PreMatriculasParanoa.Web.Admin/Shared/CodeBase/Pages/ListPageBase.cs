using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Queries.Filters.Base;
using PreMatriculasParanoa.Domain.Resources;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts.Base;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages
{
    public abstract partial class ListPageBase<TEntity, TFilter, TViewModel, TApiService> : PageBase
        where TEntity : Entity 
        where TFilter : Filter
        where TViewModel : ViewModel<TEntity> 
        where TApiService : ICrudApiContract<TEntity, TFilter, TViewModel> 
    {
        [Inject] protected TApiService ApiService { get; set; }
        [Inject] protected IDialogService Dialog { get; set; }

        protected bool SomenteLeitura = true;
        protected TFilter Filtro = Activator.CreateInstance<TFilter>();
        protected TViewModel ItemSelecionado = null;
        protected HashSet<TViewModel> Selecionados = new HashSet<TViewModel>();

        protected MudTable<TViewModel> Table;
        protected DataTableModel<TViewModel> DataTable;

        protected void OnSearch(string text)
        {
            Filtro.Search = text;
            Table.ReloadServerData();
        }

        protected async Task<TableData<TViewModel>> ServerReload(TableState state, CancellationToken cancellationToken)
        {
            DataTable = new DataTableModel<TViewModel>();

            Filtro.Year = State.AnoLetivo;
            Filtro.Desc = state.SortDirection == SortDirection.Descending;
            Filtro.SortingField = state.SortLabel;

            Filtro.Page = state.Page + 1;
            Filtro.Limit = state.PageSize;

            DataTable = await Buscar(Filtro);

            return new TableData<TViewModel>() { TotalItems = DataTable.Pagination.Total, Items = DataTable.Data };
        }

        protected virtual async Task<DataTableModel<TViewModel>> Buscar(TFilter filtro)
        {
            State.Carregando = true;
            filtro.Year = State.AnoLetivo;
            var dataTable = await ApiService.Buscar(filtro);
            State.Carregando = false;
            return dataTable;
        }

        protected async Task Excluir(int id)
        {
            bool? excluir = await Dialog.ShowMessageBox("Excluir",
                GeneralMessageResource.DesejaRealmenteExcluirEsteRegistro,
                yesText: "Sim", cancelText: "Não");

            if (excluir == true)
            {
                State.Carregando = true;
                var apiResponse = await ApiService.Excluir(id);
                State.Carregando = false;

                var commandResultErrors = GetCommandResultErrors(apiResponse);
                if (commandResultErrors?.Any() == true)
                {
                    Alert(Severity.Error, commandResultErrors);
                }
                else
                {
                    Alert(Severity.Success, GeneralMessageResource.RegistroExcluidoComSucesso);
                    await Table.ReloadServerData();
                }
            }
        }
    }
}
