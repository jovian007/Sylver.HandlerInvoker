using HandlerInvoker.Core.Handlers;
using HandlerInvoker.Core.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HandlerInvoker.Core.Services
{
    /// <summary>
    /// Provides methods to invoke a handler action.
    /// </summary>
    internal sealed class HandlerInvoker : IHandlerInvoker
    {
        private readonly HandlerActionInvokerCache _invokerCache;

        /// <summary>
        /// Creates a new <see cref="HandlerInvoker"/> instance.
        /// </summary>
        /// <param name="invokerCache">Handler action invoker cache.</param>
        public HandlerInvoker(HandlerActionInvokerCache invokerCache)
        {
            this._invokerCache = invokerCache;
        }

        /// <inheritdoc />
        public object Invoke(object handlerAction, params object[] args)
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

            object handlerResult = null;

            try
            {
                object[] handlerActionParameters = this.PrepareParameters(args, handlerActionInvoker.HandlerExecutor);

                handlerResult = handlerActionInvoker.HandlerExecutor.Execute(targetHandler, handlerActionParameters);
            }
            catch
            {
                throw;
            }
            finally
            {
                handlerActionInvoker.HandlerReleaser(targetHandler);
            }

            return handlerResult;
        }

        /// <inheritdoc />
        public Task InvokeAsync(object handlerAction, params object[] args)
        {
            throw new NotImplementedException();
        }

        private object[] PrepareParameters(object[] parameters, HandlerExecutor executor)
        {
            if (!executor.MethodParameters.Any())
            {
                return null;
            }

            var arguments = new object[executor.MethodParameters.Count()];

            for (int i = 0; i < arguments.Length; i++)
            {
                ParameterInfo methodParameterInfo = executor.MethodParameters.ElementAt(i);

                if (i < parameters.Length)
                {
                    if (parameters[i].GetType() != methodParameterInfo.ParameterType)
                    {
                        arguments[i] = executor.GetDefaultValueForParameter(i);
                    }
                    else
                    {
                        arguments[i] = parameters[i];
                    }
                }
                else
                {
                    arguments[i] = executor.GetDefaultValueForParameter(i);
                }
            }

            return arguments;
        }
    }
}
