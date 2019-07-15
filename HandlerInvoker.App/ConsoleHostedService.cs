using HandlerInvoker.App.Common;
using HandlerInvoker.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HandlerInvoker.App
{
    public class ConsoleHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<ConsoleHostedService> _logger;
        private readonly IHandlerInvoker _handlerInvoker;

        public ConsoleHostedService(ILogger<ConsoleHostedService> logger, IHandlerInvoker handlerInvoker)
        {
            this._logger = logger;
            this._handlerInvoker = handlerInvoker;
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose()");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("StartAsync()");

            this._handlerInvoker.Invoke(HandlerActionType.CreateUser, 10, 21); // Invokes CustomHander.MyFirstHandlerAction()
            this._handlerInvoker.Invoke(HandlerActionType.ShowUser, 42);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("StopAsync()");

            return Task.CompletedTask;
        }
    }
}
