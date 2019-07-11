using HandlerInvoker.Core.Models;
using System;

namespace HandlerInvoker.Core.Internal
{
    internal interface IHandlerActionCache
    {
        HandlerActionModel GetHandlerAction(object handlerAction);
    }
}
