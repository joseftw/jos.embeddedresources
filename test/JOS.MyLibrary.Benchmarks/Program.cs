using BenchmarkDotNet.Running;

namespace JOS.MyLibrary.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary1 = BenchmarkRunner.Run<EmbeddedResourceQueryBenchmark>();
        }
    }
}