using MaquininhaTheos.Data.Context;
using MaquininhaTheos.Domain.Entidades;
using MaquininhaTheos.Domain.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace MaquininhaTheos.Data.Repository
{
    public class UsuarioRepositorio : Repository<Usuario>, IUsuarioRepositorio
    {
        private readonly MaquininhaTheosContext _context; 
        public UsuarioRepositorio(MaquininhaTheosContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            return  _context.Usuarios.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}
