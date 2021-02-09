using MaquininhaTheos.Domain.Entidades;
using MaquininhaTheos.Domain.Interfaces.Notificacoes;
using MaquininhaTheos.Domain.Interfaces.Repository;
using MaquininhaTheos.Domain.Interfaces.Servicos;
using MaquininhaTheos.Domain.Shared;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.Servicos
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio; 
        public UsuarioService(
            IUsuarioRepositorio usuarioRepositorio,
            INotificationHandler notificacaoDeDominio
            ) : base(notificacaoDeDominio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public Task Salvar(Usuario usuario)
        {
            _usuarioRepositorio.AdicionarAsync(usuario);
            return Task.FromResult(true);
        }
    }
}
