using System;
using System.Reflection;

namespace HandlerInvoker.Core.Internal.Transformers
{
    internal interface IParameterTransformer
    {
        object Transform(object originalParameter, TypeInfo destinationParameterType);
    }

    internal class ParameterTransformer : IParameterTransformer
    {
        private readonly ParameterTransformerCache _transformerCache;

        public ParameterTransformer(ParameterTransformerCache transformerCache)
        {
            this._transformerCache = transformerCache;
        }

        public object Transform(object originalParameter, TypeInfo destinationParameterType)
        {
            ParameterTransformerCacheEntry transformer = this._transformerCache.GetTransformer(destinationParameterType);

            // transform parameter

            return null;
        }
    }
}
