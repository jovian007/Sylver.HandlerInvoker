using HandlerInvoker.Core.Attributes;
using HandlerInvoker.Core.Internal;
using HandlerInvoker.Core.Models;
using HandlerInvoker.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HandlerInvoker.Core
{
    public static class HandlerServiceCollectionExtensions
    {
        public static void AddHandlers(this IServiceCollection services)
        {
            services.TryAddSingleton<IHandlerActionCache>(s => HandlerCacheFactory());
            services.TryAddSingleton<HandlerActionInvokerCache>();
            services.TryAddSingleton<IHandlerInvokerService, HandlerInvokerService>();

            services.TryAddSingleton<ITypeActivatorCache, TypeActivatorCache>();
        }

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
