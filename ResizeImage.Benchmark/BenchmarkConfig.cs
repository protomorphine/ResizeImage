using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;

namespace ResizeImage.Benchmark;

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddDiagnoser(MemoryDiagnoser.Default);
        AddDiagnoser(ThreadingDiagnoser.Default);
    }
}