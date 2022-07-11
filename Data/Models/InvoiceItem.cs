namespace Data.Models;

public class InvoiceItem
{
    public virtual Invoice Invoice { get; set; } = null!;
    public string InvoiceNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Tax { get; set; }
    public int Amount { get; set; }
    public string BasicUnit { get; set; } = null!;
}