# Demo of using libs for processing images in ASP.NET Core applications.

## Libraries:
1. [ImageSharp](https://github.com/SixLabors/ImageSharp)
2. [SkiaSharp](https://github.com/mono/SkiaSharp)
3. [System.Drawing](https://learn.microsoft.com/en-us/dotnet/api/system.drawing?view=net-6.0)

## Benchmark result:

```
BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1098/21H2)
AMD Ryzen 5 5500U with Radeon Graphics, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.400
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2

```
Resize FullHD screenshot to HD.
![image](https://user-images.githubusercontent.com/6265637/197702678-14e1ff18-cb01-4cc9-97e4-97b0d1902d29.png)
