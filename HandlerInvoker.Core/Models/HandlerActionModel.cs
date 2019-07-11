using System.Reflection;

namespace HandlerInvoker.Core.Models
{
    internal class HandlerActionModel
    {
        public object ActionType { get; }

        public string Name { get; }

        public MethodInfo Method { get; }

        public TypeInfo HandlerTypeInfo { get; }

        public HandlerActionModel(object handlerActionType, MethodInfo methodInfo, TypeInfo handlerTypeInfo)
        {
            this.ActionType = handlerActionType;
            this.Name = methodInfo.Name;
            this.Method = methodInfo;
            this.HandlerTypeInfo = handlerTypeInfo;
        }

        /// <inheritdoc />
        public override string ToString() => $"{this.HandlerTypeInfo.Name}.{this.Name}()";
    }
}
