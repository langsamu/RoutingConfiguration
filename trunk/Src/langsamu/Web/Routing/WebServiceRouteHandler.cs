// <copyright file="WebServiceRouteHandler.cs" company="Samu Lang">
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
    using System;
    using System.Web;
    using System.Web.Routing;
    using System.Web.Script;

    /// <summary>
    /// Represents a route that uses a web service to handle a URL pattern.
    /// </summary>
    public sealed class WebServiceRouteHandler : System.Web.Routing.IRouteHandler
    {
        /// <summary>
        /// The name of the URL pattern segment to use as the path info.
        /// </summary>
        private string pathInfoSegment;

        /// <summary>
        /// The path to the web service file.
        /// </summary>
        private string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceRouteHandler"/> class.
        /// </summary>
        /// <param name="filePath">The path to the web service file.</param>
        /// <param name="pathInfoSegment">The name of the URL pattern segment to use as path info.</param>
        public WebServiceRouteHandler(string filePath, string pathInfoSegment)
        {
            this.filePath = filePath;
            this.pathInfoSegment = pathInfoSegment;
        }

        /// <summary>
        /// Provides the object that processes the request.
        /// </summary>
        /// <param name="requestContext">An object that encapsulates information about the request.</param>
        /// <returns>An object that processes the request.</returns>
        /// <remarks>Uses the non-public <see cref="System.Web.Script.Services.ScriptHandlerFactory"/> to get a handler.</remarks>
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            var context = HttpContext.Current;
            var request = context.Request;

            var pathInfo = requestContext.RouteData.Values[this.pathInfoSegment] as string;
            if (!string.IsNullOrEmpty(pathInfo))
            {
                context.RewritePath(this.filePath, string.Concat("/", pathInfo), request.Url.Query.ToString().Replace("?", string.Empty));
            }

            var factory = Activator.CreateInstance(typeof(AjaxFrameworkAssemblyAttribute).Assembly.GetType("System.Web.Script.Services.ScriptHandlerFactory")) as IHttpHandlerFactory;
            return factory.GetHandler(context, request.HttpMethod, this.filePath, request.MapPath(this.filePath));
        }
    }
}