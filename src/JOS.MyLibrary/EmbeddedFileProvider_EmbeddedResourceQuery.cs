using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace JOS.MyLibrary
{
    public class EmbeddedFileProvider_EmbeddedResourceQuery : IEmbeddedResourceQuery
    {
        private readonly Dictionary<Assembly, EmbeddedFileProvider> _fileProviders;

        public EmbeddedFileProvider_EmbeddedResourceQuery() : this(Array.Empty<Assembly>())
        {
        }

        public EmbeddedFileProvider_EmbeddedResourceQuery(IEnumerable<Assembly> assembliesToPreload)
        {
            _fileProviders = new Dictionary<Assembly, EmbeddedFileProvider>();
            foreach (var assembly in assembliesToPreload)
            {
                var embeddedFileProvider = new EmbeddedFileProvider(assembly);
                _fileProviders.Add(assembly, embeddedFileProvider);
            }
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

        internal Stream? ReadInternal(Assembly assembly, string resource)
        {
            if (!_fileProviders.ContainsKey(assembly))
            {
                _fileProviders[assembly] = new EmbeddedFileProvider(assembly);
            }
            
            return _fileProviders[assembly].GetFileInfo(resource).CreateReadStream();
        }
    }
}
