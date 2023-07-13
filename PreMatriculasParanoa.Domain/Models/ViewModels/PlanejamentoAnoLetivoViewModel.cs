using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using System;
using System.Collections.Generic;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class PlanejamentoAnoLetivoViewModel : ViewModel<PlanejamentoAnoLetivo>
    {
        public override int Id => IdPlanejamentoAnoLetivo;

        public int IdPlanejamentoAnoLetivo { get; set; }
        public int AnoLetivo { get; set; }
        public DateTime? DataInicioPlanejamento { get; set; } = DateTime.Now;
        public DateTime? DataTerminoPlanejamento { get; set; } = DateTime.Now.AddMonths(3);
        public int IdEscola { get; set; }
        public EscolaViewModel Escola { get; set; } = new EscolaViewModel();
        public List<PlanejamentoSerieAnoViewModel> SeriesAnos { get; set; } = new List<PlanejamentoSerieAnoViewModel>();
    }
}
