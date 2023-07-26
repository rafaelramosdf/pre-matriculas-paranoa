using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages
{
    public abstract partial class PageBase : ComponentBase, IDisposable
    {
        [Inject] protected StateContainer State { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IJSRuntime JSRun { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        const string INTERNAL_ERROR_MESSAGE_DEFAULT = "Erro interno";
        const string USER_ERROR_MESSAGE_DEFAULT = "Houve um erro inesperado na requisição! verifique se os dados estão corretos, e tente novamente, ou procure o administrador do sistema.";

        protected virtual void OnInit() { }

        protected override void OnInitialized()
        {
            State.OnChange += StateHasChanged;
            OnInit();
        }

        protected void OpenRoute(string route, bool forceLoad = false)
        {
            NavigationManager.NavigateTo(route, forceLoad);
        }
        protected async Task OpenRouteInTab(string route)
        {
            await JSRun.InvokeAsync<object>("open", route, "_blank");
        }
        protected async Task OpenRouteInPopup(string route)
        {
            await JSRun.InvokeAsync<object>("open", route, "_blank", "width=800,height=600,toolbar=0,location=0,scrollbars=0,status=0");
        }

        protected void Alert(Severity severity, string message)
        {
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
            Snackbar.Configuration.PreventDuplicates = false;
            Snackbar.Configuration.NewestOnTop = false;
            Snackbar.Configuration.ShowCloseIcon = true;
            Snackbar.Configuration.VisibleStateDuration = 10000;
            Snackbar.Configuration.HideTransitionDuration = 500;
            Snackbar.Configuration.ShowTransitionDuration = 500;
            Snackbar.Configuration.SnackbarVariant = Variant.Filled;

            Snackbar.Add(message, severity);
        }

        protected void Alert(Severity severity, List<string> messages)
        {
            Alert(severity, $"• {string.Join("<br />• ", messages)}");
        }

        protected RenderFragment ObterTextoPadraoConteudoVazioMudTable(string mensagem = "Nenhum item encontrado")
        {
            return new RenderFragment(builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "mud-alert mud-alert-standard mud-alert-outlined mud-alert-info");
                builder.AddContent(2, mensagem);
                builder.CloseElement();
            });
        }

        protected List<string> GetCommandResultErrors(ApiResponse<CommandResult> apiResponse) 
        {
            List<string> messageErrors = new List<string>();

            try
            {
                if (apiResponse?.IsSuccessStatusCode != true)
                {
                    if (apiResponse?.Error != null)
                    {
                        messageErrors.Add(USER_ERROR_MESSAGE_DEFAULT);
                        messageErrors.Add(apiResponse.Error.Message);

                        Console.WriteLine(string.Concat(messageErrors));
                        Console.WriteLine(JsonConvert.SerializeObject(apiResponse.Error));
                    }
                }
                else
                {
                    CommandResult result = apiResponse.Content;
                    if (result?.HasError == true)
                    {
                        messageErrors.AddRange(result.Errors.ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                messageErrors.Add(USER_ERROR_MESSAGE_DEFAULT);
                Console.WriteLine(INTERNAL_ERROR_MESSAGE_DEFAULT);
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }

            return messageErrors;
        }

        public void Dispose()
        {
            State.OnChange -= StateHasChanged;
        }
    }
}
