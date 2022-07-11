namespace ComputerService.Backend.Dtos;

public class RequestInvoiceDetailDto
{
    public RequestInvoiceDetailDto()
    {
    }

    public RequestInvoiceDetailDto(string city, string? name, string? nameCompany, string? nip, string postcode,
        string street, string? surname)
    {
        City = city;
        Name = name;
        NameCompany = nameCompany;
        Nip = nip;
        Postcode = postcode;
        Street = street;
        Surname = surname;
    }

    public string? Rma { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? NameCompany { get; set; }
    public string? Nip { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Postcode { get; set; } = null!;
}