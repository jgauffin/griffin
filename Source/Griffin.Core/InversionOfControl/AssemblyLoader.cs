using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Griffin.InversionOfControl;

namespace Griffin.Core.InversionOfControl
{
    /// <summary>
    /// Class used to load and register components in a container.
    /// </summary>
    /// <remarks>
    /// The loader scans all loaded assemblies for types that have been decorated with the <see cref="ComponentAttribute"/>. It takes
    /// those types and register it with all implemented interfaces (except .NET Framework interfaces). If a class do not
    /// imlement any interfaces it will be registered as itself.
    /// </remarks>
    /// <example>
    /// 
    /// </example>
    /// 
    /// 
    public class AssemblyLoader
    {
        private readonly List<Type> _loadedTypes = new List<Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyLoader"/> class.
        /// </summary>
        public AssemblyLoader()
        {
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoaded;
        }

        private void OnAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
        {
            ScanAssembly(args.LoadedAssembly);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        public void ScanAssembly(Assembly assembly)
        {
            string rootFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrEmpty(assembly.Location))
                return;

            string assemblyDir = Path.GetDirectoryName(assembly.Location);
            if (!assemblyDir.StartsWith(rootFolder))
                return;

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                ScanType(type);
            }
        }

        public void LoadAllTypes()
        {
            ScanStarted(this, EventArgs.Empty);
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                ScanAssembly(assembly);
            ScanEnded(this, EventArgs.Empty);
        }

        private void ScanType(Type type)
        {
            if (_loadedTypes.Contains(type))
                return;
            _loadedTypes.Add(type);

            var args = new TypeScannedEventArgs(type);
            TypeScanned(this, args);
        }

        /// <summary>
        /// Scan have been started
        /// </summary>
        public event EventHandler ScanStarted = delegate { };

        /// <summary>
        /// Scan has ended.
        /// </summary>
        public event EventHandler ScanEnded = delegate { };

        /// <summary>
        /// A type have been scanned.
        /// </summary>
        public event EventHandler<TypeScannedEventArgs> TypeScanned = delegate { };
    }
}