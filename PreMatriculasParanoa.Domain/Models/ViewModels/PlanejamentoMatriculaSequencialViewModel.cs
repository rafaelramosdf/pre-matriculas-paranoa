using Newtonsoft.Json;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.Enumerations;
using PreMatriculasParanoa.Domain.Models.ViewModels.Validations;
using System.Collections.Generic;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class PlanejamentoMatriculaSequencialViewModel : ViewModel<PlanejamentoMatriculaSequencial>
    {
        public override int Id => IdPlanejamentoMatriculaSequencial;

        public int IdPlanejamentoMatriculaSequencial { get; set; }

        [RequiredValidation("Ano Letivo")]
        public int AnoLetivo { get; set; }

        public EnumPeriodoMatriculaSequencial PeriodoMatriculaSequencial { get; set; }


        [RequiredValidation("EscolaOrigem")]
        public int IdEscolaOrigem { get; set; }
        public string NomeEscolaOrigem => EscolaOrigem?.Nome;
        [JsonIgnore]
        public EscolaViewModel EscolaOrigem { get; set; }


        [RequiredValidation("EscolaDestino")]
        public int IdEscolaDestino { get; set; }
        public string NomeEscolaDestino => EscolaDestino?.Nome;
        [JsonIgnore]
        public EscolaViewModel EscolaDestino { get; set; }

        public int TotalMatriculas { get; set; }

        public int TotalVagasDisponiveis { get; set; }
    }

    public class PlanejamentoMatriculaSequencialAgrupadoViewModel : ViewModel<PlanejamentoMatriculaSequencialAgrupado>
    {

        [RequiredValidation("Ano Letivo")]
        public int AnoLetivo { get; set; }
        public EnumPeriodoMatriculaSequencial PeriodoMatriculaSequencial { get; set; }

        public List<PlanejamentoMatriculaSequencialViewModel> MatriculasSequenciais { get; set; } = 
            new List<PlanejamentoMatriculaSequencialViewModel>();

        public List<EscolaViewModel> EscolasOrigem { get; set; } = new List<EscolaViewModel>();
        public List<EscolaViewModel> EscolasDestino { get; set; } = new List<EscolaViewModel>();
    }
}
