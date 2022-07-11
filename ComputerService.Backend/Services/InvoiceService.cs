using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerService.Backend.Dtos;
using ComputerService.Backend.Interfaces;
using ComputerService.Backend.Models.Exceptions;
using Data.Context;
using Data.Enums;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ComputerService.Backend.Services;

public class InvoiceService : IInvoiceService
{
    private readonly ComputerServiceContext _context;

    public InvoiceService(ComputerServiceContext context)
    {
        _context = context;
    }

    public string InvoiceNumber
    {
        get
        {
            var dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var amount = _context.Invoices.Count(i =>
                i.CreatedDateTime >= dtFrom && i.CreatedDateTime.Date <= dtTo);
            amount = amount + 1;

            return $"{DateTime.Now.Month}/{DateTime.Now.Year}/{amount}";
        }
    }

    public async Task<InvoiceCreateDataDto> GetCreateInvoiceData(string rma)
    {
        var invoiceDetails= await _context.Requests.Include(r=>r.RequestInvoiceDetail).Select(r => new InvoiceCreateDataDto
        {
            Rma = r.Rma,
            Name = r.RequestInvoiceDetail.Name,
            Surname = r.RequestInvoiceDetail.Surname,
            NameCompany = r.RequestInvoiceDetail.NameCompany,
            Nip = r.RequestInvoiceDetail.Nip,
            City = r.RequestInvoiceDetail.City,
            Street = r.RequestInvoiceDetail.Street,
            Postcode = r.RequestInvoiceDetail.Postcode,
            Estimate = r.Estimate,
            RequestStatus = r.RequestStatus
        }).AsNoTracking().FirstOrDefaultAsync(r => r.Rma == rma);
       
        var services = await _context.Services.Select(s => new InvoiceItemDto
        {
            Code = s.Code,
            Name = s.Name,
            BasicUnit = s.BasicUnit.ToString(),
            Tax = s.Tax,
            Price = s.Price
        }).AsNoTracking().ToListAsync();
        var products = await _context.Products.Select(p => new InvoiceItemDto
        {
            Code = p.Code,
            Name = $"{p.Model} {p.Brand}",
            BasicUnit = p.BasicUnit.ToString(),
            Tax = p.Tax,
            Price = p.Price,
            InStock = p.InStock
        }).Where(i => i.InStock > 0).AsNoTracking().ToListAsync();
        products.AddRange(services);
        if (!products.Any()) return invoiceDetails;
        if (invoiceDetails != null) invoiceDetails.ListProducts = products;
       
        return invoiceDetails;

    }

    public async Task<Invoice> Create(InvoiceDto model)
    {

        if (await _context.Invoices.AnyAsync(invoice => invoice.Rma == model.Rma))
            throw new ConflictException("Faktura dla zgłoszenia istnieje");

        var invoice = new Invoice
        {
            Rma = model.Rma,
            EmployeeObjectId = model.EmployeeObjectId,
            Number = InvoiceNumber,
            Total = model.Total,
            InvoiceDate = model.InvoiceDate,
            SaleDate = model.SaleDate,
            InvoicePayment = model.InvoicePayment,
            PaymentStatus = model.PaymentStatus,
            PaymentsMethod = model.PaymentsMethod,
            Name = model.Name,
            Surname = model.Surname,
            NameCompany = model.NameCompany,
            Nip = model.Nip,
            City = model.City,
            Street = model.Street,
            Postcode = model.Postcode
        };
        try
        {
            var transaction = await _context.Database.BeginTransactionAsync();


            var config = await _context.Configs.Select(c => new Config
            {
                Id = c.Id,
                IsActive = c.IsActive
            }).SingleOrDefaultAsync(c => c.IsActive == true);
            if (config == null) return null;

            invoice.ConfigId = config.Id;
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            var values = new StringBuilder();
            var row = string.Empty;
            var length = model.Items.Count;
            for (var index = 0; index < model.Items.Count; index++)
            {
                var itemDto = model.Items[index];


                if (itemDto.Code.StartsWith("P"))
                {
                    if (!await ExistProductInStock(itemDto))
                    {
                        throw new ConflictException("Nie ma wystarczacej liczby produków magazynie");
                    }

                    var product = await _context.Products.SingleAsync(p => p.Code == itemDto.Code);
                    product.InStock = product.InStock - itemDto.Amount.Value;
                    _context.Products.Update(product);
                }


                var request = await _context.Requests.FindAsync(invoice.Rma);
                request.RequestStatus = RequestStatus.Zakończone;
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"INSERT INTO InvoiceItems   values  ({invoice.Number},{itemDto.Name},{itemDto.Price},{itemDto.Tax},{itemDto.Amount},{itemDto.BasicUnit})");
                if (length != index - 1)
                    row =
                        $"({invoice.Number},{itemDto.Name},{itemDto.Price},{itemDto.Tax},{itemDto.Amount},{itemDto.BasicUnit}),";

                row =
                    $"{invoice.Number},{itemDto.Name},{itemDto.Price},{itemDto.Tax},{itemDto.Amount},{itemDto.BasicUnit});";
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return invoice;
        }
        catch (ConflictException e)
        {
            throw new ConflictException(e.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> ExistProductInStock(InvoiceItemDto model)
    {
        return await _context.Products.AnyAsync(p => p.Code == model.Code && p.InStock >= model.Amount);
    }



    public async Task<IEnumerable<InvoiceDto>> GetAll()
    {
        return await _context.Invoices.Select(invoice => new InvoiceDto
        {
            Number = invoice.Number,
            Rma = invoice.Rma,
            InvoiceDate = invoice.InvoiceDate,
            PaymentStatus = invoice.PaymentStatus,
            Total = invoice.Total
        }).AsNoTracking().ToListAsync();
    }

    public async Task<List<SummaryTaxDto>> GetSummaryTask(string invoiceNumber)

    {
        var summary = new List<SummaryTaxDto>();

        var NetValueSumarry = await _context.InvoiceItems.Where(i=>i.InvoiceNumber==invoiceNumber)
            .GroupBy(t => t.Tax)
            .Select(netValue => new { Tax = netValue.Key, NetValue = netValue.Sum(c => c.Price * c.Amount) })
            .ToListAsync();

        foreach (var item in NetValueSumarry)
            summary.Add(new SummaryTaxDto
            {
                NetTotal = item.NetValue,
                Tax = item.Tax,
                GrossTotal = (1 + item.Tax) * item.NetValue
            });

        return summary;
    }

    public async Task<InvoiceFullDataDto> Get(string invoiceNumber)
    {
        var query = from invoice in _context.Invoices
            join config in _context.Configs on invoice.ConfigId equals config.Id
            join request in _context.Requests on invoice.Rma equals request.Rma
            where invoice.Number == invoiceNumber



            select new { invoice, config, request };

        var result = await query.SingleOrDefaultAsync();
        var summary = await GetSummaryTask(invoiceNumber);

        var invoiceitems = from invoiceitem in _context.InvoiceItems
                           where invoiceitem.InvoiceNumber == invoiceNumber
                           select new InvoiceItemDto(invoiceitem.Name, invoiceitem.Price, invoiceitem.Tax, invoiceitem.Amount,
                               invoiceitem.BasicUnit);
       var invoiceItems= invoiceitems.ToList();

      
        return new InvoiceFullDataDto(result.invoice, result.config,result.request,summary,invoiceItems);

    }

}