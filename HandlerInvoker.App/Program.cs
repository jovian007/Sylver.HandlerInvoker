using HandlerInvoker.ConsoleHost;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace HandlerInvoker.App
{
    class Program
    {
        static async Task Main()
        {
            IHost consoleHost = new ConsoleHostBuilder().Build();

            await consoleHost.RunAsync();
        }
    }
}
