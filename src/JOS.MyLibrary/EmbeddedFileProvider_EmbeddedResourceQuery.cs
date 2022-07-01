using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace JOS.MyLibrary
{
    public class EmbeddedFileProvider_EmbeddedResourceQuery : IEmbeddedResourceQuery
    {
        private static readonly Dictionary<Assembly, EmbeddedFileProvider> FileProviders;

        static EmbeddedFileProvider_EmbeddedResourceQuery()
        {
            FileProviders = new Dictionary<Assembly, EmbeddedFileProvider>();
        }

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
            if (!FileProviders.ContainsKey(assembly))
            {
                FileProviders[assembly] = new EmbeddedFileProvider(assembly);
            }
            
            return FileProviders[assembly].GetFileInfo(resource).CreateReadStream();
        }
    }
}
