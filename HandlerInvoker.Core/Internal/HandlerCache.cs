using HandlerInvoker.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HandlerInvoker.Core.Internal
{
    internal class HandlerCache : IHandlerCache
    {
        private readonly ConcurrentDictionary<Type, HandlerModel> _handlerCache;

        public HandlerCache(IDictionary<Type, HandlerModel> cacheEntries)
        {
            this._handlerCache = new ConcurrentDictionary<Type, HandlerModel>(cacheEntries);
        }

        public HandlerModel GetHandler(Type handlerType)
        {
            return this._handlerCache.TryGetValue(handlerType, out HandlerModel value) ? value : null;
        }
    }
}
