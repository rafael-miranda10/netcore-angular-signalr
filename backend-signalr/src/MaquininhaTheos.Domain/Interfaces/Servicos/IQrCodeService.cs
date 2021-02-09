using MaquininhaTheos.Domain.Entidades;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.Interfaces.Servicos
{
    public interface IQrCodeService
    {
        Task<string> CreateQRCode(string content);
        Task Salvar(QrCode qrCode);
    }
}
