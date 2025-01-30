using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Versioning
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "UriVersiondAPI",
                routeTemplate: "api/v{version}/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
                );

            config.Services.Replace(typeof(IHttpControllerSelector), new Handler.CustomControllerSelector(config));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
