namespace ComputerService.Backend.Dtos;

public class ConfigDto
{
    public ConfigDto(string name, string nip, string email, string phoneNumber, string city, string street,
        string postcode, string bankAccountNumber, string postalTown, string bankName)
    {
        Name = name;
        Nip = nip;
        Email = email;
        PhoneNumber = phoneNumber;
        City = city;
        Street = street;
        Postcode = postcode;
        BankAccountNumber = bankAccountNumber;
        PostalTown = postalTown;
        BankName = bankName;
    }

    public string Name { get; set; } = null!;
    public string Nip { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Postcode { get; set; } = null!;
    public string BankAccountNumber { get; set; } = null!;
    public string PostalTown { get; set; } = null!;
    public string BankName { get; set; } = null!;
}