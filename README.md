# HandlerInvoker

HandlerInvoker is an experimental project aiming to implement an invoker like [ASP.NET Core][aspnet-core-github] to invoke actions from handler (equivalent of `Controllers` in [ASP.NET Core][aspnet-core-github]).

> The implementation has been highly inspired by the [ASP.NET Core][aspnet-core-github] MVC source code. Thank you Microsoft and all developers of ASP.NET Core :-)

## How it works

`HandlerInvoker` has been designed to be integrated with the .NET Core generic `HostBuilder`, so it can benefit from the built-in dependency injection pattern, configuration and logging.

### Create handlers

Handler classes **must** be tagged with the `[Handler]` attribute and their actions (`public` methods) with the `[HandlerAction]` attribute so the loader can load and store the informations into the handler's cache.

`[HandlerAction]` takes on parameter which represents the unique identifier of the action. It can take whatever you want, since it takes an `object`.

Handler actions can also have parameters, like primitive values or complex objects.

```cs
[Handler]
public class CustomHandler
{
    [HandlerAction("FirstHandlerAction")]
    public void MyFirstHandlerAction(int index, MyCustomObject customObject)
    {
        // Do stuff
    }
}
```

### Load handlers

Handlers integrate well with the .NET Core [HostBuilder](https://docs.microsoft.com/en-US/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2). When you create a new application using the `HostBuilder` class, simply call the `AddHandlers()` method on the `ConfigureServices()` method of the `HostBuilder`.

```cs
static async Task Main()
{
    var host = new HostBuilder()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHandlers();
            // TODO: Add IHostedService to run a background service
        })
        .UseConsoleLifetime()
        .Build();

    await host.RunAsync();
}
```

### Invoke a handler action

To invoke a handler action, you just need to inject the `IHandlerInvoker` interface into your services and call the `Invoke()` method with the handler action in first parameter and your handler parameters.

> In this example we create a hosted service that will be registered in the `ConfigureServices` of the `HostBuilder`.

```cs
public class MyService : IHostedService
{
    private readonly IHandlerInvoker _handlerInvoker;

    public MyService(IHandlerInvoker handlerInvoker)
    {
        this._handlerInvoker = handlerInvoker;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("StartAsync()");

        // Invokes the CustomHandler.MyFirstHandlerAction with 10 as first parameter and a new custom object as second parameter.
        this._handlerInvoker.Invoke("FirstHandlerAction", 10, new MyCustomObject(42));

        return Task.CompletedTask;
}

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("StopAsync()");

        return Task.CompletedTask;
    }
}
```

## How it works under the hood

When a handler action is called, the invoke informations are cached for future invocations.
The cached informations are the following:
- Handler type : Simply the action's parent handler type.
- Handler factory creator : A `Func<object>` that creates a new instance of the handler.
- Handler releaser : An `Action<object>` that calls the `IDisposable.Dispose()` method of the handler if it implements it.
- Handler action executor : A custom object that builds an expression tree and can execute the handler action. The expression tree is built once and then cached to avoid performance issues.

# Thanks

Thank you to the [ASP.NET Core][aspnet-core-github] developers for the great code and inspiration. I've learned a lot of things by reading the code. :-)

[aspnet-core-github]: https://github.com/aspnet/AspNetCore