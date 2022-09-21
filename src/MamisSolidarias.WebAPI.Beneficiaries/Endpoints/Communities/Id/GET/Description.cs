using FastEndpoints;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.GET;

internal class Description : Summary<Endpoint>
{
    public Description()
    {
        Summary = "DEPRECATED: Use GraphQL.\nIt retireves a single community by its ID";
        ExampleRequest = new Request()
        {
            Id = "MI"
        };
        Response<Response>();
        Response(401, "The user is not authenticated");
        Response(403, "The user does not have the necessary permissions");
        Response(404,"The community does not exist");
    }
}