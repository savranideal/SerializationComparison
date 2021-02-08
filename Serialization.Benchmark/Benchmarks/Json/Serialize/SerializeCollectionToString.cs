using AutoFixture;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Serialization.Benchmark.Models;
using Serialization.Benchmark.Models.Geography;
using Serialization.Benchmark.Models.Hotel;
using Serialization.Libraries.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serialization.Benchmark.Benchmarks
{

    public class SerializeCollectionToString<TCollection> : BenchmarkCollectionBase where TCollection:class, ICreateableSerializeableCollection<TCollection> ,new()
    {
        public TCollection Data { get; set; }

       
        [GlobalSetup]
        public void Setup()
        {
            Data = new TCollection().Create(DataCount);
        }

        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string Newtonsoft()
        {
            return JsonSerialize.Newtonsoft.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public byte[] UTF8Json()
        {
            return JsonSerialize.Utf8Json.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string FastJson()
        {
            return JsonSerialize.FastJson.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string ServiceStackText()
        {
            return JsonSerialize.ServiceStackText.Serialize(Data);
        }
       

        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string Swifter()
        {
            return JsonSerialize.Swifter.Serialize(Data);
        }

        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string SystemTextJson()
        {
            return JsonSerialize.SystemTextJson.Serialize(Data);
        }

        [Benchmark]
        [BenchmarkCategory("Json_Dictionary")]
        public string Jil()
        {
            return JsonSerialize.JIL.Serialize(Data);
        }

    }


}
