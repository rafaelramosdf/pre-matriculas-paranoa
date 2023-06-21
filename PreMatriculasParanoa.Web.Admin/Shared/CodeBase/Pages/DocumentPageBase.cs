using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages
{
    public abstract partial class DocumentPageBase : DocumentLocalStoragePageBase
    {
        [Inject] protected IJSRuntime JSRun { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            LimparLocalStorage();
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
    }
}
