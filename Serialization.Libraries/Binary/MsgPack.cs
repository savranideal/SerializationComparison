using MessagePack;
using System;
using System.Buffers;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Serialization.Libraries.Binary
{
    public static partial class BinarySerialize
    {
        /// <summary>
        /// MessagePack olarak serialize ve deserialize işlemlerini gerçeleştirir.
        /// </summary>
        /// <remarks>
        /// Alt planda kullanılan MessagePack-CSharp paketi şimdilik circular-referance  desteklemiyor.
        /// </remarks>
        public static class MsgPack
        {
            static MsgPack()
            {
                //haack
                var sqType = typeof(MessagePackSerializer).Assembly.GetType("MessagePack.SequencePool");
                var sq = sqType.GetField("Shared", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
                var poolField = sqType.GetField("arrayPool", BindingFlags.NonPublic | BindingFlags.Instance);
                poolField.SetValue(sq, ArrayPool<byte>.Create(80 * 1024, 1000));
            }

            private static readonly IFormatterResolver _standardResolvers = MessagePack.Resolvers.CompositeResolver.Create(
                MessagePack.Resolvers.NativeGuidResolver.Instance,
                MessagePack.Resolvers.NativeDecimalResolver.Instance,
                MessagePack.Resolvers.NativeDateTimeResolver.Instance,
                MessagePack.Resolvers.StandardResolver.Instance
                );
            private static readonly IFormatterResolver _contractlessResolvers = MessagePack.Resolvers.CompositeResolver.Create(
                MessagePack.Resolvers.NativeGuidResolver.Instance,
                MessagePack.Resolvers.NativeDecimalResolver.Instance,
                MessagePack.Resolvers.NativeDateTimeResolver.Instance,
                MessagePack.Resolvers.TypelessObjectResolver.Instance,
                MessagePack.Resolvers.ContractlessStandardResolver.Instance
                );
            private static readonly MessagePackSerializerOptions _standard = MessagePackSerializerOptions.Standard
                .WithResolver(_standardResolvers);
            private static readonly MessagePackSerializerOptions _standardCompressed = MessagePackSerializerOptions.Standard
                .WithResolver(_standardResolvers).WithCompression(MessagePackCompression.Lz4BlockArray);
            private static readonly MessagePackSerializerOptions _contractless = MessagePackSerializerOptions.Standard
                .WithResolver(_contractlessResolvers);
            private static readonly MessagePackSerializerOptions _contractlessCompressed = MessagePackSerializerOptions.Standard
                .WithResolver(_contractlessResolvers).WithCompression(MessagePackCompression.Lz4BlockArray);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static MessagePackSerializerOptions GetSerializerOptions(bool compress, bool contractless)
            {
                return compress ?
                        contractless ? _contractlessCompressed : _standardCompressed :
                        contractless ? _contractless : _standard;
            }

            /// <summary>
            /// Nesneyi MessagePack olarak serialize eder.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="objectToSerialize"></param>
            /// <param name="compress">Sıkıştırılsın mı?</param>
            /// <returns></returns>
            public static byte[] Serialize<T>(T objectToSerialize, bool compress = true, bool contractless = true)
            {
                return MessagePackSerializer.Serialize(objectToSerialize, GetSerializerOptions(compress, contractless));
            }

            public static byte[] Serialize(Type type, object objectToSerialize, bool compress = true, bool contractless = true)
            {
                return MessagePackSerializer.Serialize(type, objectToSerialize, GetSerializerOptions(compress, contractless));
            }

            public static void Serialize<T>(T objectToSerialize, Stream output, bool compress = true, bool contractless = true)
            {
                MessagePackSerializer.Serialize(output, objectToSerialize, GetSerializerOptions(compress, contractless));
            }

            public static void Serialize(Type type, object objectToSerialize, Stream output, bool compress = true, bool contractless = true)
            {
                MessagePackSerializer.Serialize(type, output, objectToSerialize, GetSerializerOptions(compress, contractless));
            }

            /// <summary>
            /// MessagePack datasını objeye dönüştürür.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="data"></param>
            /// <returns></returns>
            public static T Deserialize<T>(byte[] data, bool suppressErrors = true, bool compress = true, bool contractless = true)
            {
                try
                {
                    return MessagePackSerializer.Deserialize<T>(data, GetSerializerOptions(compress, contractless));
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();
                    return default;
                }
            }

            public static object Deserialize(Type type, byte[] data, bool suppressErrors = true, bool compress = true, bool contractless = true)
            {
                try
                {
                    return MessagePackSerializer.Deserialize(type, data, GetSerializerOptions(compress, contractless));
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();
                    return null;
                }
            }

            public static T Deserialize<T>(Stream input, bool suppressErrors = true, bool compress = true, bool contractless = true)
            {
                try
                {
                    return MessagePackSerializer.Deserialize<T>(input, GetSerializerOptions(compress, contractless));
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();
                    return default(T);
                }
            }

            public static T Deserialize<T>(ReadOnlyMemory<byte> input, bool suppressErrors = true, bool compress = true, bool contractless = true)
            {
                try
                {
                    return MessagePackSerializer.Deserialize<T>(input, GetSerializerOptions(compress, contractless));
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();
                    return default(T);
                }
            }

            public static object Deserialize(Type type, Stream input, bool suppressErrors = true, bool compress = true, bool contractless = true)
            {
                try
                {
                    return MessagePackSerializer.Deserialize(type, input, GetSerializerOptions(compress, contractless));
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
