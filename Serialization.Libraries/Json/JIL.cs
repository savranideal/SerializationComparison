using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Serialization.Libraries.Json
{
    public static partial class JsonSerialize
    {
        public static class JIL
        {
            static JIL()
            {
                Jil.JSON.SetDefaultOptions(new Jil.Options(includeInherited:true)
                { 
                    
                });
            }
            public static string Serialize<T>(T data, Jil.Options options = null)
            { 
                return Jil.JSON.Serialize(data, options);

            }
             
            public static T Deserialize<T>(Stream stream, Jil.Options options = null)
            {
               using TextReader text = new StreamReader(stream);
                return Jil.JSON.Deserialize<T>(text, options);
            }
            public static T Deserialize<T>(string data, Jil.Options options = null)
            {
                using (var input = new StringReader(data))
                {
                    return Jil.JSON.Deserialize<T>(input, options);
                } 
            } 
        }
    }
}
