using AutoFixture;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Serialization.Benchmark.Models;
using Serialization.Benchmark.Models.Geography;
using Serialization.Benchmark.Models.Hotel;
using Serialization.Libraries.Binary;
using Serialization.Libraries.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Serialization.Benchmark.Benchmarks
{

    [CategoriesColumn]
    public class SerializeToStream<TModel> : BenchmarkBase
    {
        public TModel Data { get; set; }
        [GlobalSetup]
        public void Setup()
        {
            var f = new Fixture() { RepeatCount = 1 };
            f.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString().Substring(0, 2)));
            Data = f.Create<TModel>();

        }
        [Benchmark]
        public void Newtonsoft()
        {

            JsonSerialize.Newtonsoft.Serialize(Data, new MemoryStream(short.MaxValue));

        }
        [Benchmark(Baseline = true)]
        public void UTF8Json()
        {


            JsonSerialize.Utf8Json.Serialize(Data, new MemoryStream(short.MaxValue));

        }

        [Benchmark]
        public void ServiceStackText()
        {
            JsonSerialize.ServiceStackText.Serialize(Data, new MemoryStream(short.MaxValue));
        }


        [Benchmark]
        public void Swifter()
        {
            JsonSerialize.Swifter.Serialize(Data, new MemoryStream(short.MaxValue));
        }

        [Benchmark]
        public void SystemTextJson()
        {

            JsonSerialize.SystemTextJson.Serialize(Data, new MemoryStream(short.MaxValue));
        }
        [Benchmark]
        public void Jil()
        { 
            JsonSerialize.JIL.Serialize(Data, new MemoryStream(short.MaxValue));
        }




    }


}
