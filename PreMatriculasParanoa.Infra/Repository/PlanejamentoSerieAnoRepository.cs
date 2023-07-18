using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using PreMatriculasParanoa.Infra.Context;
using PreMatriculasParanoa.Infra.Repository.Base;

namespace PreMatriculasParanoa.Infra.Repository
{
    public class PlanejamentoSerieAnoRepository : BaseRepository<PlanejamentoSerieAno>, IPlanejamentoSerieAnoRepository
    {
        public PlanejamentoSerieAnoRepository(EntityContext context)
            : base(context)
        {
        }
    }
}
