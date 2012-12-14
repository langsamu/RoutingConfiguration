// <copyright file="ProviderElement.cs" company="Samu Lang">
//      Copyright (c) 2012 Samu Lang
//
//      Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It's my name")]

namespace langsamu.Web.Routing.Configuration
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Configuration;
    using langsamu.Configuration;

    /// <summary>
    /// Represents a provider configuration element.
    /// </summary>
    public class ProviderElement : KeyedConfigurationElement
    {
        /// <summary>
        /// Holds provider specific initialization parameters obtained from unrecognized attributes.
        /// </summary>
        private NameValueCollection parameters = new NameValueCollection();

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }

            set
            {
                this["name"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the provider.
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        [TypeConverter(typeof(TypeNameConverter))]
        [SubclassTypeValidator(typeof(RouteProvider))]
        public Type ProviderType
        {
            get
            {
                return this["type"] as Type;
            }

            set
            {
                this["type"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a collection of provider specific initialization parameters.
        /// </summary>
        public NameValueCollection Parameters
        {
            get
            {
                return this.parameters;
            }

            set
            {
                this.parameters = value;
            }
        }

        /// <summary>
        /// Gets the key of the configuration element.
        /// </summary>
        internal override object Key
        {
            get
            {
                return this.Name;
            }
        }

        /// <summary>
        /// Responsible for interpreting provider specific initialization parameters. 
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <returns>Always returns true.</returns>
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            this.Parameters[name] = value;

            return true;
        }
    }
}