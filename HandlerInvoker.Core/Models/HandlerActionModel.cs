using System.Reflection;

namespace HandlerInvoker.Core.Models
{
    internal sealed class HandlerActionModel
    {
        public string Name { get; }

        public MethodInfo Method { get; }

        public HandlerActionModel(MethodInfo methodInfo)
        {
            this.Name = methodInfo.Name;
            this.Method = methodInfo;
        }
    }
}
