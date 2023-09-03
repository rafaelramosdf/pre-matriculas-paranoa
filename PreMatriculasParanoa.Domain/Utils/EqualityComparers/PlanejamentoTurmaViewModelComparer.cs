using PreMatriculasParanoa.Domain.Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PreMatriculasParanoa.Domain.Utils.EqualityComparers
{
    public class PlanejamentoTurmaViewModelComparer : IEqualityComparer<PlanejamentoTurmaViewModel>
    {
        public bool Equals(PlanejamentoTurmaViewModel x, PlanejamentoTurmaViewModel y)
        {
            return x.TurnoPeriodo == y.TurnoPeriodo && x.IdSala == y.IdSala;
        }

        public int GetHashCode([DisallowNull] PlanejamentoTurmaViewModel obj)
        {
            // Calcula o hash code com base nas propriedades. 
            // Para cada propriedade que será comparada, deve ter um hash conforma abaixo
            int hash = 17;
            hash = hash * 23 + obj.TurnoPeriodo.GetHashCode();
            hash = hash * 23 + obj.IdSala.GetHashCode();
            return hash;
        }
    }
}
