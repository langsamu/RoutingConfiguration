// <copyright file="ConstraintElement.cs" company="Samu Lang">
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
    using System.ComponentModel;
    using System.Configuration;
    using System.Linq;
    using System.Web.Routing;
    using langsamu.Configuration;

    /// <summary>
    /// Represents a routing constraint configuration element.
    /// </summary>
    public class ConstraintElement : KeyedConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name of the routing constraint.
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
        /// Gets or sets the regular expression of the routing constraint.
        /// </summary>
        [ConfigurationProperty("regex")]
        [RegexValidator]
        public string Regex
        {
            get
            {
                return this["regex"] as string;
            }

            set
            {
                this["regex"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the routing constraint.
        /// </summary>
        /// <remarks>The type must be derived from <see cref="System.Web.Routing.IRouteConstraint"/>.</remarks>
        [ConfigurationProperty("type")]
        [TypeConverter(typeof(TypeNameConverter))]
        [SubclassTypeValidator(typeof(IRouteConstraint))]
        public Type ConstraintType
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
        /// Gets the parameters required for creating an instance of the <see cref="ConstraintType">routing constraint.</see>
        /// </summary>
        [ConfigurationProperty("parameters")]
        public AttributeDictionaryElement Parameters
        {
            get
            {
                return this["parameters"] as AttributeDictionaryElement;
            }
        }

        /// <summary>
        /// Gets the <see cref="Name">key</see> of the configuration element.
        /// </summary>
        public override object Key
        {
            get
            {
                return this.Name;
            }
        }

        /// <summary>
        /// Converts the element to a <see cref="System.Web.Routing.IRouteConstraint"/>.
        /// </summary>
        /// <returns>An instance of <see cref="System.Web.Routing.IRouteConstraint"/> created from the element.</returns>
        internal IRouteConstraint ToRouteConstraint()
        {
            return Activator.CreateInstance(this.ConstraintType, (object[])this.Parameters) as IRouteConstraint;
        }

        /// <summary>
        /// Validates the configuration element.
        /// </summary>
        protected override void PostDeserialize()
        {
            if (this.ConstraintType != null)
            {
                if (!string.IsNullOrEmpty(this.Regex))
                {
                    throw new ConfigurationErrorsException("@regex and @type are mutually exclusive");
                }

                KeyedConfigurationElement.ValidateParameters(this.ConstraintType, (object[])this.Parameters);
            }
            else
            {
                if (string.IsNullOrEmpty(this.Regex))
                {
                    throw new ConfigurationErrorsException("one of @regex or @type are required");
                }

                if (((RouteValueDictionary)this.Parameters).Any())
                {
                    throw new ConfigurationErrorsException("regex constraints cannot have create parameters");
                }
            }
        }
    }
}
