// <copyright file="AttributeDictionaryElement.cs" company="Samu Lang">
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
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web.Routing;

    /// <summary>
    /// Represents a configuration element that stores its attributes as a dictionary.
    /// </summary>
    public class AttributeDictionaryElement : ConfigurationElement
    {
        /// <summary>
        /// Holds the accumulated attributes.
        /// </summary>
        private RouteValueDictionary attributes = new RouteValueDictionary();

        /// <summary>
        /// Converts the element into a <see cref="System.Web.Routing.RouteValueDictionary"/>
        /// </summary>
        /// <param name="original">The <see cref="langsamu.Configuration.AttributeDictionaryElement"/> being converted.</param>
        /// <returns>A <see cref="System.Web.Routing.RouteValueDictionary"/> containing the accumulated attributes from this element.</returns>
        public static explicit operator RouteValueDictionary(AttributeDictionaryElement original)
        {
            return original.attributes;
        }

        /// <summary>
        /// Converts the element into a <see cref="System.Object[]"/>.
        /// </summary>
        /// <param name="original">The <see cref="langsamu.Configuration.AttributeDictionaryElement"/> being converted.</param>
        /// <returns>A <see cref="System.Object[]"/> containing the values off accumulated attributes from this element.</returns>
        public static explicit operator object[](AttributeDictionaryElement original)
        {
            return original.attributes.Values.ToArray();
        }

        /// <summary>
        /// Adds each unknown attribute to the dictionary.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="value">Value of the attribute.</param>
        /// <returns>True to signify that adding the attribute was successful.</returns>
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            this.attributes.Add(name, value);

            return true;
        }
    }
}
