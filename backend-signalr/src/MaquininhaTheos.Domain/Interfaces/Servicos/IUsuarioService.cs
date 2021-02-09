using MaquininhaTheos.Domain.Entidades;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.Interfaces.Servicos
{
    public interface IUsuarioService
    {
        Task Salvar(Usuario usuario);
    }
}
