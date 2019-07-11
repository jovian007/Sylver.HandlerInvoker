using System;
using System.Collections.Generic;
using System.Text;

namespace HandlerInvoker.Core.Handlers
{
    public interface IHandlerFactory
    {
        object CreateHandler(Type handlerType);

        object ReleaseHandler(Type handlerType);
    }

    internal sealed class HandlerFactory : IHandlerFactory
    {
        public HandlerFactory()
        {

        }

        public object CreateHandler(Type handlerType)
        {
            throw new NotImplementedException();
        }

        public object ReleaseHandler(Type handlerType)
        {
            throw new NotImplementedException();
        }
    }
}
