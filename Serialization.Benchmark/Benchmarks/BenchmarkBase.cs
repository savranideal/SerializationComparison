using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Benchmark.Benchmarks
{
    //[ShortRunJob]
    //[MediumRunJob]
    //[KeepBenchmarkFiles] 
    //[AsciiDocExporter]
    //[CsvExporter]
    //[CsvMeasurementsExporter]
    [HtmlExporter]
    [PlainExporter]
    //[RPlotExporter]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)] 
    [MemoryDiagnoser]
    [SimpleJob, MaxWarmupCount(8),MinIterationCount(3), MaxIterationCount(10)]
    public abstract class BenchmarkBase
    {

    }

    public abstract class BenchmarkCollectionBase : BenchmarkBase
    {

        [Params(1, 10, 100, 1000, 10_000, 100_000)] 
        //[Params(100_000)]
        public int DataCount { get; set; }


    }
     
}
