using System.Collections.Generic;
using Data.Enums;

namespace ComputerService.Backend.Dtos;

public class InvoiceCreateDataDto:RequestInvoiceDetailDto
{
    public IEnumerable<InvoiceItemDto> ListProducts { get; set; }
    public decimal? Estimate { get; set; }
    public RequestStatus RequestStatus { get; set; }

  

}