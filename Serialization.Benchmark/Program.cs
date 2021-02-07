using AutoFixture;
using AutoFixture.Kernel;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Serialization.Benchmark.Benchmarks;
using Serialization.Benchmark.Models;
using Serialization.Benchmark.Models.Geography;
using Serialization.Libraries.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Serilization.Benchmark
{
    class Program
    {


        static void Main(string[] args)
        {

            //CreateDeserializeData();
            Environment.SetEnvironmentVariable("R_HOME", @"C:\Program Files\R\R-4.0.3\", EnvironmentVariableTarget.Process);
             var summary = BenchmarkRunner.Run<SerializeDictionary>(); 
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
        private static void CreateDeserializeData()
        {
            var f = new Fixture();
            f.Register(() => new City() { Id = f.Create<int>(), Label = f.Create<string>(), });
            f.Register(() => new Coordinate() { Latitude = f.Create<float>(), Longitude = f.Create<float>(), });
            f.Register(() => new Airport() { Id = f.Create<int>(), Label = f.Create<string>(), });
            f.Register(() => new Country() { Id = f.Create<int>(), Label = f.Create<string>(), });
            var largeOBject = f.Create<HotelInformation>();
            var largeCollectionOBject = f.CreateMany<HotelInformation>(100);

            var jsonSerializers = typeof(JsonSerialize).GetNestedTypes();
            var path = Path.Combine(Environment.CurrentDirectory, "../../../Data");
            if (!Directory.Exists(path)){
                Directory.CreateDirectory(path);
            } 

            foreach (var jsonSerializer in jsonSerializers)
            {
                var method = jsonSerializer.GetMethod("Serialize");
                var method2 = jsonSerializer.GetMethod("Serialize");
                method = method.MakeGenericMethod(typeof(HotelInformation));
                method2 = method2.MakeGenericMethod(typeof(IEnumerable<HotelInformation>));
                var list = new List<object> { largeOBject };
                var list2 = new List<object> { largeCollectionOBject };
                var c = method.GetParameters().Count();
                for (int i = 1; i < c; i++)
                {
                    list.Add(null);
                    list2.Add(null);
                }
                var fileName = $"{jsonSerializer.Name}_Large.json";  
                var cfileName = $"{jsonSerializer.Name}_Collection_Large.json";  
                var serializedJson = method.Invoke(null, list.ToArray());
                var collectionJson = method2.Invoke(null, list2.ToArray());


                if (serializedJson.GetType() == typeof(byte[]))
                {
                    System.IO.File.WriteAllBytes(Path.Combine(path, fileName), serializedJson as byte[]);
                    System.IO.File.WriteAllBytes(Path.Combine(path, cfileName), collectionJson as byte[]);
                }
                else
                {
                    System.IO.File.WriteAllText(Path.Combine(path, fileName), serializedJson.ToString());
                    System.IO.File.WriteAllText(Path.Combine(path, cfileName), collectionJson.ToString());
                }
            }


        }




    }
}
