using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace XSIS_Shop_WEBAPI
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
                name: "CustomMethod1Param",
                routeTemplate: "api/{controller}/{action}/{id}/",
                defaults: new { action = "get", id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "CustomMethod2Param",
                routeTemplate: "api/{controller}/{action}/{id}/{id2}",
                defaults: new { action = "get", id = RouteParameter.Optional, id2 = RouteParameter.Optional }
                );
        }
    }
}
