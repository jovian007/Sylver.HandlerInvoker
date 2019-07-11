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
            HandlerActionInvokerCacheEntry handlerActionExecutor = this._invokerCache.GetCachedHandlerAction(handlerAction);
        }

        public Task InvokeAsync(object handlerAction, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
