using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace JOS.MyLibrary.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net60)]
    [HtmlExporter]
    public class ReadFileBenchmark
    {
        private readonly string _path;
        private readonly EmbeddedResourceQuery _embeddedResourceQuery;
        public ReadFileBenchmark()
        {
            _path = "my-json-file.json";
            _embeddedResourceQuery = new EmbeddedResourceQuery();
        }

        [Benchmark(Baseline = true)]
        public JsonDocument FromDisk()
        {
            return JsonDocument.Parse(File.OpenRead(_path));
        }

        [Benchmark]
        public JsonDocument FromEmbeddedResource()
        {
            return JsonDocument.Parse(_embeddedResourceQuery.Read<IEmbeddedResourceQuery>(_path)!);
        }
    }
}
