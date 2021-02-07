using System;
using System.IO;
using System.Runtime.ExceptionServices;
using Wr = global::Wire;

namespace Serialization.Libraries.Binary
{
    public static partial class BinarySerialize
    {
        public static class Wire
        {
            public static byte[] Serialize<T>(T objectToSerialize, bool preserveTypeInfo = false)
            {
                var s = new Wr.Serializer(new Wr.SerializerOptions(preserveObjectReferences: preserveTypeInfo));
                using (var ms = new MemoryStream())
                {
                    s.Serialize(objectToSerialize, ms);
                    return ms.ToArray();
                }
            }

            public static byte[] Serialize(Type type, object objectToSerialize, bool preserveTypeInfo = false, bool compress = true)
            {
                var s = new Wr.Serializer(new Wr.SerializerOptions(preserveObjectReferences: preserveTypeInfo));
                using (var ms = new MemoryStream())
                {
                    s.Serialize(objectToSerialize, ms);
                    return ms.ToArray();
                }
            }

            public static T Deserialize<T>(byte[] data, bool suppressErrors = true)
            {
                try
                {
                    var s = new Wr.Serializer(new Wr.SerializerOptions());
                    using (var ms = new MemoryStream(data))
                    {
                        return s.Deserialize<T>(ms);
                    }
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();
                    return default(T);
                }
            }

            public static object Deserialize(Type type, byte[] data, bool suppressErrors = true)
            {
                try
                {
                    var s = new Wr.Serializer(new Wr.SerializerOptions());
                    using (var ms = new MemoryStream(data))
                    {
                        return s.Deserialize(ms);
                    }
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();
                    return null;
                }
            }
        }
    }
}
