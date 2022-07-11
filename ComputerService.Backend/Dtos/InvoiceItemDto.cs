namespace ComputerService.Backend.Dtos;

public class InvoiceItemDto
{
    public InvoiceItemDto(string code, string name, decimal price, decimal tax, int? amount, int? inStock,
        string basicUnit)
    {
        Code = code;
        Name = name;
        Price = price;
        Tax = tax;
        Amount = amount;
        InStock = inStock;
        BasicUnit = basicUnit;
    }

    public InvoiceItemDto()
    {
    }

    public InvoiceItemDto(string name, decimal price, decimal tax, int? amount, string basicUnit)
    {
        Name = name;
        Price = price;
        Tax = tax;
        Amount = amount;
        BasicUnit = basicUnit;
    }

    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Tax { get; set; }
    public int? Amount { get; set; }
    public int? InStock { get; set; }
    public string BasicUnit { get; set; } = null!;
}