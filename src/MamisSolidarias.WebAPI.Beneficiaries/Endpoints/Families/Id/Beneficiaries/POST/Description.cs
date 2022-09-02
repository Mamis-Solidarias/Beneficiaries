using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.Beneficiaries.POST;

internal class Description : Summary<Endpoint>
{
    public Description()
    {
        Summary = "It adds a list of beneficiaries to a family";
        Response<Response>();
        Response(400, "The parameters are not valid");
        Response(401, "The user is not authenticated");
        Response(403, "The user does not have the necessary permissions");
        Response(404,"The family does not exist");
        Response(409,"There was an error creating the beneficiaries");

        ExampleRequest = new Request
        {
            FamilyId = "TXT",
            Beneficiaries = new[]
            {
                new BeneficiaryRequest(
                    "Lucas",
                    "Dell'Isola",
                    BeneficiaryType.Adult.ToString(),
                    BeneficiaryGender.Male.ToString(),
                    new DateOnly(1996, 2, 25),
                    "37.123.934",
                    "Es el padre",
                    "Las computadoras",
                    new ClothesRequest("45", "L", "M 35"),
                    null,
                    new HealthRequest(true, true, "Fat"),
                    new JobRequest("Electricista")
                ),
                new BeneficiaryRequest(
                    "Carlos",
                    "Dell'Isola",
                    BeneficiaryType.Child.ToString(),
                    BeneficiaryGender.Male.ToString(),
                    new DateOnly(2005, 6, 5),
                    "50.667.335",
                    "Vive con el papa",
                    "Los autos",
                    new ClothesRequest("45", "L", "M 35"),
                    new EducationRequest("primer grado", "Euskal Echea", TransportationMethod.Car.ToString()),
                    new HealthRequest(true, true, "Fat"),
                    null
                )
            }
        };
    }
}