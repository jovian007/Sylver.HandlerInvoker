using Microsoft.Extensions.Hosting;

namespace HandlerInvoker.ConsoleHost
{
    public interface IConsoleHostBuilder : IHostBuilder
    {
        IConsoleHostBuilder AddStartupService<TStartup>() where TStartup : class, IConsoleStartup;
    }
}
