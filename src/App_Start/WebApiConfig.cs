using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using LightInject;
using Ltht.TechTest.Ioc;

namespace Ltht.TechTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = ContainerFactory.Create();
            container.EnableWebApi(config);
            container.RegisterApiControllers(typeof(WebApiConfig).Assembly);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling =
                                   Newtonsoft.Json.DefaultValueHandling.Include;
        }
    }
}
