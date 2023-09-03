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
using PreMatriculasParanoa.Domain.Extensions;

namespace PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages
{
    public abstract partial class FormPageBase<TEntity, TFilter, TViewModel, TApiService> : PageBase 
        where TEntity : Entity
        where TFilter : Filter
        where TViewModel : ViewModel<TEntity>
        where TApiService : ICrudApiContract<TEntity, TFilter, TViewModel>
    {
        [Inject] protected ILogger<FormPageBase<TEntity, TFilter, TViewModel, TApiService>> Logger { get; set; }
        [Inject] protected TApiService ApiService { get; set; }
        [Inject] protected IDialogService Dialog { get; set; }

        [Parameter] public int? Id { get; set; }
        protected bool FormValid { get; set; } = false;

        protected bool IsNewRegister => Id == null;
        protected string BackRoute { get; set; }
        protected TViewModel Model { get; set; } = Activator.CreateInstance<TViewModel>();
        protected TViewModel ModelOriginal { get; set; } = Activator.CreateInstance<TViewModel>();

        protected override async Task OnInitializedAsync()
        {
            if (!IsNewRegister)
            {
                await GetModel();
            }            
            ModelOriginal = Model.Clone();
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Task.Delay(2500);
                await JSRun.InvokeVoidAsync("setMaskInput");

                var dotNetObjectReference = DotNetObjectReference.Create(this);
                await JSRun.InvokeVoidAsync("blazor.registerGetAlertSaveChangesOnBeforeUnload", dotNetObjectReference, nameof(GetAlertSaveChangesOnBeforeUnload));
            }
        }

        [JSInvokable]
        public async Task<string> GetAlertSaveChangesOnBeforeUnload()
        {
            var msg = "";
            var hasChanges = await HasChangesCheck();
            if (hasChanges)
            {
                msg = "Existem dados alterados que não foram salvos! Deseja realmente sair?";
            }

            return msg;
        }

        [JSInvokable]
        public async Task<bool> HasChangesCheck()
        {
            return await Task.Run(() => 
            {
                var hasChanges = Model.Clone().CompareObjects(ModelOriginal) == false;
                return hasChanges;
            });
        }

        protected async Task ValidateSaveOnBeforeOpenRoute()
        {
            var hasChanges = await HasChangesCheck();
            if (hasChanges)
            {
                var msg = "Existem dados alterados que não foram salvos! Deseja salvar?";
                bool? salvar = await Dialog.ShowMessageBox("Atenção!", msg, yesText: "Sim", cancelText: "Não");

                if (salvar == true)
                {
                    await Submit(new EditContext(Model));
                }
            }
        }

        protected virtual async Task GetModel()
        {
            State.Carregando = true;
            Model = await ApiService.Buscar(Id.Value);
            State.Carregando = false;
        }

        protected virtual async Task Submit(EditContext context)
        {
            await _Submit(context);
            if(FormValid) 
            {
                Alert(Severity.Success, GeneralMessageResource.DadosSalvosComSucesso);
                StateHasChanged();
            }
        }

        protected virtual async Task SubmitAndExit(EditContext context)
        {
            await _Submit(context);

            if(FormValid) 
            {
                await OpenRoute(BackRoute);
                Alert(Severity.Success, GeneralMessageResource.DadosSalvosComSucesso);
                StateHasChanged();
            }
        }

        private async Task _Submit(EditContext context) 
        {
            FormValid = context.Validate();

            if (!FormValid)
            {
                Alert(Severity.Error, context.GetValidationMessages().ToList());
                return;
            }

            var viewModelValidations = Model.ViewModelValidations();
            if (viewModelValidations.HasError) 
            {
                FormValid = false;
                Alert(Severity.Error, viewModelValidations.Errors.ToList());
                return;
            }

            var apiResponse = IsNewRegister
            ? await ApiService.Incluir(Model)
            : await ApiService.Alterar((int)Id, Model);

            var commandResultErrors = GetCommandResultErrors(apiResponse);

            if (commandResultErrors?.Any() == true)
            {
                FormValid = false;
                Alert(Severity.Error, commandResultErrors);
                return;
            }

            ModelOriginal = Model.Clone();
        }

        public override async ValueTask DisposeAsync()
        {
            await ValidateSaveOnBeforeOpenRoute();
            await base.DisposeAsync();
        }
    }
}
