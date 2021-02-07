using Ceras;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace Serialization.Libraries.Binary
{
    public static partial class BinarySerialize
    {
        public static class Ceras
        {
            private static readonly ConcurrentDictionary<string, Lazy<ObjectPool<CerasSerializer>>> _serializerPools = new ConcurrentDictionary<string, Lazy<ObjectPool<CerasSerializer>>>();
            private static readonly DefaultObjectPoolProvider _objectPoolProvider = new DefaultObjectPoolProvider();

            private class CerasPoolPolicy : IPooledObjectPolicy<CerasSerializer>
            {
                private readonly bool _preserveTypeInfo;
                private readonly bool _compatibleWithDataContractSerializer;
                private readonly Type[] _knownTypes;

                public CerasPoolPolicy(bool preserveTypeInfo, bool compatibleWithDataContractSerializer, Type[] knownTypes)
                {
                    _preserveTypeInfo = preserveTypeInfo;
                    _compatibleWithDataContractSerializer = compatibleWithDataContractSerializer;
                    _knownTypes = knownTypes;
                }

                public CerasSerializer Create()
                {
                    var config = new SerializerConfig
                    {
                        PreserveReferences = _preserveTypeInfo
                    };

                    if (_compatibleWithDataContractSerializer)
                    {
                        config.OnConfigNewType = t => t.TryConfigureLikeDataContractSerializer();
                    }

                    config.KnownTypes.AddRange(_knownTypes);
                    return new CerasSerializer(config);
                }

                public bool Return(CerasSerializer obj)
                {
                    return obj != null;
                }
            }

            static Ceras()
            {
                CerasBufferPool.Pool = new CerasDefaultBufferPool();
            }

            private static string GetConfigKey(bool preserveTypeInfo, bool compatibleWithDataContractSerializer, Type[] knownTypes)
            {
                return $"{preserveTypeInfo}-{compatibleWithDataContractSerializer}-{string.Join(",", knownTypes.Select(t => t.FullName))}";
            }

            private static ObjectPool<CerasSerializer> GetPool(bool preserveTypeInfo, bool compatibleWithDataContractSerializer, Type[] knownTypes)
            {
                return _serializerPools.GetOrAdd(GetConfigKey(preserveTypeInfo, compatibleWithDataContractSerializer, knownTypes), _ => new Lazy<ObjectPool<CerasSerializer>>(() =>
                    _objectPoolProvider.Create(new CerasPoolPolicy(preserveTypeInfo, compatibleWithDataContractSerializer, knownTypes)))).Value;
            }

            public static byte[] Serialize<T>(T objectToSerialize, bool preserveTypeInfo = true, bool compatibleWithDataContractSerializer = true, params Type[] knownTypes)
            {
                var serializerPool = GetPool(preserveTypeInfo, compatibleWithDataContractSerializer, knownTypes);

                CerasSerializer serializer = null;

                try
                {
                    serializer = serializerPool.Get();
                    return serializer.Serialize(objectToSerialize);
                }
                finally
                {
                    serializerPool.Return(serializer);
                }
            }            

            public static T Deserialize<T>(ReadOnlySpan<byte> data, bool suppressErrors = true, bool preserveTypeInfo = true, bool compatibleWithDataContractSerializer = true, params Type[] knownTypes)
            {
                var serializerPool = GetPool(preserveTypeInfo, compatibleWithDataContractSerializer, knownTypes);

                CerasSerializer serializer = null;

                try
                {
                    serializer = serializerPool.Get();
                    return serializer.Deserialize<T>(data.ToArray());
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                    {
                        e.Data["SerializerDebugReport"] = serializer?.GenerateSerializationDebugReport(typeof(T));
                        ExceptionDispatchInfo.Capture(e).Throw();
                    }
                    return default;
                }
                finally
                {
                    serializerPool.Return(serializer);
                }
            }
        }
    }
}