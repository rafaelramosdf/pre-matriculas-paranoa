using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Queries.Filters.Base;
using PreMatriculasParanoa.Domain.Resources;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Models;
using System.Collections.Generic;

namespace PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages
{
    public abstract partial class FormPageBase<TEntity, TFilter, TViewModel, TApiService> : PageBase
        where TEntity : Entity
        where TFilter : Filter
        where TViewModel : ViewModel<TEntity>
        where TApiService : ICrudApiContract<TEntity, TFilter, TViewModel>
    {
        [Inject] protected ILogger<FormPageBase<TEntity, TFilter, TViewModel, TApiService>> Log { get; set; }
        [Inject] protected TApiService ApiService { get; set; }
        [Inject] protected IDialogService Dialog { get; set; }

        [Parameter] public int? Id { get; set; }
        protected bool FormValid { get; set; } = false;

        protected bool IsNewRegister => Id == null;
        protected string BackRoute { get; set; }
        protected TViewModel Model { get; set; } = Activator.CreateInstance<TViewModel>();

        protected override async Task OnInitializedAsync()
        {
            if (!IsNewRegister)
            {
                await GetModel();
            }

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Task.Delay(2500);
                await JSRun.InvokeVoidAsync("setMaskInput");
            }
        }

        protected virtual async Task GetModel()
        {
            State.Carregando = true;
            Model = await ApiService.Buscar(Id.Value);
            State.Carregando = false;
        }

        protected virtual async Task OnSubmit(EditContext context)
        {
            FormValid = context.Validate();

            if (!FormValid)
            {
                Alert(Severity.Error, context.GetValidationMessages().ToList());
                return;
            }

            var apiResponse = IsNewRegister
            ? await ApiService.Incluir(Model)
            : await ApiService.Alterar((int)Id, Model);

            var commandResultErrors = GetCommandResultErrors(apiResponse);

            if (commandResultErrors?.Any() == true) 
            {
                Alert(Severity.Error, commandResultErrors);
                return;
            }

            OpenRoute(BackRoute);
            Alert(Severity.Success, GeneralMessageResource.DadosSalvosComSucesso);
            StateHasChanged();
        }
    }
}
