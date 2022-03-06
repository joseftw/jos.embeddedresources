using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace JOS.MyLibrary.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net60)]
    [HtmlExporter]
    public class EmbeddedResourceQueryBenchmark
    {
        private readonly EmbeddedResourceQuery _embeddedResourceQuery;
        private readonly EmbeddedFileProvider_EmbeddedResourceQuery _embeddedFileProviderEmbeddedResourceQuery;

        public EmbeddedResourceQueryBenchmark()
        {
            _embeddedResourceQuery = new EmbeddedResourceQuery();
            _embeddedFileProviderEmbeddedResourceQuery = new EmbeddedFileProvider_EmbeddedResourceQuery();
        }

        [Benchmark(Baseline = true)]
        public Stream? EmbeddedResourceQuery_Generic()
        {
            return _embeddedResourceQuery.Read<IEmbeddedResourceQuery>("my-json-file.json");
        }

        [Benchmark]
        public Stream? EmbeddedFileProvider_Generic()
        {
            return _embeddedFileProviderEmbeddedResourceQuery.Read<IEmbeddedResourceQuery>("my-json-file.json");
        }

        [Benchmark]
        public Stream? EmbeddedResourceQuery_AssemblyNameAndResource()
        {
            return _embeddedResourceQuery.Read("JOS.MyLibrary", "my-json-file.json");
        }

        [Benchmark]
        public Stream? EmbeddedFileProvider_AssemblyNameAndResource()
        {
            return _embeddedFileProviderEmbeddedResourceQuery.Read("JOS.MyLibrary", "my-json-file.json");
        }

        [Benchmark]
        public Stream? EmbeddedResourceQuery_AssemblyAndResource()
        {
            var assembly = Assembly.Load("JOS.MyLibrary");
            return _embeddedResourceQuery.Read(assembly, "my-json-file.json");
        }

        [Benchmark]
        public Stream? EmbeddedFileProvider_AssemblyAndResource()
        {
            var assembly = Assembly.Load("JOS.MyLibrary");
            return _embeddedFileProviderEmbeddedResourceQuery.Read(assembly, "my-json-file.json");
        }
    }
}
