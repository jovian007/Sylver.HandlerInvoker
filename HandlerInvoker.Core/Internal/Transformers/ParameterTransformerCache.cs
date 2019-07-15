using HandlerInvoker.Core.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HandlerInvoker.Core.Internal.Transformers
{
    internal sealed class ParameterTransformerCache
    {
        private readonly IDictionary<TypeInfo, ParameterTransformerCacheEntry> _cache;
        private readonly IDictionary<TypeInfo, TransformerModel> _transformers;

        public ParameterTransformerCache()
        {
            this._cache = new ConcurrentDictionary<TypeInfo, ParameterTransformerCacheEntry>();
            this._transformers = new ConcurrentDictionary<TypeInfo, TransformerModel>();
        }

        public void AddTransformer(TransformerModel transformerModel)
        {
            this._transformers.Add(transformerModel.Destination, transformerModel);
        }

        public ParameterTransformerCacheEntry GetTransformer(TypeInfo type)
        {
            if (!this._cache.TryGetValue(type, out ParameterTransformerCacheEntry transformer))
            {
                TransformerModel transformerModel = this.GetTransformerModel(type);

                if (transformer == null)
                    return null;

                // TODO: create transformer and cache it.
            }

            return transformer;
        }

        private TransformerModel GetTransformerModel(TypeInfo type)
        {
            TransformerModel transformer = null;

            if (!this._transformers.TryGetValue(type, out transformer))
            {
                TypeInfo[] interfaces = type.GetInterfaces().Select(x => x.GetTypeInfo()).ToArray();

                for (int i = 0; i < interfaces.Length; i++)
                {
                    if (this._transformers.TryGetValue(interfaces[i], out transformer))
                    {
                        break;
                    }
                }
            }

            return transformer;
        }
    }

    internal class ParameterTransformerCacheEntry
    {
    }
}
