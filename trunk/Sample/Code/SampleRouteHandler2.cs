using System;
using System.Web;
using System.Web.Routing;

namespace Sample
{
    public class SampleRouteHandler2 : IRouteHandler
    {
        private string p1;
        private string p2;

        public SampleRouteHandler2(string p1,string p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            requestContext.HttpContext.Response.Write("Raw response from SampleRouteHandler2");

            requestContext.HttpContext.Response.Write("<br /><br />");

            requestContext.HttpContext.Response.Write("p1 = ");
            requestContext.HttpContext.Response.Write(this.p1);
            requestContext.HttpContext.Response.Write("<br />");
            requestContext.HttpContext.Response.Write("p2 = ");
            requestContext.HttpContext.Response.Write(this.p2);

            requestContext.HttpContext.Response.Write("<br /><br />");

            return new Sample.SampleHttpHandler1();
        }
    }
}