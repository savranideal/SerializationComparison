``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.17134.1967 (1803/April2018Update/Redstone4)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]    : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  MediumRun : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

Job=MediumRun  IterationCount=15  LaunchCount=2  
WarmupCount=10  Categories=Json_Small  

```
|           Method |       Mean |       Error |      StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |-----------:|------------:|------------:|-------:|------:|------:|----------:|
|         UTF8Json |   311.9 ns |    24.44 ns |    36.57 ns | 0.0534 |     - |     - |     224 B |
|    SimdJsonSharp |   318.7 ns |    10.40 ns |    14.92 ns | 0.0534 |     - |     - |     224 B |
|   SystemTextJson |   630.4 ns |    20.32 ns |    30.41 ns | 0.1354 |     - |     - |     568 B |
|              Jil |   846.6 ns |    22.03 ns |    32.29 ns | 0.3595 |     - |     - |    1504 B |
| ServiceStackText |   967.6 ns |    31.70 ns |    46.46 ns | 0.1812 |     - |     - |     760 B |
|       Newtonsoft | 1,417.4 ns |    47.00 ns |    67.41 ns | 0.5035 |     - |     - |    2112 B |
|         FastJson | 1,968.8 ns |    70.87 ns |   101.65 ns | 0.9575 |     - |     - |    4008 B |
|          Swifter | 8,470.4 ns | 3,008.91 ns | 4,218.08 ns |      - |     - |     - |     800 B |
