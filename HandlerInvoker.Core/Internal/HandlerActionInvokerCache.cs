using HandlerInvoker.Core.Handlers;
using HandlerInvoker.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HandlerInvoker.Core.Internal
{
    internal sealed class HandlerActionInvokerCache : IDisposable
    {
        private readonly IDictionary<object, HandlerActionInvokerCacheEntry> _cache;
        private readonly IHandlerActionCache _handlerCache;
        private readonly IHandlerFactory _handlerFactory;

        public HandlerActionInvokerCache(IHandlerActionCache handlerCache, IHandlerFactory handlerFactory)
        {
            this._cache = new ConcurrentDictionary<object, HandlerActionInvokerCacheEntry>();
            this._handlerCache = handlerCache;
            this._handlerFactory = handlerFactory;
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

                cacheEntry = new HandlerActionInvokerCacheEntry(
                    handlerActionModel.HandlerTypeInfo.AsType(),
                    this._handlerFactory.CreateHandler, 
                    this._handlerFactory.ReleaseHandler,
                    new HandlerExecutor(handlerActionModel.HandlerTypeInfo, handlerActionModel.Method, null));

                this._cache.Add(handlerAction, cacheEntry);
            }

            return cacheEntry;
        }

        public void Dispose()
        {
            this._cache.Clear();
        }
    }

    internal class HandlerActionInvokerCacheEntry
    {
        public Type HandlerType { get; }

        public Func<Type, object> HandlerFactory { get; }

        public Action<object> HandlerReleaser { get; }

        public HandlerExecutor HandlerExecutor { get; }

        internal HandlerActionInvokerCacheEntry(Type handlerType, Func<Type, object> handlerFactory, Action<object> handlerReleaser, HandlerExecutor handlerExecutor)
        {
            this.HandlerType = handlerType;
            this.HandlerFactory = handlerFactory;
            this.HandlerReleaser = handlerReleaser;
            this.HandlerExecutor = handlerExecutor;
        }
    }
}
