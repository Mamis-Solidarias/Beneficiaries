using FastEndpoints;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.POST;

internal class Description : Summary<Endpoint>
{
    public Description()
    {
        Summary = "It enables a previously disabled beneficiary";
        ExampleRequest = new Request {Id = 123};
        Response();
        Response(400, "The Id is not valid");
        Response(401, "the user is not authenticated");
        Response(403, "The user does not have the necessary permissions");
        Response(404, "The beneficiary does not exists");
    }
}