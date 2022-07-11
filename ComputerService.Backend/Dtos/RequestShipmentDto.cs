namespace RequestAF.Dtos;

public class RequestShipmentDto
{
    public RequestShipmentDto(string city, string street, string postcode)
    {
        City = city;
        Street = street;
        Postcode = postcode;
    }

    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Postcode { get; set; } = null!;
}