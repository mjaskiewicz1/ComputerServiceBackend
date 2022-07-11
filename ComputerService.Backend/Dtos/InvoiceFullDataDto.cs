using System;
using System.Collections.Generic;
using Data.Enums;
using Data.Models;

namespace ComputerService.Backend.Dtos;

public class InvoiceFullDataDto : InvoiceDto
{
    public ConfigDto Config;
    public List<SummaryTaxDto> SummaryTax;


    public InvoiceFullDataDto(Invoice invoice, Config config, Request request, List<SummaryTaxDto> summaryTax, List<InvoiceItemDto> items) : base(invoice.EmployeeObjectId, invoice.Rma, invoice.Number, invoice.Total, invoice.InvoiceDate, invoice.SaleDate, invoice.InvoicePayment, invoice.PaymentStatus, invoice.PaymentsMethod, invoice.Name, invoice.Surname, invoice.NameCompany, invoice.Nip, invoice.City, invoice.Street, invoice.Postcode, items)
    {
        ClientEmail = request.Email;
        SummaryTax = summaryTax;
        Config = new ConfigDto(config.Name, config.Nip, config.Email, config.PhoneNumber, config.City, config.Street,
            config.Postcode, config.BankAccountNumber, config.PostalTown, config.BankName);
    }

    public string ClientEmail { get; set; }
}