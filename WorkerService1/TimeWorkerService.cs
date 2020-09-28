using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerService1
{
    public class TimeWorkerService : IHostedService, IDisposable
    {
        private readonly ILogger<TimeWorkerService> _logger;
        private Timer _timer;
        public TimeWorkerService(ILogger<TimeWorkerService> logger)
        {
            _logger = logger;

        }
        public void Dispose()
        {
            _logger.LogInformation("Dispose event called");
            _timer?.Dispose();
        }
        private void OnTimer(object state)
        {
            _logger.LogInformation("OnTime event called");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StartAsync event called");
            _timer = new Timer(OnTimer, cancellationToken, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StopAsync event called");
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}