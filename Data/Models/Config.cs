using Data.Models.BaseModel;

namespace Data.Models;

public class Config : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Nip { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool IsActive { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Postcode { get; set; } = null!;
    public string BankAccountNumber { get; set; } = null!;
    public string PostalTown { get; set; } = null!;
    public string BankName { get; set; } = null!;
}