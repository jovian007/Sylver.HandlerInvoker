using HandlerInvoker.Core.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandlerInvoker.Core.Handlers
{
    public interface IHandlerFactory
    {
        object CreateHandler(Type handlerType);

        void ReleaseHandler(object handler);
    }

    internal sealed class HandlerFactory : IHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITypeActivatorCache _typeActivatorCache;

        public HandlerFactory(IServiceProvider serviceProvider, ITypeActivatorCache typeActivatorCache)
        {
            this._serviceProvider = serviceProvider;
            this._typeActivatorCache = typeActivatorCache;
        }

        /// <inheritdoc />
        public object CreateHandler(Type handlerType)
        {
            if (handlerType == null)
            {
                throw new ArgumentNullException(nameof(handlerType));
            }

            return this._typeActivatorCache.Create<object>(this._serviceProvider, handlerType);
        }

        /// <inheritdoc />
        public void ReleaseHandler(object handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (handler is IDisposable disposableHandler)
            {
                disposableHandler.Dispose();
            }
        }
    }
}
