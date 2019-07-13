using HandlerInvoker.App.Common;
using HandlerInvoker.App.Services;
using HandlerInvoker.Core.Attributes;

namespace HandlerInvoker.App.Handlers
{
    [Handler]
    public sealed class CustomHandler
    {
        private readonly IDefaultService _defaultService;

        public CustomHandler(IDefaultService defaultService)
        {
            this._defaultService = defaultService;
        }

        [HandlerAction(HandlerActionType.CreateUser)]
        public void MyFirstHandlerAction(int index, TestObject test)
        {
            var isObjectNull = test == null ? "null" : "TestObject()";

            this._defaultService.Print($"MyFirstHandlerAction({index}, {isObjectNull})");
        }

        [HandlerAction(HandlerActionType.ShowUser)]
        public void MySecondHandlerAction()
        {
            this._defaultService.Print("MySecondHandlerAction()");
        }
    }

    public class TestObject
    {
        public int Test { get; set; }
    }
}
