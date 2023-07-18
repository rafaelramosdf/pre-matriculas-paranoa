﻿using Newtonsoft.Json;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.Entities;

namespace PreMatriculasParanoa.Domain.Models.ViewModels
{
    public class PlanejamentoTurmaViewModel : ViewModel<PlanejamentoTurma>
    {
        public override int Id => IdPlanejamentoTurma;

        public int IdPlanejamentoTurma { get; set; }
        public char SiglaTurma { get; set; }
        public string TurnoPeriodo { get; set; }
        public string TipoAtendimento { get; set; }
        public decimal CapacidadeFisicaAcordada { get; set; }

        public int IdSala { get; set; }
        public int IdPlanejamentoSerieAno { get; set; }

        [JsonIgnore]
        public SalaViewModel Sala { get; set; }
        [JsonIgnore]
        public PlanejamentoSerieAnoViewModel PlanejamentoSerieAno { get; set; }
    }
}
