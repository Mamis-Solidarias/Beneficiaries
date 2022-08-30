using FastEndpoints;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.PATCH;

internal class Description : Summary<Endpoint>
{
    public Description()
    {
        Summary = "It partially updates a family";
        ExampleRequest = new Request
        {
            FamilyId = "TXT-123",
            Address = "Nueva calle falsa 123",
        };
        
        Response<Response>();
        Response(400,"The parameters are not valid");
        Response(401,"The user is not logged in");
        Response(403, "The user lacks the necessary permissions");
        Response(404,"The family does not exist");
    }
}