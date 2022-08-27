using FastEndpoints;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST;

internal class Description : Summary<Endpoint>
{
    public Description()
    {
        Summary = "It creates a set of new families";
        ExampleRequest = new Request
        {
            Families = new[]
            {
                new FamilyRequest(null, "Garcia", "Calle falsa 123", "EZE", null,
                    new[]
                    {
                        new ContactRequest(ContactType.Instagram, "@paula", "Redes Mama", false)

                    }),
                new FamilyRequest(
                    123, "Gonzales", "Figueroa Alcorta 1232", "FG", "Todo ok",
                    ArraySegment<ContactRequest>.Empty
                )
            }
        };
        
        Response(201,"The families were created");
        Response(400, "Invalid parameters");
        Response(401, "The user is not logged in");
        Response(403, "The user does not have the required permissions");
        Response(409,"One or more errors occurred. Rolling back changes");
    }
}