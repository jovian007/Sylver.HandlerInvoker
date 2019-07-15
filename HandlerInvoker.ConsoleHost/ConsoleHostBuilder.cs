using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HandlerInvoker.ConsoleHost
{
    public class ConsoleHostBuilder : IConsoleHostBuilder
    {
        private readonly HostBuilder _hostBuilder;

        private Action<IHostBuilder> _hostBuilderConfigurationActions;

        /// <summary>
        /// Creates a new <see cref="ConsoleHostBuilder"/> instance.
        /// </summary>
        public ConsoleHostBuilder()
        {
            this._hostBuilder = new HostBuilder();
            
        }

        /// <inheritdoc />
        public IConsoleHostBuilder AddStartupService<TStartup>()
            where TStartup : class, IConsoleStartup
        {
            this._hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IHostedService, TStartup>();
            });

            return this;
        }

        /// <inheritdoc />
        public IConsoleHostBuilder ConfigureHost(Action<IHostBuilder> hostBuilderConfigurator)
        {
            this._hostBuilderConfigurationActions += hostBuilderConfigurator;

            return this;
        }

        /// <inheritdoc />
        public IConsoleHostBuilder ConfigureServices(Action<IServiceCollection> services)
        {
            this._hostBuilder.ConfigureServices(services);

            return this;
        }

        /// <inheritdoc />
        public IHost Build()
        {
            this._hostBuilder.UseConsoleLifetime();

            this._hostBuilderConfigurationActions?.Invoke(this._hostBuilder);

            return this._hostBuilder.Build();
        }
    }
}
