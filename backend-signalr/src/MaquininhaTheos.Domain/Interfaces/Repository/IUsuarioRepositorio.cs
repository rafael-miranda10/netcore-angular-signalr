using MaquininhaTheos.Domain.Entidades;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.Interfaces.Repository
{
    public interface IUsuarioRepositorio : IRepository<Usuario>
    {
        Task<Usuario> ObterPorIdAsync(int id);
    }
}
