using FastEndpoints;
using FastEndpoints.Swagger;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.WebAPI.Beneficiaries.CustomJsonConverters;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.StartUp;

internal static class MiddlewareRegistrator
{
    public static void Register(WebApplication app)
    {
        app.UseCors(t =>
        {
            t.AllowAnyHeader();
            t.AllowAnyOrigin();
            t.AllowAnyMethod();
        });
        
        app.UseDefaultExceptionHandler();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseFastEndpoints(t =>
        {
            t.Serializer.Options.Converters.Add(new DateOnlyJsonConverter());
        });
        
        app.MapGraphQL();
        
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<BeneficiariesDbContext>();
            db.Database.Migrate();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(t => t.ConfigureDefaults());
        }
    }
}


