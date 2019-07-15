using System;
using System.Reflection;

namespace HandlerInvoker.Core.Models
{
    internal class TransformerModel
    {
        public TypeInfo Source { get; }

        public TypeInfo Destination { get; }

        public Func<object, object, object> Trasnformer { get; }

        public TransformerModel(TypeInfo source, TypeInfo destination, Func<object, object, object> transformer)
        {
            this.Source = source;
            this.Destination = destination;
            this.Trasnformer = transformer;
        }
    }
}
