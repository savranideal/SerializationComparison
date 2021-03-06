﻿using AutoFixture;
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
using BenchmarkDotNet.Columns;
using System.Runtime.Serialization;

namespace Serilization.Benchmark
{
    

    class Program
    {
        private const string _serializedString = "String";
        private const string _serializedStream = "Stream"; 
        private const string _objectType = "Input Type";
        private const string _processType = "Output";
        static void Main(string[] args)
        {
            //var t2 = new Test2() { A1 = "a1text" };
            //var t = new Test() { B1 = "b1text", Test2 = t2 };
            //t2.Test = t;
            //var l=JsonSerialize.ServiceStackText.Serialize(t);
            //var m = l;

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

            //CreateSerializedData();
            //BenchmarkRunner.Run<DeserializeJson>();
            //// primitive types
            //BenchmarkRunner.Run<SerializeToString<string>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => typeof(string).Name))
            //    .AddColumn(new TagColumn(_processType, c => _serializedString))
            //    );
            //// primitive types
            //BenchmarkRunner.Run<SerializeToString<double>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => typeof(double).Name))
            //    .AddColumn(new TagColumn(_processType, c => _serializedString))
            //    );


            //BenchmarkRunner.Run<SerializeToString<SmallHotelChain>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => typeof(SmallHotelChain).Name))
            //    .AddColumn(new TagColumn(_processType, c => _serializedString))
            //    );

            //BenchmarkRunner.Run<SerializeToString<LargeHotelChain>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => typeof(LargeHotelChain).Name))
            //    .AddColumn(new TagColumn(_processType, c => _serializedString))
            //    );




            ///// dictionary with primitive tpyes
            //BenchmarkRunner.Run<SerializeCollectionToString<DictionarySerializeModel<string,string>>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => "Dictionary"))
            //    .AddColumn(new TagColumn(_processType, c => _serializedString))
            //    );

            /* Dictionary with small object  */
            //BenchmarkRunner.Run<SerializeCollectionToString<DictionarySerializeModel<string, SmallHotelChain>>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => "Dictionary"))
            //    .AddColumn(new TagColumn(_processType, c => _serializedString))
            //    );

            ///* Dictionary with large object  */
            //BenchmarkRunner.Run<SerializeCollectionToString<DictionarySerializeModel<string, LargeHotelChain>>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => "Dictionary"))
            //    .AddColumn(new TagColumn(_processType, c => _serializedString))
            //    );



            ///// List with primitive tpyes
            //BenchmarkRunner.Run<SerializeCollectionToString<EnumerableSerializeModel<string>>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => "List"))
            //    .AddColumn(new TagColumn(_processType, c => _serializedString))
            //    );

            ///* List with small object  */
            //BenchmarkRunner.Run<SerializeCollectionToString<EnumerableSerializeModel<SmallHotelChain>>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => "List"))
            //    .AddColumn(new TagColumn(_processType, c => _serializedString))
            //    );

            ///* List with large object  */
            BenchmarkRunner.Run<SerializeCollectionToString<EnumerableSerializeModel<LargeHotelChain>>>(ManualConfig.Create(DefaultConfig.Instance)
                .AddColumn(new TagColumn(_objectType, c => "List"))
                .AddColumn(new TagColumn(_processType, c => _serializedString))
                );

            #region Serialize To Stream
            ///* Stream with primitive type  */
            //BenchmarkRunner.Run<SerializeToStream<string>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => "string"))
            //    .AddColumn(new TagColumn(_processType, c => _serializedStream))
            //    );

            ///* Stream with small object  */
            //BenchmarkRunner.Run<SerializeToStream<SmallHotelChain>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => typeof(SmallHotelChain).Name))
            //    .AddColumn(new TagColumn(_processType, c => _serializedStream))
            //    );

            ///* Stream with large object  */
            //BenchmarkRunner.Run<SerializeToStream<LargeHotelChain>>(ManualConfig.Create(DefaultConfig.Instance)
            //    .AddColumn(new TagColumn(_objectType, c => typeof(LargeHotelChain).Name))
            //    .AddColumn(new TagColumn(_processType, c => _serializedStream))
            //    );

            #endregion




            // BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
        private static void CreateSerializedData()
        {
            var f = new Fixture() { RepeatCount = 1 };
            f.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString().Substring(0, 2)));

            var counts = new int[] { 1, 10, 100, 1000, 10_000 };
            // var builder = f.Build<HotelChain>().With(x => x.Hotels, new Fixture().CreateMany<Hotel>(1).ToList());
            foreach (var item in counts)
            {
                var listObjects = f.CreateMany<SmallHotelChain>(item);
                var jsonSerializers = typeof(JsonSerialize).GetNestedTypes();
                var path = Path.Combine(Environment.CurrentDirectory, "../../../Data");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var jsonSerializer in jsonSerializers)
                {
                    var methodForList = jsonSerializer.GetMethods().Where(c=>c.Name=="Serialize" &&(c.ReturnType ==typeof(string) || c.ReturnType==typeof(byte[]))).FirstOrDefault();
                    methodForList = methodForList.MakeGenericMethod(typeof(IEnumerable<SmallHotelChain>));
                    var listList = new List<object> { listObjects };
                    var c = methodForList.GetParameters().Count();
                    for (int i = 1; i < c; i++)
                    {
                        listList.Add(null);
                    }
                    var listFileName = $"Serialized_{jsonSerializer.Name}_SmallHotelChain_{item}.json";

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
