using System.Web;
using System.Web.Routing;
using langsamu.Web.Routing;
using langsamu.Web.Routing.Configuration;

namespace Sample
{
    public class SampleRouteProvider : RouteProvider
    {
        private string sampleProperty;

        public override langsamu.Web.Routing.Configuration.RouteCollection Routes
        {
            get
            {
                var routes = new langsamu.Web.Routing.Configuration.RouteCollection();

                var ignore = new RouteElement();
                ignore.ElementType = RouteElementType.Ignore;
                ignore.Url = "{resource}.axd/{*pathInfo}";
                routes.Add(ignore);

                var home = new RouteElement();
                home.ElementType = RouteElementType.PhysicalFile;
                home.Name = "Home";
                home.PhysicalFile = "~/App_Pages/Home.aspx";
                routes.Add(home);

                var testValues = new RouteElement();
                testValues.ElementType = RouteElementType.PhysicalFile;
                testValues.Name = "TestValues";
                testValues.Url = "test-values/{segment1}/{segment2}";
                testValues.PhysicalFile = "~/App_Pages/TestValues.aspx";
                testValues.Defaults.Add("segment2", "segment-2-default-value");
                testValues.DataTokens.Add("token1", "token-1-value");
                testValues.DataTokens.Add("token2", "token-2-value");
                routes.Add(testValues);

                var testConstraints = new RouteElement()
                {
                    ElementType = RouteElementType.PhysicalFile,
                    Name = "TestConstraints",
                    Url = "test-constraints/{segment1}/{segment2}",
                    PhysicalFile = "~/App_Pages/TestValues.aspx"
                };
                testConstraints.Constraints.Add(
                    new ConstraintElement()
                    {
                        Name = "segment1",
                        Regex = "^foo|bar$"
                    });
                testConstraints.Constraints.Add(
                    new ConstraintElement()
                    {
                        Name = "segment2",
                        ConstraintType = typeof(SampleConstraint1)
                    });
                routes.Add(testConstraints);

                var testHandler1 = new RouteElement()
                {
                    Name = "TestHandler1",
                    ElementType = RouteElementType.IRouteHandler,
                    Url = "test-handler1/{segment1}/{segment2}",
                    HandlerType = typeof(SampleRouteHandler1)
                };
                testHandler1.Defaults.Add("segment2", "segment-2-default-value");
                testHandler1.DataTokens.Add("token1", "token-1-value");
                testHandler1.DataTokens.Add("token2", "token-2-value");
                testHandler1.DataTokens.Add("SampleProperty", this.sampleProperty);
                routes.Add(testHandler1);

                var testHandler2 = new RouteElement()
                {
                    Name = "TestHandler2",
                    ElementType = RouteElementType.IRouteHandler,
                    Url = "test-handler2",
                    HandlerType = typeof(SampleRouteHandler2)
                };
                testHandler2.Parameters.Add("p1", "1");
                testHandler2.Parameters.Add("p2", "2");
                var constraint3 = new ConstraintElement()
                {
                    ConstraintType = typeof(SampleConstraint2)
                };
                constraint3.Parameters.Add("p3", "1");
                testHandler2.Constraints.Add(constraint3);
                routes.Add(testHandler2);


                var testRouteBase = new RouteElement();

                testRouteBase.Name = "TestRouteBase";
                testRouteBase.ElementType = RouteElementType.RouteBase;
                testRouteBase.RouteType = typeof(SampleRoute1);

                testRouteBase.Parameters.Add("param1", "param-1-value");
                testRouteBase.Parameters.Add("param2", "param-2-value");

                routes.Add(testRouteBase);

                return routes;
            }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);

            this.sampleProperty = config["SampleProperty"];
        }
    }
}