using HandlerInvoker.Core.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HandlerInvoker.Core.Internal
{
    internal class HandlerActionCache : IHandlerActionCache
    {
        private readonly IDictionary<object, HandlerActionModel> _handlerCache;

        public HandlerActionCache(IDictionary<object, HandlerActionModel> cacheEntries)
        {
            this._handlerCache = new ConcurrentDictionary<object, HandlerActionModel>(cacheEntries);
        }

        public HandlerActionModel GetHandlerAction(object handlerAction)
        {
            return this._handlerCache.TryGetValue(handlerAction, out HandlerActionModel value) ? value : null;
        }
    }
}
