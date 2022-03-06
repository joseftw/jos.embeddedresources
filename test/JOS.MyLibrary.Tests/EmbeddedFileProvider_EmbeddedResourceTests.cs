using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.FileProviders;
using Shouldly;
using Xunit;

namespace JOS.MyLibrary.Tests
{
    public class EmbeddedFileProvider_EmbeddedResourceTests
    {
        [Fact]
        public void CanReadMyJsonFileEmbeddedResource()
        {
            var libraryAssembly = Assembly.GetAssembly(typeof(IEmbeddedResourceQuery));
            var embeddedProvider = new EmbeddedFileProvider(libraryAssembly!);

            using var jsonStream = embeddedProvider.GetFileInfo("my-json-file.json").CreateReadStream();
            var data = JsonDocument.Parse(jsonStream);

            data.RootElement.GetProperty("data").GetBoolean().ShouldBeTrue();
        }
    }
}