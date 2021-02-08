using AutoFixture;
using AutoFixture.Kernel;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Serialization.Benchmark.Benchmarks;
using Serialization.Benchmark.Models;
using Serialization.Benchmark.Models.Hotel;
using Serialization.Benchmark.Models.Geography;
using Serialization.Libraries.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Serilization.Benchmark
{
    class Program
    {
        private static IReadOnlyDictionary<string, Action> _benchmarks = new Dictionary<string, Action>
        {
            { "large",()=>BenchmarkRunner.Run<SerializeLargeSize>()},
            { "small",()=>BenchmarkRunner.Run<SerializeSmallSize>()},
            { "list",()=>BenchmarkRunner.Run<SerializeList>()},
            { "dictionary",()=>BenchmarkRunner.Run<SerializeDictionary>()}
        };
        static void Main(string[] args)
        {

            #region Enviroment Settings 
            const string envVariable = "Data";
            if (Environment.GetEnvironmentVariable(envVariable) == null)
            {
                string root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string path = Path.Join(Directory.GetParent(root).Parent.Parent.FullName, envVariable);
                Environment.SetEnvironmentVariable(envVariable, path);
            }
            Environment.SetEnvironmentVariable("R_HOME", @"C:\Program Files\R\R-4.0.3\", EnvironmentVariableTarget.Process);

            #endregion

            if(args!=null && args.Any())
            {
                foreach (var item in args)
                {
                    if (_benchmarks.ContainsKey(item))
                        _benchmarks[item]();
                }
            }

            //CreateSerializedData();

            var b = new SerializeLargeSize();
            b.Setup();
            b.Jil();
            //var k = b.FastJson();
            //var k2 = b.Jil();
            //var k3 = b.ServiceStackText();
            //var k4 = b.Swifter();
            //var k5 = b.SystemTextJson();
            //var k6 = b.UTF8Json();
            //var summary = BenchmarkRunner.Run<DeserializeLarge>();
            // BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
        private static void CreateSerializedData()
        {
            var f = new Fixture() { RepeatCount = 1 };
            f.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString().Substring(0, 2)));
            
            var counts = new int[] { 1, 10, 100, 500, 1000, 10_000,  100_000,  500_000 };
           // var builder = f.Build<HotelChain>().With(x => x.Hotels, new Fixture().CreateMany<Hotel>(1).ToList());
            foreach (var item in counts)
            {
                var listObjects = f.CreateMany<HotelChain>(item);
                var jsonSerializers = typeof(JsonSerialize).GetNestedTypes();
                var path = Path.Combine(Environment.CurrentDirectory, "../../../Data");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var jsonSerializer in jsonSerializers)
                {
                    var methodForList = jsonSerializer.GetMethod("Serialize");
                    methodForList = methodForList.MakeGenericMethod(typeof(IEnumerable<HotelChain>));
                    var listList = new List<object> { listObjects };
                    var c = methodForList.GetParameters().Count();
                    for (int i = 1; i < c; i++)
                    {
                        listList.Add(null);
                    }
                    var listFileName = $"Serialized_{jsonSerializer.Name}_HotelChain_{item}.json";

                    var listJson = methodForList.Invoke(null, listList.ToArray());
                    if (listJson.GetType() == typeof(byte[]))
                    {
                        System.IO.File.WriteAllBytes(Path.Combine(path, listFileName), listJson as byte[]);
                    }
                    else
                    {
                        System.IO.File.WriteAllText(Path.Combine(path, listFileName), listJson.ToString());
                    }
                }
            }


        }




    }
}
