using System;
using System.Web;

namespace Sample
{
    public class SampleHttpHandler1 : IHttpHandler
    {
        bool IHttpHandler.IsReusable
        {
            get
            {
                return false;
            }
        }

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            context.Response.Write("Raw response from SampleHttpHandler");

            context.Response.Write("<br /><br />");

            context.Response.Write("Values:");
            context.Response.Write("<br />");
            foreach (System.Collections.Generic.KeyValuePair<string, object> item in context.Request.RequestContext.RouteData.Values)
            {
                context.Response.Write(item.Key);
                context.Response.Write("=");
                context.Response.Write(item.Value);
                context.Response.Write("<br />");
            }

            context.Response.Write("<br />");

            context.Response.Write("Tokens:");
            context.Response.Write("<br />");
            foreach (System.Collections.Generic.KeyValuePair<string, object> item in context.Request.RequestContext.RouteData.DataTokens)
            {
                context.Response.Write(item.Key);
                context.Response.Write("=");
                context.Response.Write(item.Value);
                context.Response.Write("<br />");
            }
        }
    }
}