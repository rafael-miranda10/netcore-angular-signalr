using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.Shared
{
    public abstract class HostedServiceBase : IHostedService
    {
        // see for correct usage of IHostedService implementation: 
        // https://gist.github.com/davidfowl/a7dd5064d9dcf35b6eae1a7953d615e3

        private Task _executingTask;
        private CancellationTokenSource _cts;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _executingTask = ExecuteAsync(_cts.Token);
            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }

            _cts.Cancel();
            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));
            cancellationToken.ThrowIfCancellationRequested();
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
