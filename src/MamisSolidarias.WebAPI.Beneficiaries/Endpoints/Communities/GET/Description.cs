using FastEndpoints;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.GET;

internal class Description : Summary<Endpoint>
{
    public Description()
    {
        Summary = "DEPRECATED: Use GraphQL.\nIt retrieves all the created communities";
        Response<Response>();
        Response(401, "The user is not authenticated");
        Response(403, "The user does not have the required permissions");
    }
}