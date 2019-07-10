using HandlerInvoker.Core.Models;
using System;

namespace HandlerInvoker.Core.Internal
{
    internal interface IHandlerCache
    {
        HandlerModel GetHandler(Type handlerType);
    }
}
