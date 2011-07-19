using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Griffin.Core.Reflection
{
    /// <summary>
    /// Traverses all assemblies (running and loaded during runtime) in the hunt of wanted assemblies.
    /// </summary>
    /// <remarks>
    /// It will keep a list of all found assemblies to be able to not trigger an event for the same assemly twice.
    /// </remarks>
    /// <example>
    /// <code>
    /// var observer = new AssemblyObserver();
    /// 
    /// observer.AddAssemblyFilter(AssemblyObserver.ExcludeDotNet);
    /// observer.AddAssemblyFilter(assembly => Assembly.Location.StarsWith("C:\\MyAppfolder");
    /// 
    /// observer.AddTypeFilter(type => !type.IsInterface && !type.IsAbstract);
    /// 
    /// observer.TypeScanned += (o,e) => Console.WriteLine("Found type: " + e.FoundType);
    /// observer.Observe();
    /// </code>
    /// </example>
    public class AssemblyObserver
    {
        private readonly List<Type> _loadedTypes = new List<Type>();
        public List<Func<Type, bool>>  _typeFilters = new List<Func<Type, bool>>();
        public List<Func<Assembly, bool>>  _assemblyFilters = new List<Func<Assembly, bool>>();


        private void OnAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
        {
            ScanAssembly(args.LoadedAssembly);
        }

        public void Observe()
        {
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoaded;
            ScanAllLoadedAssemblies();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        public void ScanAssembly(Assembly assembly)
        {

            foreach (Type type in assembly.GetTypes())
            {
                ScanType(type);
            }
        }


        private void ScanAllLoadedAssemblies()
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

        /// <summary>
        /// Method used to filter out Microsoft .NET framework assemblies.
        /// </summary>
        /// <param name="assembly">Assembly to check</param>
        /// <returns>true if it's not a .NET assemby; otherwise false.</returns>
        public static bool ExcludeDotNet(Assembly assembly)
        {
            var company = assembly.GetAttribute<AssemblyCompanyAttribute>(false);
            if (company != null && company.Company.ToLower().Contains("microsoft"))
                return false;
            if (assembly.GetName().Check(name => name.Name.ToLower().StartsWith("system") || name.Name.Contains("microsoft")))
                return false;
            if (assembly.Location.ToLower().Contains("mscorlib"))
                return false;

            return true;
        }

        /// <summary>
        /// Only include assemblies which is located in app folder or it's sub folders.
        /// </summary>
        /// <param name="assembly">Assembly to validate</param>
        /// <returns><c>true</c> if it can be included; otherwise <c>false</c>.</returns>
        public static bool OnlyInAppPath(Assembly assembly)
        {
            if (string.IsNullOrEmpty(AppDomain.CurrentDomain.BaseDirectory))
                return true;

            var assemblyDir = Path.GetDirectoryName(assembly.Location);
            return assemblyDir != null && assemblyDir.StartsWith(AppDomain.CurrentDomain.BaseDirectory);
        }

        public void AddAssemblyFilter(Func<Assembly, bool> filter)
        {
            _assemblyFilters.Add(filter);
        }

        public void AddTypeFilter(Func<Type, bool> filter)
        {
            _typeFilters.Add(filter);
        }

        /// <summary>
        /// Only include types that are concrete classes (exclude interfaces and abstract + static classes)
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns><c>true</c> if it can be included; otherwise <c>false</c>.</returns>
        public static bool OnlyConcreateClasses(Type type)
        {
            return !type.IsAbstract && !type.IsInterface; //static classes is really abstract/sealed.
        }
    }
}