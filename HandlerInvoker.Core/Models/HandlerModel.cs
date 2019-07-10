using System.Collections.Generic;
using System.Reflection;

namespace HandlerInvoker.Core.Models
{
    internal class HandlerModel
    {
        public string Name { get; }

        public TypeInfo TypeInfo { get; }

        public IList<HandlerActionModel> Actions { get; }

        public HandlerModel(TypeInfo handlerType, IEnumerable<HandlerActionModel> actions)
        {
            this.Name = handlerType.Name;
            this.TypeInfo = handlerType;
            this.Actions = new List<HandlerActionModel>(actions);
        }
    }
}
