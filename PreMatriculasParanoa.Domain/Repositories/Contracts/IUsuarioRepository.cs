using System.Threading.Tasks;
using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Repositories.Base;

namespace PreMatriculasParanoa.Domain.Repositories.Contracts
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> Authenticate(string login, string senha);
    }
}
