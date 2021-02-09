using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AdicionarAsync(TEntity obj);
        Task AtualizarAsync(TEntity obj);
        Task RemoverAsync(TEntity obj);
         Task<IEnumerable<TEntity>> ListarAsync();
    }
}
