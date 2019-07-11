using HandlerInvoker.Core.Internal;
using System;
using System.Threading.Tasks;

namespace HandlerInvoker.Core.Services
{
    public interface IHandlerInvokerService
    {
        void Invoke(object handlerAction, params object[] args);

        Task InvokeAsync(object handlerAction, params object[] args);
    }

    internal sealed class HandlerInvokerService : IHandlerInvokerService
    {
        private readonly HandlerActionInvokerCache _invokerCache;

        public HandlerInvokerService(HandlerActionInvokerCache invokerCache)
        {
            this._invokerCache = invokerCache;
        }

        public void Invoke(object handlerAction, params object[] args)
        {
            HandlerActionInvokerCacheEntry handlerActionInvoker = this._invokerCache.GetCachedHandlerAction(handlerAction);

            if (handlerActionInvoker == null)
            {
                throw new ArgumentNullException(nameof(handlerActionInvoker));
            }

            var targetHandler = handlerActionInvoker.HandlerFactory(handlerActionInvoker.HandlerType);

            if (targetHandler == null)
            {
                throw new ArgumentNullException(nameof(targetHandler));
            }

            handlerActionInvoker.HandlerExecutor.Execute(targetHandler, args);
        }

        public Task InvokeAsync(object handlerAction, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
