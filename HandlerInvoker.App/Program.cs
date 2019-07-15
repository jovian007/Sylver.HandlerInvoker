using HandlerInvoker.App.Services;
using HandlerInvoker.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using NLog.Extensions.Hosting;
using HandlerInvoker.App.Models;

namespace HandlerInvoker.App
{
    class Program
    {
        static async Task Main()
        {
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHandlers();
                    services.AddTransient<IDefaultService, DefaultService>();
                    services.AddSingleton<IHostedService, ConsoleHostedService>();
                })
                .UseNLog()
                .UseConsoleLifetime()
                .Build();

            await host
                .AddHandlerParameterTransformer<int, ICustomInterface>((originalParam, newParam) =>
                {
                    newParam.DoSomething(originalParam * 2);

                    return newParam;
                })
                .RunAsync();
        }
    }
}
