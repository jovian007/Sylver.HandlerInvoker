using HandlerInvoker.App.Common;
using HandlerInvoker.Core.Attributes;
using System;

namespace HandlerInvoker.App.Handlers
{
    [Handler]
    public sealed class CustomHandler
    {
        public int Test { get; }

        public CustomHandler()
        {
        }

        [HandlerAction(HandlerActionType.CreateUser)]
        public void MyFirstHandlerAction(int index)
        {
            Console.WriteLine($"MyFirstHandlerAction({index})");
        }

        [HandlerAction(HandlerActionType.ShowUser)]
        public void MySecondHandlerAction()
        {
            Console.WriteLine("MySecondHandlerAction()");
        }
    }
}
