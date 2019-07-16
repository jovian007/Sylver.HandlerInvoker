using System;
using System.Reflection;

namespace HandlerInvoker.Core.Internal.Transformers
{
    public delegate object TransformerMethod(object source, object destination);

    internal class ParameterTransformerCacheEntry
    {
        public TypeInfo Source { get; }

        public TypeInfo Destination { get; }

        public TypeInfo Target { get; }

        public Func<TypeInfo, object> ParameterFactory { get; }

        public TransformerMethod Transformer { get; }

        public ParameterTransformerCacheEntry(TypeInfo source, TypeInfo destination, TypeInfo target, Func<TypeInfo, object> parameterFactory, TransformerMethod transformer)
        {
            this.Source = source;
            this.Destination = destination;
            this.Target = target;
            this.ParameterFactory = parameterFactory;
            this.Transformer = transformer;
        }
    }
}
