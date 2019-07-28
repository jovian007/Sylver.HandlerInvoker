using System;

namespace Sylver.HandlerInvoker.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class HandlerAttribute : Attribute
    {
    }
}
