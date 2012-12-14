// <copyright file="RoutingModule.cs" company="Samu Lang">
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
    /// <summary>
    /// Represents an HTTP module that registers routes obtained from a route provider.
    /// </summary>
    public sealed class RoutingModule : System.Web.IHttpModule
    {
        /// <summary>
        /// Indicates whether the module has already been initialized.
        /// </summary>
        private static volatile bool moduleInitialised = false;

        /// <summary>
        /// Used for synchronization.
        /// </summary>
        private static object moduleLock = new object();

        /// <summary>
        /// Performs clean-up.
        /// </summary>
        void System.Web.IHttpModule.Dispose()
        {
        }

        /// <summary>
        /// Initializes the module and registers routes.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        void System.Web.IHttpModule.Init(System.Web.HttpApplication context)
        {
            if (!moduleInitialised)
            {
                lock (moduleLock)
                {
                    if (!moduleInitialised)
                    {
                        RouteManager.RegisterRoutes();

                        moduleInitialised = true;
                    }
                }
            }
        }
    }
}