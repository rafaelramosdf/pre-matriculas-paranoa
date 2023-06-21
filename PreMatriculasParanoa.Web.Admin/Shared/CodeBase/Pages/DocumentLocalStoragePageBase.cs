using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;

namespace PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages
{
    public abstract partial class DocumentLocalStoragePageBase : ComponentBase, IDisposable
    {
        [Inject] protected StateContainer State { get; set; }
        [Inject] protected ISyncLocalStorageService LocalStorage { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            State.OnChange += StateHasChanged;
        }

        protected const string DATA_IMPRESSAO_DOCUMENTO_KEY = "DOC_DATA_IMPRESSAO";

        protected void LimparLocalStorage()
        {
            DataImpressaoDocumento = DateTime.Now;
        }

        protected DateTime? DataImpressaoDocumento
        {
            get => LocalStorage.ContainKey(DATA_IMPRESSAO_DOCUMENTO_KEY)
                ? LocalStorage.GetItem<DateTime>(DATA_IMPRESSAO_DOCUMENTO_KEY)
                : null;

            set => LocalStorage.SetItem(DATA_IMPRESSAO_DOCUMENTO_KEY, value);
        }

        public void Dispose()
        {
            State.OnChange -= StateHasChanged;
        }
    }
}
