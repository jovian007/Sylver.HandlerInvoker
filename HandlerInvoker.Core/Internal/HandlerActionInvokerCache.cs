using HandlerInvoker.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HandlerInvoker.Core.Internal
{
    internal sealed class HandlerActionInvokerCache : IDisposable
    {
        private readonly IDictionary<object, HandlerActionInvokerCacheEntry> _cache;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHandlerActionCache _handlerCache;

        public HandlerActionInvokerCache(IServiceProvider serviceProvider, IHandlerActionCache handlerCache)
        {
            this._cache = new ConcurrentDictionary<object, HandlerActionInvokerCacheEntry>();
            this._serviceProvider = serviceProvider;
            this._handlerCache = handlerCache;
        }

        public HandlerActionInvokerCacheEntry GetCachedHandlerAction(object handlerAction)
        {
            if (!this._cache.TryGetValue(handlerAction, out HandlerActionInvokerCacheEntry cacheEntry))
            {
                HandlerActionModel handlerActionModel = this._handlerCache.GetHandlerAction(handlerAction);

                if (handlerActionModel == null)
                {
                    throw new ArgumentNullException(nameof(handlerActionModel));
                }

                object handlerFactory = null;

                cacheEntry = new HandlerActionInvokerCacheEntry();

                // TODO: Create executor

                this._cache.Add(handlerAction, cacheEntry);
            }

            return cacheEntry;
        }

        public void Dispose()
        {
        }
    }

    internal class HandlerActionInvokerCacheEntry
    {
        internal HandlerActionInvokerCacheEntry()
        {

        }
    }
}
