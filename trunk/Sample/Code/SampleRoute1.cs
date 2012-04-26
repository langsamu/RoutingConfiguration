using System;
using System.Web;
using System.Web.Routing;

namespace Sample
{
    public class SampleRoute1 : RouteBase
    {
        private string p1;
        private string p2;

        public SampleRoute1(string p1, string p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            httpContext.Response.Write("<h1>");
            httpContext.Response.Write("Raw response from SampleRoute (catch-all)");
            httpContext.Response.Write("</h1>");
            httpContext.Response.Write("<br />");
            httpContext.Response.Write(string.Format("p1 = {0}, p2 = {1}", this.p1, this.p2));
            httpContext.Response.Write("<br />");

            return new RouteData(this, new Sample.SampleRouteHandler1());
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            throw new NotImplementedException();
        }
    }
}