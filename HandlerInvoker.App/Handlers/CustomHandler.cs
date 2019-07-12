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
        public void MyFirstHandlerAction(int index)
        {
            this._defaultService.Print($"MyFirstHandlerAction({index})");
        }

        [HandlerAction(HandlerActionType.ShowUser)]
        public void MySecondHandlerAction()
        {
            this._defaultService.Print("MySecondHandlerAction()");
        }
    }
}
