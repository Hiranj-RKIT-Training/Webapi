using Swashbuckle.Application;
using System.Web.Http;

public class SwaggerConfig
{
    public static void Register()
    {
        GlobalConfiguration.Configuration
            .EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Versoning");
                c.IncludeXmlComments(GetXmlCommentsPath());
            })
            .EnableSwaggerUi(d =>
            {
                d.EnableApiKeySupport("Auhtorization", "header");
                //d.EnableOAuth2Support();
            });
    }

    private static string GetXmlCommentsPath()
    {
        return System.String.Format(@"{0}\bin\Versioning.XML", System.AppDomain.CurrentDomain.BaseDirectory);
    }
}
