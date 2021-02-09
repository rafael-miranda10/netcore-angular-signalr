using MaquininhaTheos.CrossCuting.Configuracoes;
using MaquininhaTheos.Data.Context;
using MaquininhaTheos.Data.Repository;
using MaquininhaTheos.Domain.Interfaces.Notificacoes;
using MaquininhaTheos.Domain.Interfaces.Repository;
using MaquininhaTheos.Domain.Interfaces.Servicos;
using MaquininhaTheos.Domain.Notificacoes;
using MaquininhaTheos.Domain.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MaquininhaTheos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<INotificationHandler, NotificationHandler>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IQrCodeRepositorio, QrCodeRepositorio>();

            services.AddScoped<IPayServerService, PayServerService>();
            services.AddScoped<IQrCodeService, QrCodeService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddScoped<MaquininhaTheosContext>();
            return services;
        }
    }
}
