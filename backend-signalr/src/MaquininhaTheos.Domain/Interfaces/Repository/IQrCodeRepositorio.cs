using MaquininhaTheos.Domain.Entidades;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.Interfaces.Repository
{
    public interface IQrCodeRepositorio : IRepository<QrCode>
    {
        Task<QrCode> ObterPorIdAsync(int id);
    }
}
