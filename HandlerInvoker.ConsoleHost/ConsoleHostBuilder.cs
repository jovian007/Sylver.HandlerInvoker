using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HandlerInvoker.ConsoleHost
{
    public class ConsoleHostBuilder : IConsoleHostBuilder
    {
        public IDictionary<object, object> Properties { get; }

        /// <summary>
        /// Creates a new <see cref="ConsoleHostBuilder"/> instance.
        /// </summary>
        public ConsoleHostBuilder()
        {
        }

        /// <inheritdoc />
        public IConsoleHostBuilder AddStartupService<TStartup>()
            where TStartup : class, IConsoleStartup
        {
            // TODO: add the 
            return this;
        }

        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            // TODO
            return this;
        }

        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            // TODO
            return this;
        }

        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            // TODO
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            // TODO
            return this;
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            // TODO
            return this;
        }

        public IHost Build()
        {
            throw new NotImplementedException();
        }
    }
}
