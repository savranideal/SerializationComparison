﻿using AutoFixture;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Serialization.Benchmark.Models;
using Serialization.Benchmark.Models.Geography;
using Serialization.Benchmark.Models.Hotel;
using Serialization.Libraries.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serialization.Benchmark.Benchmarks
{

    [Config(typeof(Config))]
    public class SerializeSmallSize: BenchmarkBase
    {
        private class Config : ManualConfig
        {
            public Config()
            { 
                AddColumn(new TagColumn("Format", name => "Json Serialize"));
                AddColumn(new TagColumn("Object Size", name => "Small")); 
            }
        }
        public HotelChain Data { get; set; } 
        [GlobalSetup]
        public void Setup()
        {
            var f = new Fixture() { RepeatCount = 1 };
            f.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString().Substring(0, 2)));

            Data = f.Create<HotelChain>();
        }
        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string Newtonsoft()
        {
            return JsonSerialize.Newtonsoft.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public byte[] UTF8Json()
        {
            return JsonSerialize.Utf8Json.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string FastJson()
        {
            return JsonSerialize.FastJson.Serialize(Data);

        }
        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string ServiceStackText()
        {
            return JsonSerialize.ServiceStackText.Serialize(Data); 
        }
       

        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string Swifter()
        {
            return JsonSerialize.Swifter.Serialize(Data); 
        }

        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string SystemTextJson()
        {
            return JsonSerialize.SystemTextJson.Serialize(Data); 
        }
        [Benchmark]
        [BenchmarkCategory("Json_Small")]
        public string Jil()
        {
            return JsonSerialize.JIL.Serialize(Data); 
        }


    }

     
}
