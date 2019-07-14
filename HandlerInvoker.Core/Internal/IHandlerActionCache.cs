using HandlerInvoker.Core.Models;
using System;

namespace HandlerInvoker.Core.Internal
{
    internal interface IHandlerActionCache
    {
        /// <summary>
        /// Gets the handler action model associated to the handler action type.
        /// </summary>
        /// <param name="handlerAction">Handler action type.</param>
        /// <returns><see cref="HandlerActionModel"/></returns>
        HandlerActionModel GetHandlerAction(object handlerAction);
    }
}
