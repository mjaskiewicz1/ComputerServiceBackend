namespace ComputerService.Backend.Dtos;

public class InvoiceNumberDto
{
    public InvoiceNumberDto(string number)
    {
        Number = number;
    }

    public InvoiceNumberDto()
    {
    }

    public string Number { get; set; }
}