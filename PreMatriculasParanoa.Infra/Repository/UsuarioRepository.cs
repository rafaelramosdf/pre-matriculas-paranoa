using System.Threading.Tasks;
using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using PreMatriculasParanoa.Infra.Context;
using PreMatriculasParanoa.Infra.Repository.Base;

namespace PreMatriculasParanoa.Infra.Repository
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(EntityContext context)
            : base(context)
        {
        }

        public async Task<Usuario> Authenticate(string login, string senha)
        {
            return await Task.Run(() => new Usuario());
            // TODO: Refazer com EntityFramework
            //var query = $@"{GetSelectStatement(null, null, $@"WHERE Usuario.Login = '{login}' AND Usuario.Senha = '{senha}'")}";
            //return await Dapper.DapperConnection.QueryFirstOrDefaultAsync<Usuario>(query);
        }
    }
}
