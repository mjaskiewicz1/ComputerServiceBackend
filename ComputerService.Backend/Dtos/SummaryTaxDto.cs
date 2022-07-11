namespace ComputerService.Backend.Dtos;

public class SummaryTaxDto
{
    public decimal Tax { get; set; }
    public decimal NetTotal { get; set; }
    public decimal GrossTotal { get; set; }
}