
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using  Json = Serialization.Libraries.Json.JsonSerialize.Newtonsoft;

namespace Serialization.Libraries.Binary
{
    public static partial class BinarySerialize
    {
        public static class Bson
        {
            public static byte[] Serialize<T>(T objectToSerialize,
                bool preserveTypeInfo = false,
                List<JsonConverter> converters = null,
                bool indent = false,
                Json.JsonSerialize.Newtonsoft.ReferenceLoop referenceLoopHandling = Json.JsonSerialize.Newtonsoft.ReferenceLoop.Ignore)
            {
                using (var ms = new MemoryStream())
                using (var writer = new BsonDataWriter(ms))
                {
                    var serializer = JsonSerializer.Create(Json.JsonSerialize.Newtonsoft.PrepareSettings(preserveTypeInfo, converters, indent, referenceLoopHandling));
                    serializer.Serialize(writer, objectToSerialize);
                    return ms.ToArray();
                }
            }

            public static byte[] Serialize(Type type, object objectToSerialize,
                bool preserveTypeInfo = false,
                List<JsonConverter> converters = null,
                bool indent = false,
                Json.JsonSerialize.Newtonsoft.ReferenceLoop referenceLoopHandling = Json.JsonSerialize.Newtonsoft.ReferenceLoop.Ignore)
            {
                using (var ms = new MemoryStream())
                using (var writer = new BsonDataWriter(ms))
                {
                    var serializer = JsonSerializer.Create(Json.JsonSerialize.Newtonsoft.PrepareSettings(preserveTypeInfo, converters, indent, referenceLoopHandling));
                    serializer.Serialize(writer, objectToSerialize);
                    return ms.ToArray();
                }
            }

            public static T Deserialize<T>(byte[] data, bool suppressErrors = true, bool preserveTypeInfo = false,
            List<JsonConverter> converters = null)
            {
                try
                {
                    using (var ms = new MemoryStream(data))
                    using (var reader = new BsonDataReader(ms))
                    {
                        var serializer = JsonSerializer.Create(Json.JsonSerialize.Newtonsoft.PrepareSettings(preserveTypeInfo, converters));
                        return serializer.Deserialize<T>(reader);
                    }
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();
                    return default;
                }
            }

            public static object Deserialize(Type type, byte[] data, bool suppressErrors = true, bool preserveTypeInfo = false,
            List<JsonConverter> converters = null)
            {
                try
                {
                    using (var ms = new MemoryStream(data))
                    using (var reader = new BsonDataReader(ms))
                    {
                        var serializer = JsonSerializer.Create(Json.JsonSerialize.Newtonsoft.PrepareSettings(preserveTypeInfo, converters));
                        return serializer.Deserialize(reader, type);
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
