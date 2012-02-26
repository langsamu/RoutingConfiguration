using System;
using System.Web;
using System.Web.Routing;

namespace Sample
{
    public class SampleRoute1 : RouteBase
    {
        public SampleRoute1(string p1, string p2)
        {
            //TODO: use parameter values
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            httpContext.Response.Write("<h1>");
            httpContext.Response.Write("Raw response from SampleRoute (catch-all)");
            httpContext.Response.Write("</h1>");
            httpContext.Response.Write("<br />");

            return new RouteData(this, new Sample.SampleRouteHandler1());
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            throw new NotImplementedException();
        }
    }
}