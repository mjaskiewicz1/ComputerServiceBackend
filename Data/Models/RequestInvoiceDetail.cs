using Data.Models.BaseModel;

namespace Data.Models;

public class RequestInvoiceDetail : BaseEntityRma
{
    public virtual Request Request { get; set; } = null!;

    public virtual Invoice? Invoice { get; set; }

    public string? Name { get; set; }
    public string? Surname { get; set; } 
    public string? NameCompany { get; set; }
    public string? Nip { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Postcode { get; set; } = null!;
}