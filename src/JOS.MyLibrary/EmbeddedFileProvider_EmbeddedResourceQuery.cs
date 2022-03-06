using System.IO;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace JOS.MyLibrary
{
    public class EmbeddedFileProvider_EmbeddedResourceQuery : IEmbeddedResourceQuery
    {
        public Stream? Read<T>(string resource)
        {
            return ReadInternal(typeof(T).Assembly, resource);
        }

        public Stream? Read(Assembly assembly, string resource)
        {
            return ReadInternal(assembly, resource);
        }

        public Stream? Read(string assemblyName, string resource)
        {
            var assembly = Assembly.Load(assemblyName);
            return ReadInternal(assembly, resource);
        }

        internal static Stream? ReadInternal(Assembly assembly, string resource)
        {
            var embeddedProvider = new EmbeddedFileProvider(assembly);
            return embeddedProvider.GetFileInfo(resource).CreateReadStream();
        }
    }
}
