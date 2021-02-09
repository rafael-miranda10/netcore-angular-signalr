using MaquininhaTheos.Data.Context;
using MaquininhaTheos.Domain.Entidades;
using MaquininhaTheos.Domain.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace MaquininhaTheos.Data.Repository
{
    public class QrCodeRepositorio : Repository<QrCode>, IQrCodeRepositorio
    {
        private readonly MaquininhaTheosContext _context;
        public QrCodeRepositorio(MaquininhaTheosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<QrCode> ObterPorIdAsync(int id)
        {
            return _context.QrCodes.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}
