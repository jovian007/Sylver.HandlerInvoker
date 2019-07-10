using HandlerInvoker.App.Common;
using HandlerInvoker.ConsoleHost;
using HandlerInvoker.Core;
using HandlerInvoker.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HandlerInvoker.App
{
    class Program
    {
        static async Task Main()
        {
            //IHost consoleHost = new ConsoleHostBuilder().Build();

            //await consoleHost.RunAsync();

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHandlers();
                    services.AddSingleton<IHostedService, ConsoleHostedService>();
                })
                .UseConsoleLifetime()
                .Build();

            await host.RunAsync();

        }
    }

    public class ConsoleHostedService : IHostedService, IDisposable
    {
        private readonly IHandlerInvokerService _handlerInvoker;

        public ConsoleHostedService(IHandlerInvokerService handlerInvoker)
        {
            this._handlerInvoker = handlerInvoker;
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose()");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StartAsync()");

            this._handlerInvoker.Invoke(HandlerActionType.CreateUser); // Invokes CustomHander.MyFirstHandlerAction()

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StopAsync()");

            return Task.CompletedTask;
        }
    }
}
