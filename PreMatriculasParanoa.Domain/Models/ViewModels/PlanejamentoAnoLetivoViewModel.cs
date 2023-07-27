﻿using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public string NomeEscola => Escola?.Nome;

        public EscolaViewModel Escola { get; set; }

        public List<PlanejamentoSerieAnoViewModel> SeriesAnos { get; set; } = new List<PlanejamentoSerieAnoViewModel>();

        public int TotalSeriesAnosCadastrados => SeriesAnos?.Count ?? 0;
        public int TotalTurmasCadastradas => SeriesAnos?.SelectMany(s => s.Turmas)?.Count() ?? 0;
    }
}