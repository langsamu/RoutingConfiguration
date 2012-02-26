// <copyright file="SimpleElementCollection.cs" company="Samu Lang">
//      Copyright (c) 2012 Samu Lang
//
//      Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It's my name")]

namespace langsamu.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Represents a generic collection of simple configuration elements.
    /// </summary>
    /// <typeparam name="T">The type of configuration element to use.</typeparam>
    public abstract class SimpleElementCollection<T> : ConfigurationElementCollection, System.Collections.Generic.IEnumerable<T> where T : ConfigurationElement, new()
    {
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            foreach (var item in this)
            {
                yield return item as T;
            }
        }

        /// <summary>
        /// Creates a new instance of the given configuration element type.
        /// </summary>
        /// <returns>The newly created configuration element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        /// <summary>
        /// Retrieves the key of the given configuration element.
        /// </summary>
        /// <param name="element">The configuration element to get the key for.</param>
        /// <returns>The key of the configuration element if the element is a <see cref="langsamu.Configuration.KeyedConfigurationElement"/>, otherwise the element itself.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            var keyed = (KeyedConfigurationElement)element;

            if (keyed != null)
            {
                return keyed.Key;
            }
            else
            {
                return element;
            }
        }
    }
}
