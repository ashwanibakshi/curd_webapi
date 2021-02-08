using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CURD_webapi
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

            //custom api route
            config.Routes.MapHttpRoute(
                name: "Action",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //removing xml formatter
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
