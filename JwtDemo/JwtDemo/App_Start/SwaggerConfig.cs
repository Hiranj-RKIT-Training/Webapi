using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Web.Http;
using System.Web.Http.Description;

public class SwaggerConfig
{
    public static void Register()
    {
        GlobalConfiguration.Configuration
            .EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Your API Name");
                c.ApiKey("Bearer")
                 .Description("JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"")
                 .Name("Authorization")
                 .In("header");
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.OperationFilter<AssignJwtSecurityRequirements>();

            })
            .EnableSwaggerUi();
    }

    private static string GetXmlCommentsPath()
    {
        return System.String.Format(@"{0}\bin\JwtDemo.XML", System.AppDomain.CurrentDomain.BaseDirectory);
    }
    public class AssignJwtSecurityRequirements : IOperationFilter

    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null) return;

            // Add JWT bearer token parameter
            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                description = "Bearer token for JWT authentication",
                required = false,
                type = "string"
            });
        }
    }
}
