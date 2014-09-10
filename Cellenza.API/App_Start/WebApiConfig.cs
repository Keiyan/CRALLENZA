
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace Cellenza.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.Routes.MapHttpRoute(
                name: "api_cra",
                routeTemplate: "cra/{year}/{month}/{user}",
                defaults: new { action = "get", year = UrlParameter.Optional, month = UrlParameter.Optional, user = UrlParameter.Optional },
                constraints: new
                {
                    year = new CompoundRouteConstraint(new IRouteConstraint[] { new IntRouteConstraint(), new MinRouteConstraint(0) }),
                    month = new CompoundRouteConstraint(new IRouteConstraint[] { new IntRouteConstraint(), new RangeRouteConstraint(1, 12) })
                }
            );

        }
    }
}
