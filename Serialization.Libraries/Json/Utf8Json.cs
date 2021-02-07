using System.IO;
using System.Text;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Serialization.Libraries.Json
{
    public static partial class JsonSerialize
    {
        /// <summary>
        /// Polimofizm, preserveTypeInfo ve standart json formatterları kullanmıyorsanız çok daha hızlı json işlemleri için bunu kullanabilirsiniz. 
        /// </summary>
        public static class Utf8Json
        {
            private static readonly IJsonFormatterResolver _allowPrivateExcludeNull = CompositeResolver.Create(new[]{
                EnumResolver.Default, StandardResolver.AllowPrivateExcludeNull});
            private static readonly IJsonFormatterResolver _allowPrivate = CompositeResolver.Create(new[]{
                EnumResolver.Default, StandardResolver.AllowPrivate});
            private static readonly IJsonFormatterResolver _excludeNull = CompositeResolver.Create(new[]{
                EnumResolver.Default, StandardResolver.ExcludeNull});
            private static readonly IJsonFormatterResolver _default = CompositeResolver.Create(new[]{
                EnumResolver.Default, StandardResolver.Default});

            private static IJsonFormatterResolver GetResolver(bool allowPrivate, bool excludeNull)
            {
                if (allowPrivate && excludeNull)
                    return _allowPrivateExcludeNull;
                else if (allowPrivate && !excludeNull)
                    return _allowPrivate;
                else if (!allowPrivate && excludeNull)
                    return _excludeNull;
                else
                    return _default;
            }

            public static byte[] Serialize<T>(T objectToSerialize, bool allowPrivate = false, bool excludeNull = true)
            {
                return JsonSerializer.Serialize(objectToSerialize, GetResolver(allowPrivate, excludeNull));
            }

            //public static void Serialize<T>(T objectToSerialize, Stream stream, bool allowPrivate = false, bool excludeNull = true)
            //{
            //    JsonSerializer.Serialize(stream, objectToSerialize, GetResolver(allowPrivate, excludeNull));
            //}

            public static T Deserialize<T>(byte[] jsonByteArray, bool allowPrivate = false, bool excludeNull = true)
            {
                return jsonByteArray?.Length > 0 ? JsonSerializer.Deserialize<T>(jsonByteArray, GetResolver(allowPrivate, excludeNull)) : default;
            }

            public static T Deserialize<T>(string jsonStr, bool allowPrivate = false, bool excludeNull = true)
            {
                return jsonStr?.Length > 0 ? JsonSerializer.Deserialize<T>(jsonStr, GetResolver(allowPrivate, excludeNull)) : default;
            }

            public static T Deserialize<T>(Stream stream, bool allowPrivate = false, bool excludeNull = true)
            {
                return stream.Length > 0 ? JsonSerializer.Deserialize<T>(stream, GetResolver(allowPrivate, excludeNull)) : default;
            }
        }
    }
}
