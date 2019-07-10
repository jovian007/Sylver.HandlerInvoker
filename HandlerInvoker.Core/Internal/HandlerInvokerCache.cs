using Microsoft.Extensions.DependencyInjection;
using System;

namespace HandlerInvoker.Core.Internal
{
    internal sealed class HandlerInvokerCache : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHandlerCache _handlerCache;

        public HandlerInvokerCache(IServiceProvider serviceProvider, IHandlerCache handlerCache)
        {
            this._serviceProvider = serviceProvider;
            this._handlerCache = handlerCache;
        }

        public void Dispose()
        {
        }
    }
}
