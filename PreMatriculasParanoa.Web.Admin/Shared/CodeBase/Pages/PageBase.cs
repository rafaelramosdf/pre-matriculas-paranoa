using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages
{
    public abstract partial class PageBase : ComponentBase, IDisposable
    {
        [Inject] protected StateContainer State { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IJSRuntime JSRun { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

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

        public void Dispose()
        {
            State.OnChange -= StateHasChanged;
        }
    }
}
