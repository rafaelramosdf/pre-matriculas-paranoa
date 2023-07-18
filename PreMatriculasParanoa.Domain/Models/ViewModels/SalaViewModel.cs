using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels.Validations;
using System;
using System.Text.Json.Serialization;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class SalaViewModel : ViewModel<Sala>
    {
        public override int Id => IdSala;

        public int IdSala { get; set; }

        public int Numero { get; set; }

        public string Bloco { get; set; }

        [RequiredValidation("Metragem")]
        public decimal Metragem { get; set; }

        public decimal CapacidadeFisicaPadrao { get; set; }


        public int IdEscola { get; set; }

        [JsonIgnore]
        public EscolaViewModel Escola { get; set; }

        public string NomeEscola => Escola?.Nome;

        public void CalcularCapacidadeFisicaPadrao() 
        {
            const decimal METRAGEM_POR_ALUNO = 1.2m;
            if (Metragem > METRAGEM_POR_ALUNO)
            {
                CapacidadeFisicaPadrao = Math.Round(Metragem / METRAGEM_POR_ALUNO, 2);
            }
        }
    }
}
