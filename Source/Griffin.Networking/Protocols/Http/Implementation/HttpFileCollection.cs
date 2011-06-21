using System;
using System.Collections.Generic;
using System.IO;

namespace Griffin.Core.Net.Protocols.Http.Implementation
{
    /// <summary>
    /// Collection of files.
    /// </summary>
    public class HttpFileCollection : IHttpFileCollection
    {
        private readonly Dictionary<string, IHttpFile> _files =
            new Dictionary<string, IHttpFile>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Get a file
        /// </summary>
        /// <param name="name">Name in form</param>
        /// <returns>File if found; otherwise <c>null</c>.</returns>
        public IHttpFile this[string name]
        {
            get
            {
                IHttpFile file;
                return _files.TryGetValue(name, out file) ? file : null;
            }
        }

        /// <summary>
        /// Checks if a file exists.
        /// </summary>
        /// <param name="name">Name of the file (form item name)</param>
        /// <returns></returns>
        public bool Contains(string name)
        {
            return _files.ContainsKey(name);
        }


        /// <summary>
        /// Gets number of files
        /// </summary>
        public int Count
        {
            get { return _files.Count; }
        }

        /// <summary>
        /// Add a new file.
        /// </summary>
        /// <param name="file">File to add.</param>
        public void Add(IHttpFile file)
        {
            _files.Add(file.Name, file);
        }

        /// <summary>
        /// Remove all files from disk.
        /// </summary>
        public void Clear()
        {
            foreach (HttpFile file in _files.Values)
            {
                if (File.Exists(file.TempFileName))
                    File.Delete(file.TempFileName);
            }
        }
    }
}