using Data.Enums;
using Data.Models.BaseModel;

namespace Data.Models;

//ogólne informacje nt. requesta (Request)
public class Request : BaseEntityRma
{
    public string? EmployeeObjectId { get; set; }

    public Guid Url { get; set; }

    //Dane Klienta
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;

    public RequestStatus RequestStatus { get; set; }
    public decimal? Estimate { get; set; }

    //dane o urzadzeniu 
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string? SerialNumber { get; set; }

    public string? Details { get; set; } = null!; //np haslo do laptopa
    public string FailureDescription { get; set; } = null!;
    public DateTime CreatedDateTime { get; set; }
    public RequestShipmentDetail? RequestShipmentDetail { get; set; }
    public RequestInvoiceDetail RequestInvoiceDetail { get; set; } = null!;
    public virtual Invoice? Invoice { get; set; }
}