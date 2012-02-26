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

namespace langsamu.Web.Routing.Configuration
{
    using System;
    using System.Web.Configuration;
    using System.Web.Routing;

    /// <summary>
    /// A helper containing utility methods for working with declarative route configuration.
    /// </summary>
    public static class RouteManager
    {
        /// <summary>
        /// Gets the route configuration section.
        /// </summary>
        private static RoutingSection Config
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
        private static RouteCollection Routes
        {
            get
            {
                return RouteManager.Config.Routes;
            }
        }

        /// <summary>
        /// Registeres routes based on declarative route configuration.
        /// </summary>
        public static void RegisterRoutes()
        {
            var routes = RouteTable.Routes;

            routes.RouteExistingFiles = RouteManager.RouteExistingFiles;

            foreach (Web.Routing.Configuration.RouteElement route in RouteManager.Routes)
            {
                switch (route.ElementType)
                {
                    case ElementType.PhysicalFile:
                        routes.MapPageRoute(
                            route.Name,
                            route.Url,
                            route.PhysicalFile,
                            route.CheckPhysicalUrlAccess,
                            (RouteValueDictionary)route.Defaults,
                            (RouteValueDictionary)route.Constraints,
                            (RouteValueDictionary)route.DataTokens);

                        break;

                    case ElementType.Ignore:
                        //// Must use this to mimic System.Web.Routing.RouteCollection.Ignore,
                        //// because that implementation relies on an object for storing constraints,
                        //// while we have a collection.

                        var ignoreRouteType = typeof(System.Web.Routing.RouteCollection).GetNestedType("IgnoreRouteInternal", System.Reflection.BindingFlags.NonPublic);
                        var ignoreRoute = (Route)Activator.CreateInstance(ignoreRouteType, new object[] { route.Url });
                        ignoreRoute.Constraints = (RouteValueDictionary)route.Constraints;

                        routes.Add(ignoreRoute);

                        break;

                    case ElementType.RouteBase:
                        routes.Add(
                            route.Name,
                            Activator.CreateInstance(route.RouteType, (object[])route.Parameters) as RouteBase);

                        break;

                    case ElementType.IRouteHandler:
                        routes.Add(
                            route.Name,
                            new Route(
                                route.Url,
                                (RouteValueDictionary)route.Defaults,
                                (RouteValueDictionary)route.Constraints,
                                (RouteValueDictionary)route.DataTokens,
                                Activator.CreateInstance(route.HandlerType, (object[])route.Parameters) as IRouteHandler));

                        break;

                    case ElementType.Mvc:
                        routes.Add(
                            route.Name,
                            new Route(
                                route.Url,
                                (RouteValueDictionary)route.Defaults,
                                (RouteValueDictionary)route.Constraints,
                                new System.Web.Mvc.MvcRouteHandler()));

                        break;
                }
            }
        }
    }
}
