using HandlerInvoker.App.Common;
using HandlerInvoker.App.Handlers;
using HandlerInvoker.App.Services;
using HandlerInvoker.ConsoleHost;
using HandlerInvoker.Core;
using HandlerInvoker.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                    services.AddTransient<IDefaultService, DefaultService>();
                    services.AddSingleton<IHostedService, ConsoleHostedService>();
                })
                .UseConsoleLifetime()
                .Build();

            await host.RunAsync();

        }
    }

    public class ConsoleHostedService : IHostedService, IDisposable
    {
        private readonly IHandlerInvoker _handlerInvoker;

        public ConsoleHostedService(IHandlerInvoker handlerInvoker)
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

            this._handlerInvoker.Invoke(HandlerActionType.CreateUser, 10); // Invokes CustomHander.MyFirstHandlerAction()

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StopAsync()");

            return Task.CompletedTask;
        }
    }
}
