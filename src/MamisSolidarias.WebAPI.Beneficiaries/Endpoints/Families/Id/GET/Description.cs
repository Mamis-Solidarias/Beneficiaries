using FastEndpoints;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.GET;

internal class Description : Summary<Endpoint>
{
    public Description()
    {
        Summary = "It retrieves a single family";
        ExampleRequest = new Request
        {
            FamilyId = "TXT-123"
        };
        
        Response<Response>();
        Response(400,"The community and family id do not match");
        Response(401,"The user is not authenticated");
        Response(403,"The user does not have the necessary permissions");
        Response(404,"The family does not exists");
    }
}