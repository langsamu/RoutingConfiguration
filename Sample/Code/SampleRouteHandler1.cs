using System;
using System.Web;
using System.Web.Routing;

namespace Sample
{
    public class SampleRouteHandler1 : IRouteHandler
    {
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new Sample.SampleHttpHandler1();
        }
    }
}