using HandlerInvoker.App.Common;
using HandlerInvoker.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandlerInvoker.App.Handlers
{
    [Handler]
    public sealed class CustomHandler
    {
        public CustomHandler()
        {

        }

        [HandlerAction(HandlerActionType.CreateUser)]
        public void MyFirstHandlerAction()
        {
        }
    }
}
