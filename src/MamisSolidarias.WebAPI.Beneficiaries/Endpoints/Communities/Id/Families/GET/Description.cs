using FastEndpoints;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.GET;

internal class Description : Summary<Endpoint>
{
    public Description()
    {
        Summary = "DEPRECATED: Use GraphQL.\nThis endpoint retrieves all the families related to a given community";
        ExampleRequest = new Request()
        {
            Id = "MIS",
            Page = 2,
            PageSize = 10,
        };
        
        Response<Response>();
        Response(400,"Invalid parameters");
        Response(401, "The user is not logged in");
        Response(403, "The user does not have the necessary permissions");
    }
}