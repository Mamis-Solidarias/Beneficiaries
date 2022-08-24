using FastEndpoints;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.POST;

internal class Description : Summary<Endpoint>
{
    public Description()
    {
        Summary = "It creates a series of communities";
        ExampleRequest = new Request
        {
            Communities = new[]
            {
                new CommunityRequest("Misiones", "Cataratas 123", "It is a nice village", "MI"),
                new CommunityRequest("Ezeiza", "Au Richieri 42053", null, null)
            }
        };
        
        Response<Response>(201,"All the communities have been created");
        Response(400,"One or more of the communities have invalid parameters");
        Response(401,"The user is not authenticated");
        Response(403,"The user does not have the necessary permissions");
        Response(409,"There was an error creating one or more communities and the process was cancelled");
    }
}