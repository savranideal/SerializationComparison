using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Serialization.Libraries.Json
{
    public static partial class JsonSerialize
    {
       public static class ServiceStackText
        {
            
            public static string Serialize<T>(T data)
            { 
                return JsonSerializer.SerializeToString(data);
            }
            public static void Serialize<T>(T data,Stream stream)
            { 
                 JsonSerializer.SerializeToStream(data,stream);
            }
             
            public static T Deserialize<T>(string data)
            {
                return JsonSerializer.DeserializeFromString<T>(data);

            }
        }
    }
}
