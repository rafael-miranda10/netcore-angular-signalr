using FluentValidation.Results;
using MaquininhaTheos.Domain.Interfaces.Notificacoes;
using MaquininhaTheos.Domain.Notificacoes;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.Shared
{
    public abstract class BaseService
    {
        protected readonly INotificationHandler _notificacaoDeDominio;

        protected BaseService(INotificationHandler notificacaoDeDominio)
        {
            _notificacaoDeDominio = notificacaoDeDominio;
        }

        public async Task NotificarValidacoesDeDominio(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notificacaoDeDominio.Handle(new Notification(error.ErrorMessage));
            }
        }

        public async Task NotificarValidacoesDeServico(string mensagem)
        {
            _notificacaoDeDominio.Handle(new Notification(mensagem));
        }

    }
}
