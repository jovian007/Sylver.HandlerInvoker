using Microsoft.Extensions.Hosting;
using System;

namespace HandlerInvoker.ConsoleHost
{
    public interface IConsoleStartup : IHostedService, IDisposable
    {
    }
}
