// <copyright file="KeyedConfigurationElement.cs" company="Samu Lang">
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
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using langsamu.Web.Routing.Configuration;

    /// <summary>
    /// Represents a configuration element that has a key.
    /// </summary>
    public abstract class KeyedConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the key of the configuration elements.
        /// </summary>
        public abstract object Key { get; }

        /// <summary>
        /// Checks whether the specified type has a constructor with the appropriate number of parameters.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="parameters">The collection of parapeters.</param>
        internal static void ValidateParameters(Type type, object[] parameters)
        {
            var parameterCount = parameters.Count();

            Type[] parameterTypes = new Type[parameterCount];

            for (int i = 0; i < parameterCount; i++)
            {
                parameterTypes[i] = typeof(string);
            }

            var constructor = type.GetConstructor(parameterTypes);

            if (constructor == null)
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.InvariantCulture, Resources.IncorrectParametersForInitialisation, type));
            }
        }
    }
}
