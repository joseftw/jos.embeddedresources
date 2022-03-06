using System.Text.Json;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace JOS.MyLibrary.Tests
{
    public class EmbeddedFileProvider_EmbeddedResourceQueryTests
    {
        private readonly EmbeddedFileProvider_EmbeddedResourceQuery _sut;

        public EmbeddedFileProvider_EmbeddedResourceQueryTests()
        {
            _sut = new EmbeddedFileProvider_EmbeddedResourceQuery();
        }

        [Theory]
        [InlineData("my-json-file.json")]
        [InlineData("Data.data.json")]
        public async Task CanReadEmbeddedResource_Generic(string resource)
        {
            await using var stream = _sut.Read<IEmbeddedResourceQuery>(resource);

            var jsonDocument = await JsonDocument.ParseAsync(stream!);
            jsonDocument.RootElement.GetProperty("data").GetBoolean().ShouldBeTrue();
        }

        [Theory]
        [InlineData("my-json-file.json")]
        [InlineData("Data.data.json")]
        public async Task CanReadEmbeddedResource_AssemblyAndResource(string resource)
        {
            var assembly = typeof(IEmbeddedResourceQuery).Assembly;
            await using var stream = _sut.Read(assembly, resource);

            var jsonDocument = await JsonDocument.ParseAsync(stream!);
            jsonDocument.RootElement.GetProperty("data").GetBoolean().ShouldBeTrue();
        }

        [Theory]
        [InlineData("my-json-file.json")]
        [InlineData("Data.data.json")]
        public async Task CanReadEmbeddedResource_AssemblyNameAndResource(string resource)
        {
            await using var stream = _sut.Read("JOS.MyLibrary", resource);

            var jsonDocument = await JsonDocument.ParseAsync(stream!);
            jsonDocument.RootElement.GetProperty("data").GetBoolean().ShouldBeTrue();
        }
    }
}
