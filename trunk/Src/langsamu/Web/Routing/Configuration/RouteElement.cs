// <copyright file="RouteElement.cs" company="Samu Lang">
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
    using langsamu.Configuration;
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Configuration;
    using System.Web.Routing;

    /// <summary>
    /// Represents a route configuration element.
    /// </summary>
    public class RouteElement : KeyedConfigurationElement
    {
        /// <summary>
        /// Gets or sets the url of the route.
        /// </summary>
        [ConfigurationProperty("url")]
        [RouteUrlConfigurationValidator]
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Follows naming in System.Web.Routing.Route and System.Web.Routing.RouteCollection.MapPageRoute")]
        public string Url
        {
            get
            {
                return this["url"] as string;
            }

            set
            {
                this["url"] = value;
            }
        }

        /// <summary>
        /// Gets the collection of constraints associated with the route.
        /// </summary>
        [ConfigurationProperty("constraints")]
        public ConstraintCollection Constraints
        {
            get
            {
                return this["constraints"] as ConstraintCollection;
            }
        }

        /// <summary>
        /// Gets or sets the name of the route.
        /// </summary>
        [ConfigurationProperty("name")]
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
        /// Gets or sets the type of the route.
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        [TypeConverter(typeof(GenericEnumConverter<RouteElementType>))]
        public RouteElementType ElementType
        {
            get
            {
                return (RouteElementType)this["type"];
            }

            set
            {
                this["type"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the physical file to use for the route.
        /// </summary>
        [ConfigurationProperty("physicalFile")]
        [VirtualPathValidator]
        public string PhysicalFile
        {
            get
            {
                return this["physicalFile"] as string;
            }

            set
            {
                this["physicalFile"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to check physical url access.
        /// </summary>
        [ConfigurationProperty("checkPhysicalUrlAccess")]
        public bool CheckPhysicalUrlAccess
        {
            get
            {
                return (bool)this["checkPhysicalUrlAccess"];
            }

            set
            {
                this["checkPhysicalUrlAccess"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of <see cref="IRouteHandler">route handler</see> associated with the route.
        /// </summary>
        [ConfigurationProperty("handler")]
        [TypeConverter(typeof(TypeNameConverter))]
        [SubclassTypeValidator(typeof(IRouteHandler))]
        public Type HandlerType
        {
            get
            {
                return this["handler"] as Type;
            }

            set
            {
                this["handler"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of <see cref="RouteBase">route base derived class</see> associated with the route.
        /// </summary>
        [ConfigurationProperty("route")]
        [TypeConverter(typeof(TypeNameConverter))]
        [SubclassTypeValidator(typeof(RouteBase))]
        public Type RouteType
        {
            get
            {
                return this["route"] as Type;
            }

            set
            {
                this["route"] = value;
            }
        }

        /// <summary>
        /// Gets a collection of parameters used to create the instance of route or route handler.
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
        /// Gets a collection of namespaces for the Mvc route.
        /// </summary>
        [ConfigurationProperty("namespaces")]
        public NamespaceCollection Namespaces
        {
            get
            {
                return this["namespaces"] as NamespaceCollection;
            }
        }

        /// <summary>
        /// Gets a collection of default values associated with the route.
        /// </summary>
        [ConfigurationProperty("defaults")]
        public AttributeDictionaryElement Defaults
        {
            get
            {
                return this["defaults"] as AttributeDictionaryElement;
            }
        }

        /// <summary>
        /// Gets a collection of date tokens associated with the route.
        /// </summary>
        [ConfigurationProperty("tokens")]
        public AttributeDictionaryElement DataTokens
        {
            get
            {
                return this["tokens"] as AttributeDictionaryElement;
            }
        }

        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        internal override object Key
        {
            get
            {
                switch (this.ElementType)
                {
                    case RouteElementType.Ignore:
                        return this.Url;

                    default:
                        return this.Name;
                }
            }
        }

        /// <summary>
        /// Validates required and forbidden attributes on the configuration element based on its <see cref="langsamu.Web.Routing.Configuration.RouteElement.ElementType"/>.
        /// </summary>
        protected override void PostDeserialize()
        {
            var missingConstraints = !((RouteValueDictionary)this.Constraints).Any();
            var missingDataTokens = !((RouteValueDictionary)this.DataTokens).Any();
            var missingDefaults = !((RouteValueDictionary)this.Defaults).Any();
            var missingHandler = this.HandlerType == null;
            var missingName = string.IsNullOrEmpty(this.Name);
            var missingParameters = !((RouteValueDictionary)this.Parameters).Any();
            var missingPhysicalFile = string.IsNullOrEmpty(this.PhysicalFile);
            var missingRoute = this.RouteType == null;
            var missingUrl = this.Url == null;
            var missingNamespaces = this.Namespaces.Count == 0;

            switch (this.ElementType)
            {
                case RouteElementType.PhysicalFile:
                    if (missingName || missingPhysicalFile || missingUrl)
                    {
                        throw new ConfigurationErrorsException(Resources.RoutePhysicalAttributesRequired);
                    }

                    if (!(missingHandler && missingParameters && missingRoute && missingNamespaces))
                    {
                        throw new ConfigurationErrorsException(Resources.RoutePhysicalAttributesForbidden);
                    }

                    break;

                case RouteElementType.Ignore:
                    if (missingUrl)
                    {
                        throw new ConfigurationErrorsException(Resources.RouteIgnoreAttributesRequired);
                    }

                    if (!(missingDataTokens && missingDefaults && missingHandler && missingName && missingParameters && missingPhysicalFile && missingRoute && missingNamespaces))
                    {
                        throw new ConfigurationErrorsException(Resources.RouteIgnoreAttributesForbidden);
                    }

                    break;

                case RouteElementType.RouteBase:
                    if (missingName || missingRoute)
                    {
                        throw new ConfigurationErrorsException(Resources.RouteRouteAttributesRequired);
                    }

                    if (!(missingConstraints && missingDataTokens && missingDefaults && missingHandler && missingPhysicalFile && missingNamespaces))
                    {
                        throw new ConfigurationErrorsException(Resources.RouteRouteAttributesForbidden);
                    }

                    KeyedConfigurationElement.ValidateParameters(this.RouteType, (object[])this.Parameters);

                    break;

                case RouteElementType.IRouteHandler:
                    if (missingName || missingHandler || missingUrl)
                    {
                        throw new ConfigurationErrorsException(Resources.RouteHandlerAttributesRequired);
                    }

                    if (!(missingPhysicalFile && missingRoute && missingNamespaces))
                    {
                        throw new ConfigurationErrorsException(Resources.RouteHandlerAttributesForbidden);
                    }

                    KeyedConfigurationElement.ValidateParameters(this.HandlerType, (object[])this.Parameters);

                    break;

                case RouteElementType.Mvc:
                    if (missingName || missingUrl)
                    {
                        throw new ConfigurationErrorsException(Resources.RouteMvcAttributesRequired);
                    }

                    if (!(missingHandler && missingParameters && missingPhysicalFile && missingRoute))
                    {
                        throw new ConfigurationErrorsException(Resources.RouteMvcAttributesForbidden);
                    }

                    break;
            }
        }
    }
}
