using FastEndpoints;
using FastEndpoints.Swagger;
using MamisSolidarias.WebAPI.Beneficiaries.Extensions;

namespace MamisSolidarias.WebAPI.Beneficiaries.StartUp;

internal static class MiddlewareRegistrar
{
    public static void Register(WebApplication app)
    {
        app.UseDefaultExceptionHandler();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseFastEndpoints();
        app.MapGraphQL();
        app.RunMigrations();

        if (!app.Environment.IsProduction())
            app.UseSwaggerGen();
    }
}