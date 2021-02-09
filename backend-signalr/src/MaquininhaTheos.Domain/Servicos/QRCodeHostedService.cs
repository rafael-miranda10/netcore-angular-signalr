using MaquininhaTheos.Domain.Interfaces.Servicos;
using MaquininhaTheos.Domain.Shared;
using MaquininhaTheos.Domain.TheosHub;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.Servicos
{
    public class QRCodeHostedService : HostedServiceBase
    {
        private readonly IHubContext<TheosMaquininhaHub> _hubContext;
        private readonly IOptions<TimerServiceConfiguration> _options;
        private readonly ILogger<QRCodeHostedService> _logger;
        private readonly IServiceScopeFactory _factory;
        private IQrCodeService _qrCodeService;

        public QRCodeHostedService(
            ILoggerFactory loggerFactory,
            IOptions<TimerServiceConfiguration> options,
            IHubContext<TheosMaquininhaHub> hubContext,
            IServiceScopeFactory factory,
            IQrCodeService qrCodeService
           )
        {
            _logger = loggerFactory.CreateLogger<QRCodeHostedService>();
            _options = options;
            _hubContext = hubContext;
            _factory = factory;
            _qrCodeService = qrCodeService;
        }


        private async Task ExecuteQrCodeGenerator()
        {
            var data = Guid.NewGuid().ToString();
            using (var scope = _factory.CreateScope())
            {
               // var qrCodeService = scope.ServiceProvider.GetRequiredService<QrCodeService>();
                var base64 = _qrCodeService.CreateQRCode(data).Result;
                await _hubContext.Clients.All.SendAsync("SendTestebase64", base64);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Enviando um novo QR Code ...");
                await ExecuteQrCodeGenerator();
                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);

                //TimeSpan.FromMilliseconds(_options.Value.Period)
            }
        }
    }
}
