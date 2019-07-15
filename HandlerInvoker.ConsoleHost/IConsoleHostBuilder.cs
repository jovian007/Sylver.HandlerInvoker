using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HandlerInvoker.ConsoleHost
{
    public interface IConsoleHostBuilder
    {
        /// <summary>
        /// Adds a service that will be started when the host starts.
        /// </summary>
        /// <typeparam name="TStartup">Service type.</typeparam>
        /// <returns></returns>
        IConsoleHostBuilder AddStartupService<TStartup>() where TStartup : class, IConsoleStartup;

        IConsoleHostBuilder ConfigureHost(Action<IHostBuilder> hostBuilderConfigurator);

        IConsoleHostBuilder ConfigureServices(Action<IServiceCollection> services);

        IHost Build();
    }
}
