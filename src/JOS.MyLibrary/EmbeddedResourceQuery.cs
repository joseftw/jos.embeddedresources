using System.IO;
using System.Reflection;

namespace JOS.MyLibrary
{
    public class EmbeddedResourceQuery : IEmbeddedResourceQuery
    {
        public Stream? Read<T>(string resource)
        {
            var assembly = typeof(T).Assembly;
            return ReadInternal(assembly, resource);
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
            return assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{resource}");
        }
    }
}
