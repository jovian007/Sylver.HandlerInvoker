using System;

namespace HandlerInvoker.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class HandlerActionAttribute : Attribute
    {
        public object Action { get; }

        public HandlerActionAttribute(object action)
        {
            this.Action = action;
        }
    }
}
