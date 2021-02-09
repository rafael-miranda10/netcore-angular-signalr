using MaquininhaTheos.Domain.Interfaces.Servicos;
using MaquininhaTheos.Domain.TheosHub;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.Servicos
{
    public class TesteHostedService : IHostedService
    {
        private Timer _timer;
        private readonly IHubContext<TheosMaquininhaHub> _hubContext;
        private readonly ILogger<TesteHostedService> _logger;
        private IQrCodeService _qrCodeService;
        public TesteHostedService(
            ILoggerFactory loggerFactory,
            IQrCodeService qrCodeService,
            IHubContext<TheosMaquininhaHub> hubContext)
        {
            _hubContext = hubContext;
            _qrCodeService = qrCodeService;
            _logger = loggerFactory.CreateLogger<TesteHostedService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
          //  _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var data = Guid.NewGuid().ToString(); 
            var base64 = _qrCodeService.CreateQRCode(data).Result;
            _logger.LogInformation($"Enviando um novo QR Code ...");
            _hubContext.Clients.All.SendAsync("SendTestebase64", base64);
        }
    }
}
