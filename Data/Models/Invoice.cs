using Data.Enums;
using Data.Models.BaseModel;

namespace Data.Models;

public class Invoice : BaseEntityRma
{
    public string EmployeeObjectId { get; set; } = null!;
    public int ConfigId { get; set; }
    public virtual Request Request { get; set; } = null!;
    public string Number { get; set; } = null!;
    public decimal Total { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime SaleDate { get; set; }
    public DateTime InvoicePayment { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentsMethod PaymentsMethod { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? NameCompany { get; set; }
    public string? Nip { get; set; }
    public virtual RequestInvoiceDetail RequestInvoiceDetail { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Postcode { get; set; } = null!;
    public DateTime CreatedDateTime { get; set; }
}