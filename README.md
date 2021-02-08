``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.17134.1967 (1803/April2018Update/Redstone4)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-XCENQX : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

MaxIterationCount=10  MaxWarmupIterationCount=8  MinIterationCount=3  

|           Method |      Mean |      Error |     StdDev | Ratio | RatioSD |     Object Type |     Process Type |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|----------------- |----------:|-----------:|-----------:|------:|--------:|---------------- |----------------- |-------:|-------:|------:|----------:|
|         UTF8Json |  4.233 μs |  0.3615 μs |  0.2151 μs |  1.00 |    0.00 | SmallHotelChain | SerializedStream | 7.8049 | 0.4272 |     - |  32.09 KB |
|   SystemTextJson |  6.373 μs |  1.1648 μs |  0.6931 μs |  1.51 |    0.19 | SmallHotelChain | SerializedStream | 7.9346 | 0.7935 |     - |  32.54 KB |
| ServiceStackText |  6.659 μs |  0.6720 μs |  0.4445 μs |  1.58 |    0.15 | SmallHotelChain | SerializedStream | 9.2163 |      - |     - |  37.84 KB |
|       Newtonsoft |  9.048 μs |  1.0028 μs |  0.6633 μs |  2.11 |    0.16 | SmallHotelChain | SerializedStream | 9.4299 | 0.9308 |     - |  38.59 KB |
|              Jil | 26.789 μs |  5.1366 μs |  3.0567 μs |  6.35 |    0.86 | SmallHotelChain | SerializedStream |      - |      - |     - |   37.7 KB |
|          Swifter | 49.680 μs | 26.0204 μs | 17.2109 μs | 12.36 |    3.67 | SmallHotelChain | SerializedStream |      - |      - |     - |  37.71 KB |
