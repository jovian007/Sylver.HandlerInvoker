using System;
using System.Collections.Generic;
using System.Text;

namespace HandlerInvoker.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class HandlerAttribute : Attribute
    {
    }
}
