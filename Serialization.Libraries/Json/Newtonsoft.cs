using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;



namespace Serialization.Libraries.Json
{
    public static partial class JsonSerialize
    {
        public static  class Newtonsoft
        { 
            public static string Serialize<T>(T objectToSerialize,
                bool preserveTypeInfo = false,
                List<JsonConverter> converters = null,
                bool indent = false,
                ReferenceLoop referenceLoopHandling = ReferenceLoop.Ignore)
            {
                return JsonConvert.SerializeObject(objectToSerialize, PrepareSettings(preserveTypeInfo, converters, indent, referenceLoopHandling));
            }
            
            public static void Serialize<T>(T objectToSerialize,
                Stream stream,
                bool preserveTypeInfo = false,
                List<JsonConverter> converters = null,
                bool indent = false,
                ReferenceLoop referenceLoopHandling = ReferenceLoop.Ignore)
            {
                var serializer = JsonSerializer.CreateDefault(PrepareSettings(preserveTypeInfo, converters, indent, referenceLoopHandling));
                using (var writer = new StreamWriter(stream))
                {
                    serializer.Serialize(writer, objectToSerialize);
                }
            }
             

            public static void SerializeCompressed<T>(T objectToSerialize, Stream stream,
                bool preserveTypeInfo = false,
                List<JsonConverter> converters = null,
                bool indent = false,
                ReferenceLoop referenceLoopHandling = ReferenceLoop.Ignore)
            {
                using (var compressor = new GZipStream(stream, CompressionMode.Compress, true))
                using (var writer = new StreamWriter(compressor))
                {
                    var serializer = JsonSerializer.CreateDefault(PrepareSettings(preserveTypeInfo, converters, indent, referenceLoopHandling));
                    serializer.Serialize(writer, objectToSerialize);
                }
            }
             
            public static T Deserialize<T>(string json,
                bool suppressErrors = true,
                bool preserveTypeInfo = false,
                List<JsonConverter> converters = null)
            {
                var settings = PrepareSettings(preserveTypeInfo, converters, false);

                try
                {
                    return JsonConvert.DeserializeObject<T>(json, settings);
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();
                    return default(T);
                }
            }
             
            public static T Deserialize<T>(string json, JsonSerializerSettings settings)
            {
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
             
            public static object Deserialize(string json, Type type,
                bool suppressErrors = true,
                bool preserveTypeInfo = false,
                List<JsonConverter> converters = null)
            {
                var settings = PrepareSettings(preserveTypeInfo, converters, false);
                try
                {
                    return JsonConvert.DeserializeObject(json, type, settings);
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();

                    if (type.GetTypeInfo().IsValueType)
                        return Activator.CreateInstance(type);

                    return null;
                }
            }

            public static T Deserialize<T>(Stream stream, JsonSerializerSettings settings = null)
            {
                var jsonSerializer = new JsonSerializer();
                using (stream)
                using (var sr = new StreamReader(stream))
                using (var jsonTextReader = new JsonTextReader(sr))
                {
                    return jsonSerializer.Deserialize<T>(jsonTextReader);
                }
            }

            public static T DeserializeCompressed<T>(Stream stream, bool suppressErrors = true,
                bool preserveTypeInfo = false,
                List<JsonConverter> converters = null)
            {
                var settings = PrepareSettings(preserveTypeInfo, converters, false);

                try
                {
                    using (var compressor = new GZipStream(stream, CompressionMode.Decompress, true))
                    using (var reader = new StreamReader(compressor))
                    using (var jsonReader = new JsonTextReader(reader))
                    {
                        var serializer = JsonSerializer.CreateDefault(settings);
                        return serializer.Deserialize<T>(jsonReader);
                    }
                }
                catch (Exception e)
                {
                    if (!suppressErrors)
                        ExceptionDispatchInfo.Capture(e).Throw();
                    return default;
                }
            }

            public enum ReferenceLoop
            {
                Error = ReferenceLoopHandling.Error,
                Ignore = ReferenceLoopHandling.Ignore,
                Serialize = ReferenceLoopHandling.Serialize
            }

            internal static JsonSerializerSettings PrepareSettings(bool preserveTypeInfo, List<JsonConverter> converters, bool indent = false,
                ReferenceLoop referenceLoopHandling = ReferenceLoop.Ignore)
            {
                var settings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    Converters = converters ?? new List<JsonConverter>(),
                    Formatting = indent ? Formatting.Indented : Formatting.None,
                    ReferenceLoopHandling = (ReferenceLoopHandling)(int)referenceLoopHandling,
                };

                if (preserveTypeInfo)
                {
                    settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                    settings.TypeNameHandling = TypeNameHandling.Auto;
                    //bunun ile $type etiketi eklenip polimorfik objelere izin veriliyor.
                    settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
                    // bu olmazsa ctor'daki default değerlere ekleme yapar.
                    settings.ContractResolver = new NoConstructorCreationContractResolver();
                    settings.SerializationBinder = new NetCoreSerializationBinder();
                }
                return settings;
            }

            public class NetCoreSerializationBinder : DefaultSerializationBinder
            {
                private static readonly Regex regex = new Regex(
                    @"System\.Private\.CoreLib(, Version=[\d\.]+)?(, Culture=[\w-]+)(, PublicKeyToken=[\w\d]+)?");

                private static readonly ConcurrentDictionary<Type, (string assembly, string type)> cache =
                    new ConcurrentDictionary<Type, (string, string)>();

                public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
                {
                    base.BindToName(serializedType, out assemblyName, out typeName);

                    if (cache.TryGetValue(serializedType, out var name))
                    {
                        assemblyName = name.assembly;
                        typeName = name.type;
                    }
                    else
                    {
                        if (assemblyName.AsSpan().Contains("System.Private.CoreLib".AsSpan(), StringComparison.OrdinalIgnoreCase))
                            assemblyName = regex.Replace(assemblyName, "mscorlib");

                        if (typeName.AsSpan().Contains("System.Private.CoreLib".AsSpan(), StringComparison.OrdinalIgnoreCase))
                            typeName = regex.Replace(typeName, "mscorlib");

                        cache.TryAdd(serializedType, (assemblyName, typeName));
                    }
                }
            }
        }

       
    } public class NoConstructorCreationContractResolver : DefaultContractResolver
        {

            /// <summary>
            /// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonObjectContract"/> for the given type.
            /// </summary>
            /// <param name="objectType">Type of the object.</param>
            /// <returns>
            /// A <see cref="T:Newtonsoft.Json.Serialization.JsonObjectContract"/> for the given type.
            /// </returns>
            protected override JsonObjectContract CreateObjectContract(Type objectType)
            {
                // prepare contract using default resolver
                var objectContract = base.CreateObjectContract(objectType);
                var typeInfo = objectContract.CreatedType.GetTypeInfo();
                // if type has constructor marked with JsonConstructor attribute or can't be instantiated, return default contract
                if (GetAttributeConstructor(objectType) != null || typeInfo.IsInterface || typeInfo.IsAbstract)
                    return objectContract;

                // prepare function to check that specified constructor parameter corresponds to non writable property on a type
                Func<JsonProperty, bool> isParameterForNonWritableProperty =
                    parameter =>
                    {
                        var propertyForParameter = objectContract.Properties.FirstOrDefault(property => property.PropertyName == parameter.PropertyName);

                        if (propertyForParameter == null)
                            return false;

                        return !propertyForParameter.Writable;
                    };

                // if type has parameterized constructor and any of constructor parameters corresponds to non writable property, return default contract
                // this is needed to handle special cases for types that can be initialized only via constructor, i.e. Tuple<>
                if (GetParameterizedConstructor(objectType) != null
                    && objectContract.CreatorParameters.Any(parameter => isParameterForNonWritableProperty(parameter)))
                    return objectContract;

                // override default creation method to create object without constructor call
                objectContract.DefaultCreatorNonPublic = false;


                objectContract.DefaultCreator = () => FormatterServices.GetSafeUninitializedObject(objectContract.CreatedType);
                return objectContract;
            }

            private ConstructorInfo GetAttributeConstructor(Type objectType)
            {
                IEnumerator<ConstructorInfo> en = objectType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(c => c.IsDefined(typeof(JsonConstructorAttribute), true)).GetEnumerator();

                if (en.MoveNext())
                {
                    ConstructorInfo conInfo = en.Current;
                    if (en.MoveNext())
                    {
                        throw new JsonException("Multiple constructors with the JsonConstructorAttribute.");
                    }

                    return conInfo;
                }

                // little hack to get Version objects to deserialize correctly
                if (objectType == typeof(Version))
                {
                    return objectType.GetConstructor(new[] { typeof(int), typeof(int), typeof(int), typeof(int) });
                }

                return null;
            }

            private ConstructorInfo GetParameterizedConstructor(Type objectType)
            {
                ConstructorInfo[] constructors = objectType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
                if (constructors.Length == 1)
                {
                    return constructors[0];
                }
                return null;
            }
        }
}
