using System.Reflection;

namespace HandlerInvoker.Core.Models
{
    internal class HandlerActionModel
    {
        public object ActionType { get; }

        public string Name { get; }

        public MethodInfo Method { get; }

        public HandlerActionModel(object handlerActionType, MethodInfo methodInfo)
        {
            this.ActionType = handlerActionType;
            this.Name = methodInfo.Name;
            this.Method = methodInfo;
        }
    }
}
