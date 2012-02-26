// <copyright file="ElementType.cs" company="Samu Lang">
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
    /// <summary>
    /// Represents the type of a route.
    /// </summary>
    public enum ElementType
    {
        /// <summary>
        /// Requests matching the url pattern will be handled using a file on disk.
        /// </summary>
        PhysicalFile,

        /// <summary>
        /// Requests matching the url pattern will not be handled by routing.
        /// </summary>
        Ignore,

        /// <summary>
        /// Requests will be handled by a class that derives from <see cref="System.Web.Routind.RouteBase"/>.
        /// </summary>
        RouteBase,

        /// <summary>
        /// Requests matching the url pattern will be handled by a class that derives from <see cref="System.Web.Routing.IRouteHandler"/>.
        /// </summary>
        IRouteHandler,

        /// <summary>
        /// Requests matching the url pattern will be handled by an <see cref="System.Web.Mvc.MvcRouteHandler"/>
        /// </summary>
        Mvc
    }
}
