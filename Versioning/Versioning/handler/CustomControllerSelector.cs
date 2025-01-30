using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Versioning.Handler
{
    /// <summary>
    /// Custom controller selector for API versioning.
    /// Determines which controller to invoke based on version information in the request.
    /// </summary>
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        /// <summary>
        /// The configuration for the HTTP server.
        /// </summary>
        private readonly HttpConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomControllerSelector"/> class.
        /// </summary>
        /// <param name="config">The HTTP configuration object.</param>
        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        /// <summary>
        /// Selects the appropriate controller based on versioning logic.
        /// </summary>
        /// <param name="request">The incoming HTTP request.</param>
        /// <returns>The appropriate <see cref="HttpControllerDescriptor"/> for the request.</returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // Extract route data and controller mappings
            var routeData = request.GetRouteData();
            var controllers = GetControllerMapping();
            var controllerName = routeData.Values["controller"];

            // Default version if none is specified
            string version = "2";

            // Parse query strings and headers to determine the version
            var queryStrings = HttpUtility.ParseQueryString(request.RequestUri.Query);

            if (routeData.Values.ContainsKey("version"))
            {
                version = routeData.Values["version"].ToString();
            }
            else if (queryStrings["v"] != null)
            {
                version = queryStrings["v"].ToString();
            }
            else if (request.Headers.Contains("X-StudentService-Version"))
            {
                version = request.Headers.GetValues("X-StudentService-Version").FirstOrDefault();
            }

            // Build the versioned controller name
            string controller = $"{controllerName}V{version}";

            // Return the corresponding controller if it exists
            if (controllers.ContainsKey(controller))
            {
                return controllers[controller];
            }

            // Fallback to the base class's implementation if no versioned controller is found
            return base.SelectController(request);
        }
        //public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        //{

        //    var routeData = request.GetRouteData();
        //    var controllers = GetControllerMapping();

        //    if (routeData.Values.ContainsKey("version"))
        //    {
        //        string version = routeData.Values["version"].ToString();
        //        var controllerName = routeData.Values["controller"].ToString();

        //        var versionedController = $"{controllerName}V{version}";

        //        if (controllers.ContainsKey(versionedController))
        //        {
        //            return controllers[versionedController];
        //        }
        //        throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
        //    }
        //    Debug.WriteLine("Route does not contain version.");
        //    return base.SelectController(request);


        //}
        //public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        //{
        //    var routedata = request.GetRouteData();
        //    var controllers = GetControllerMapping();


        //    var controllerName = routedata.Values["controller"];
        //    var queryStrings = HttpUtility.ParseQueryString(request.RequestUri.Query);
        //    if (queryStrings["V"] != null)
        //    {
        //        string version = queryStrings["v"].ToString();
        //        string controller = $"{controllerName}V{version}";
        //        if (controllers.ContainsKey(controller))
        //        {
        //            return controllers[controller];
        //        }
        //        throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

        //    }
        //    return base.SelectController(request);
        //}

        //public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        //{
        //    var routeData = request.GetRouteData();
        //    var controllers = GetControllerMapping();

        //    // Check if the custom header 'X-StudentService-Version' is present
        //    if (request.Headers.Contains("X-StudentService-Version"))
        //    {
        //        // Get the version from the header
        //        var version = request.Headers.GetValues("X-StudentService-Version").FirstOrDefault();
        //        var controllerName = routeData.Values["controller"].ToString();
        //        if (!string.IsNullOrEmpty(version))
        //        {
        //            // Build the versioned controller name
        //            var versionedController = $"{controllerName}V{version}";

        //            // Check if the versioned controller exists in the controller mapping
        //            if (controllers.ContainsKey(versionedController))
        //            {
        //                return controllers[versionedController];
        //            }

        //            // If no versioned controller is found, return a 404
        //            throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
        //        }
        //    }

        //    // If no version header is found or version is invalid, fall back to default
        //    return base.SelectController(request);
        //}
    }
}
