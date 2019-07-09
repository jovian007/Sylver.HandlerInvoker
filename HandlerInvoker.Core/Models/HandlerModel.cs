using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HandlerInvoker.Core.Models
{
    internal sealed class HandlerModel
    {
        public TypeInfo TypeInfo { get; }

        public IList<HandlerActionModel> Actions { get; }

        public HandlerModel(TypeInfo handlerType)
        {
            this.TypeInfo = handlerType;
            this.Actions = new List<HandlerActionModel>();
        }
    }
}
