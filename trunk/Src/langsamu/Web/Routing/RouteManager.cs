// <copyright file="RouteManager.cs" company="Samu Lang">
//      Copyright (c) 2012 Samu Lang
//
//      Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It's my name")]

namespace langsamu.Web.Routing
{
    using langsamu.Web.Routing.Configuration;
    using System;
    using System.Linq;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// A helper containing utility methods for working with declarative route configuration.
    /// </summary>
    public static class RouteManager
    {
        /// <summary>
        /// Gets the current provider.
        /// </summary>
        internal static RouteProvider Provider
        {
            get
            {
                var providerName = RouteManager.Config.DefaultProviderName;
                var providerElement = RouteManager.Config.Providers.Where(provider => provider.Name == providerName).Single();
                var providerInstance = System.Activator.CreateInstance(providerElement.ProviderType) as RouteProvider;

                providerInstance.Initialize(providerName, providerElement.Parameters);

                return providerInstance;
            }
        }

        /// <summary>
        /// Gets the route configuration section.
        /// </summary>
        internal static RoutingSection Config
        {
            get
            {
                return WebConfigurationManager.GetSection("routing") as RoutingSection;
            }
        }

        /// <summary>
        /// Gets a value indicating whether to route existing files.
        /// </summary>
        private static bool RouteExistingFiles
        {
            get
            {
                return RouteManager.Config.RouteExistingFiles;
            }
        }

        /// <summary>
        /// Gets the route declarations.
        /// </summary>
        private static langsamu.Web.Routing.Configuration.RouteCollection Routes
        {
            get
            {
                return RouteManager.Provider.Routes;
            }
        }

        /// <summary>
        /// Registers routes based on declarative route configuration.
        /// </summary>
        public static void RegisterRoutes()
        {
            var routes = RouteTable.Routes;

            routes.RouteExistingFiles = RouteManager.RouteExistingFiles;

            foreach (RouteElement route in RouteManager.Routes)
            {
                switch (route.ElementType)
                {
                    case RouteElementType.PhysicalFile:
                        routes.MapPageRoute(
                            route.Name,
                            route.Url,
                            route.PhysicalFile,
                            route.CheckPhysicalUrlAccess,
                            (RouteValueDictionary)route.Defaults,
                            (RouteValueDictionary)route.Constraints,
                            (RouteValueDictionary)route.DataTokens);

                        break;

                    case RouteElementType.Ignore:
                        routes.Ignore(route.Url, (RouteValueDictionary)route.Constraints);

                        break;

                    case RouteElementType.RouteBase:
                        routes.Add(
                            route.Name,
                            Activator.CreateInstance(route.RouteType, (object[])route.Parameters) as RouteBase);

                        break;

                    case RouteElementType.IRouteHandler:
                        routes.Add(
                            route.Name,
                            new Route(
                                route.Url,
                                (RouteValueDictionary)route.Defaults,
                                (RouteValueDictionary)route.Constraints,
                                (RouteValueDictionary)route.DataTokens,
                                Activator.CreateInstance(route.HandlerType, (object[])route.Parameters) as IRouteHandler));

                        break;

                    case RouteElementType.Mvc:
                        var namespaces = new NamespaceInfo[route.Namespaces.Count];
                        route.Namespaces.CopyTo(namespaces, 0);

                        routes.MapRoute(
                            route.Name,
                            route.Url,
                            (RouteValueDictionary)route.Defaults,
                            (RouteValueDictionary)route.Constraints,
                            namespaces.Select(ns => ns.Namespace).ToArray());

                        break;
                }
            }
        }
    }
}
