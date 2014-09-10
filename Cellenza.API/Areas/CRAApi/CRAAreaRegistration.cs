using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace Cellenza.API.Areas.CRAApi
{
    public class CRAAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CRAApi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    name: "cra_test",
            //    url: "cra",
            //    defaults: new { controller = "cra", action = "get", year = 2010, month = 12, user = "me" }
            //    );

            context.MapRoute(
                name: "api_cra",
                url: "cra/{year}/{month}/{user}",
                defaults: new { controller = "cra", action = "get", year = UrlParameter.Optional, month = UrlParameter.Optional, user = UrlParameter.Optional },
                constraints: new
            {
                year = new CompoundRouteConstraint(new IRouteConstraint[] { new IntRouteConstraint(), new MinRouteConstraint(0) }),
                month = new CompoundRouteConstraint(new IRouteConstraint[] { new IntRouteConstraint(), new RangeRouteConstraint(1, 12) })
            }
            );

        }
    }
}