using HandlerInvoker.Core.Internal.Transformers;
using System.Reflection;

namespace HandlerInvoker.Core.Models
{
    internal class TransformerModel
    {
        public TypeInfo Source { get; }

        public TypeInfo Destination { get; }

        public TransformerMethod Transformer { get; }

        public TransformerModel(TypeInfo source, TypeInfo destination, TransformerMethod transformer)
        {
            this.Source = source;
            this.Destination = destination;
            this.Transformer = transformer;
        }
    }
}
