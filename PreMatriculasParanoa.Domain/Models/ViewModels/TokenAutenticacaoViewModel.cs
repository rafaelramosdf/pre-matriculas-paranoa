using System;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class TokenAutenticacaoViewModel
    {
        public string Token { get; set; }
        public DateTime ExpiraEm { get; set; }
    }
}
