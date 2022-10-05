using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using MamisSolidarias.Utils.Security;
using MamisSolidarias.WebAPI.Beneficiaries.Extensions;

namespace MamisSolidarias.WebAPI.Beneficiaries.StartUp;

internal static class ServiceRegistrator
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.SetUpOpenTelemetry(builder.Configuration,!builder.Environment.IsProduction());
        
        builder.Services.AddFastEndpoints(t=> t.SourceGeneratorDiscoveredTypes = DiscoveredTypes.All);
        builder.Services.AddAuthenticationJWTBearer(
            builder.Configuration["JWT:Key"],
            builder.Configuration["JWT:Issuer"]
        );
        builder.Services.AddAuthorization(t => t.ConfigurePolicies(Services.Beneficiaries));
        
        builder.Services.SetUpEntityFramework(builder.Configuration,builder.Environment);

        builder.Services.SetUpGraphQl();

        if (!builder.Environment.IsProduction())
            builder.Services.AddSwaggerDoc(t=> t.Title = "Beneficiaries");

        builder.Services.AddCors();
    }
}