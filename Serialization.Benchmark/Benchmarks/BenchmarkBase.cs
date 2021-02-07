using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Benchmark.Benchmarks
{
    //[ShortRunJob]
    [MediumRunJob]
    //[KeepBenchmarkFiles] 
    //[AsciiDocExporter]
    //[CsvExporter]
    //[CsvMeasurementsExporter]
    [HtmlExporter]
    [PlainExporter]
    //[RPlotExporter]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    [MemoryDiagnoser]
    public abstract class BenchmarkBase
    {

    }

    public abstract class BenchmarkCollectionBase : BenchmarkBase
    {

        [Params(1, 10, 100, 500, 1000)]
        public int DataCount { get; set; }


    }
}
