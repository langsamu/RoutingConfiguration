using System.Web;
using System.Web.Routing;

namespace Sample
{
    public class SampleConstraint1 : IRouteConstraint
    {
        bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            int numericSegment;

            return int.TryParse(values[parameterName] as string, out numericSegment) ? numericSegment % 2 == 0 : false;
        }
    }
}