using System;
using Microsoft.Win32;

namespace Hopex.RegEdit
{
    /// <summary>
    /// Registry workflow.
    /// </summary>
    public class RegEdit
    {
        /// <summary>
        /// Root registry key.
        /// </summary>
        private RegistryKey registryKey { get; } = Registry.CurrentUser;

        /// <summary>
        /// Returns the number of values in the section.
        /// </summary>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="System.IO.IOException"/>
        public int ValueCount => registryKey.ValueCount;

        /// <summary>
        /// Returns the number of subsections for the current section.
        /// </summary>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="System.IO.IOException"/>
        public int SubKeyCount => registryKey.SubKeyCount;

        /// <summary>
        /// Registry workflow.
        /// </summary>
        /// <param name="registryKey">Root registry key. By default HKEY_CURRENT_USER.</param>
        public RegEdit(RegistryKey registryKey = null)
        {
            if (!registryKey.Equals(null))
            {
                this.registryKey = registryKey;
            }
        }

        /// <summary>
        /// Validating registry path.
        /// </summary>
        /// <param name="path">Registry path.</param>
        /// <returns>Valid registry path.</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        private string PreparePath(string path) => path.Replace("/", "\\").Trim('\\');

        /// <summary>
        /// Writing data to the registry.
        /// </summary>
        /// <param name="path">The name or path of the subsection being created or opened. This line is case-insensitive.</param>
        /// <param name="parameter">The name of the parameter to save.</param>
        /// <param name="value">To save data.</param>
        /// <param name="writable">A value of true indicates that the new subsection is writable; otherwise, the value is false.</param>
        /// <param name="registryOptions">Registry parameter to use.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="System.IO.IOException"/>
        public void Write(string path, string parameter, object value, bool writable = true, RegistryOptions registryOptions = RegistryOptions.None)
        {
            registryKey
                .CreateSubKey(
                    subkey: PreparePath(path),
                    writable: writable,
                    options: registryOptions
                )
                .SetValue(
                    name: parameter,
                    value: value
                );
        }

        /// <summary>
        /// Reading data from the registry.
        /// </summary>
        /// <param name="path">The name or path of the subsection being opened. This line is case-insensitive.</param>
        /// <param name="parameter">The name of the parameter to read.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="System.IO.IOException"/>
        public object Read(string path, string parameter)
        {
            return registryKey
                .OpenSubKey(name: PreparePath(path))
                .GetValue(name: parameter);
        }

        /// <summary>
        /// Deleting data from the registry.
        /// </summary>
        /// <param name="path">The name or path of the subsection being removed. This line is case-insensitive.</param>
        /// <param name="parameter">The name of the parameter to remove.</param>
        /// <param name="throwOnMissingValue">Indicates whether an exception should be thrown if the specified value cannot be found.
        /// If this argument is true and the specified value does not exist, an exception occurs.
        /// If this argument is false and the specified value does not exist, no actions are performed.
        /// </param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="System.IO.IOException"/>
        public void DeleteValue(string path, string parameter, bool throwOnMissingValue = false)
        {
            registryKey
                .OpenSubKey(name: PreparePath(path))
                .DeleteValue(
                    name: parameter,
                    throwOnMissingValue: throwOnMissingValue
                );
        }

        /// <summary>
        /// Normal or recursive deletion of a registry key.
        /// </summary>
        /// <param name="path">The name or path of the subsection being removed. This line is case-insensitive.</param>
        /// <param name="recurcive">Indicates the deletion of a subsection and all child subsections recursively.</param>
        /// <param name="throwOnMissingSubKey">Specifies whether an exception should be thrown if the specified subsection cannot be found.
        /// If this argument is true and the specified subsection does not exist, an exception occurs.
        /// If this argument is false and the specified subsection does not exist, no actions are performed.
        /// </param>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="System.IO.IOException"/>
        public void DeleteKey(string path, bool recurcive = false, bool throwOnMissingSubKey = false)
        {
            if (recurcive)
            {
                registryKey.DeleteSubKeyTree(
                    subkey: path,
                    throwOnMissingSubKey: throwOnMissingSubKey
                );
            }
            else
            {
                registryKey.DeleteSubKey(
                    subkey: path,
                    throwOnMissingSubKey: throwOnMissingSubKey
                );
            }
        }

        /// <summary>
        /// Checks the specified registry key and returns the number of its values.
        /// </summary>
        /// <param name="path">The name or path of the subsection being checked. This line is case-insensitive.</param>
        /// <returns>Number of values in the specified section.</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="System.IO.IOException"/>
        public int GetValueCount(string path) => registryKey.OpenSubKey(name: path).ValueCount;

        /// <summary>
        /// Checks the specified registry key and returns the number of its partitions.
        /// </summary>
        /// <param name="path">The name or path of the subsection being checked. This line is case-insensitive.</param>
        /// <returns>Number of subsections for the specified section.</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="System.IO.IOException"/>
        public int GetSubKeyCount(string path) => registryKey.OpenSubKey(name: path).SubKeyCount;
    }
}
