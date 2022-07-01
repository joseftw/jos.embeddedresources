using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace JOS.MyLibrary
{
    public class EmbeddedResourceQuery : IEmbeddedResourceQuery
    {
        private static readonly Dictionary<Assembly, string> AssemblyNames;

        static EmbeddedResourceQuery()
        {
            AssemblyNames = new Dictionary<Assembly, string>();
        }

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
            if (!AssemblyNames.ContainsKey(assembly))
            {
                AssemblyNames[assembly] = assembly.GetName().Name!;
            }
            return assembly.GetManifestResourceStream($"{AssemblyNames[assembly]}.{resource}");
        }
    }
}
