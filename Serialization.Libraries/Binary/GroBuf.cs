using System;
using System.Runtime.ExceptionServices;
using Gro = global::GroBuf;

namespace Serialization.Libraries.Binary
{
    public static partial class BinarySerialize
    {
        public static class GroBuf
        {
            public static byte[] Serialize<T>(T objectToSerialize)
            {
                var s = new Gro.Serializer(new Gro.DataMembersExtracters.PropertiesExtractor(), options: Gro.GroBufOptions.None);
                return s.Serialize(objectToSerialize);
            }

            public static byte[] Serialize(Type type, object objectToSerialize, bool compress = true)
            {
                var s = new Gro.Serializer(new Gro.DataMembersExtracters.PropertiesExtractor(), options: Gro.GroBufOptions.None);
                return s.Serialize(objectToSerialize);
            }

            public static T Deserialize<T>(byte[] data, bool suppressErrors = true)
            {
                try
                {
                    var s = new Gro.Serializer(new Gro.DataMembersExtracters.PropertiesExtractor(), options: Gro.GroBufOptions.None);
                    return s.Deserialize<T>(data);
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();
                    return default;
                }
            }

            public static object Deserialize(Type type, byte[] data, bool suppressErrors = true)
            {
                try
                {
                    var s = new Gro.Serializer(new Gro.DataMembersExtracters.PropertiesExtractor(), options: Gro.GroBufOptions.None);
                    return s.Deserialize(type, data);
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();
                    return "";
                }
            }
        }
    }
}
