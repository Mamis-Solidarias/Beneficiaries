using FastEndpoints;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.PATCH;

internal class Description : Summary<Endpoint>
{
    public Description()
    {
        Summary = "It updates the parameters of a given beneficiary";
        ExampleRequest = new Request
        {
            Id = 123,
            FirstName = "Carlitos",
            LastName = "Bala"
        };
        Response();
        Response(400, "The Id is not valid");
        Response(401, "the user is not authenticated");
        Response(403, "The user does not have the necessary permissions");
        Response(404, "The beneficiary does not exists");
    }
}