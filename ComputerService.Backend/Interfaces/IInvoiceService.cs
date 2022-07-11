using System.Collections.Generic;
using System.Threading.Tasks;
using ComputerService.Backend.Dtos;

using Data.Models;

namespace ComputerService.Backend.Interfaces;

public interface IInvoiceService
{
    public Task<InvoiceCreateDataDto> GetCreateInvoiceData(string rma);
    public Task<Invoice> Create(InvoiceDto model);
    public Task<bool> ExistProductInStock(InvoiceItemDto model);
   
    public Task<IEnumerable<InvoiceDto>> GetAll();
    public Task<List<SummaryTaxDto>> GetSummaryTask(string invoiceNumber);
    public Task<InvoiceFullDataDto> Get(string invoiceNumber);
}