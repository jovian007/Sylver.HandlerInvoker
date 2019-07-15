using HandlerInvoker.Core.Attributes;
using HandlerInvoker.Core.Internal;
using HandlerInvoker.Core.Internal.Transformers;
using HandlerInvoker.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HandlerInvoker.Core
{
    public static class HandlerServiceCollectionExtensions
    {
        /// <summary>
        /// Add handlers to the container.
        /// </summary>
        /// <param name="services">Service collection container.</param>
        public static void AddHandlers(this IServiceCollection services)
        {
            services.TryAddSingleton<IHandlerActionCache>(s => HandlerCacheFactory());
            services.TryAddSingleton<HandlerActionInvokerCache>();
            services.TryAddSingleton<IHandlerInvoker, HandlerActionInvoker>();
            services.TryAddSingleton<IHandlerFactory, HandlerFactory>();

            services.TryAddSingleton<ParameterTransformerCache>();
            services.TryAddSingleton<IParameterTransformer, ParameterTransformer>();

            services.TryAddSingleton<ITypeActivatorCache, TypeActivatorCache>();
        }

        public static IHost AddHandlerParameterTransformer<TSource, TDest>(this IHost host, Func<TSource, TDest, TDest> transformer)
        {
            var transformerModel = new TransformerModel(typeof(TSource).GetTypeInfo(), typeof(TDest).GetTypeInfo(), (a, b) => transformer((TSource)a, (TDest)b));
            var transformersCache = host.Services.GetRequiredService<ParameterTransformerCache>();

            transformersCache.AddTransformer(transformerModel);

            return host;
        }

        /// <summary>
        /// Loads handlers and handler actions and store them into a cache.
        /// </summary>
        /// <returns><see cref="HandlerActionCache"/> containing the handler actions.</returns>
        private static HandlerActionCache HandlerCacheFactory()
        {
            var handlerCacheEntries = new Dictionary<object, HandlerActionModel>();
            IEnumerable<Type> handlers = from x in Assembly.GetEntryAssembly().GetTypes()
                                         where x.GetCustomAttributes<HandlerAttribute>().Any()
                                         select x;

            foreach (Type handlerType in handlers)
            {
                TypeInfo handlerTypeInfo = handlerType.GetTypeInfo();
                IEnumerable<HandlerActionModel> handlerActions = from x in handlerType.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                                                                 let attribute = x.GetCustomAttribute<HandlerActionAttribute>()
                                                                 where attribute != null
                                                                 select new HandlerActionModel(attribute.Action, x, handlerTypeInfo);

                foreach (HandlerActionModel handlerAction in handlerActions)
                {
                    if (!handlerCacheEntries.ContainsKey(handlerAction.ActionType))
                    {
                        handlerCacheEntries.Add(handlerAction.ActionType, handlerAction);
                    }
                }
            }

            return new HandlerActionCache(handlerCacheEntries);
        }
    }
}
