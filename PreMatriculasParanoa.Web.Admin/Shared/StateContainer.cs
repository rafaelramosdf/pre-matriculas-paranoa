using System;

namespace PreMatriculasParanoa.Web.Admin.Shared
{
    public class StateContainer
    {
        private string tituloPagina { get; set; }
        public string TituloPagina 
        { 
            get => tituloPagina;
            set
            {
                tituloPagina = value;
                NotifyStateChanged();
            } 
        }

        private bool carregando { get; set; }
        public bool Carregando
        {
            get => carregando;
            set
            {
                carregando = value;
                NotifyStateChanged();
            }
        }

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
