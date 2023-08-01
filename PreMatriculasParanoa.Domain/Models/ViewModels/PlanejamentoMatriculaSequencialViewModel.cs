using Newtonsoft.Json;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels.Validations;
using System.Collections.Generic;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class PlanejamentoMatriculaSequencialViewModel : ViewModel<PlanejamentoMatriculaSequencial>
    {
        public override int Id => IdPlanejamentoMatriculaSequencial;

        public int IdPlanejamentoMatriculaSequencial { get; set; }

        [RequiredValidation("Ano Letivo")]
        public int AnoLetivo { get; set; }

        public int SerieAnoOrigem { get; set; }
        public int SerieAnoDestino => SerieAnoOrigem + 1;


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
    }

    public class PlanejamentoMatriculaSequencialAgrupadoViewModel : ViewModel<PlanejamentoMatriculaSequencialAgrupado> 
    {
        public List<PlanejamentoMatriculaSequencialViewModel> MatriculasSequenciais { get; set; } = new List<PlanejamentoMatriculaSequencialViewModel>();

        [RequiredValidation("Ano Letivo")]
        public int AnoLetivo { get; set; }

        public int SerieAnoOrigem { get; set; }
        public int SerieAnoDestino => SerieAnoOrigem + 1;


        [RequiredValidation("EscolaOrigem")]
        public int IdEscolaOrigem { get; set; }
        public string NomeEscolaOrigem => EscolaOrigem?.Nome;
        public EscolaViewModel EscolaOrigem { get; set; }

        public int TotalGeralMatriculas => MatriculasSequenciais.Sum(s => s.TotalMatriculas);
    }
}
