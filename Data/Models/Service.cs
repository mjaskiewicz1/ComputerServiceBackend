using Data.Enums;
using Data.Models.BaseModel;

namespace Data.Models;

public class Service 
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Tax { get; set; }
    public string? Description { get; set; }
    public BasicUnitService BasicUnit { get; set; }
    public DateTime CreatedDateTime { get; set; }
}