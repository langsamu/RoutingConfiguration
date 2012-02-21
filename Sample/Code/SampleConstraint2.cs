using System.Web;
using System.Web.Routing;

namespace Sample
{
    public class SampleConstraint2 : IRouteConstraint
    {
        private string p1;

        public SampleConstraint2(string p1)
        {
            this.p1 = p1;
        }

        bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            int numericSegment;

            return int.TryParse(this.p1, out numericSegment) ? numericSegment % 2 != 0 : false;
        }
    }
}