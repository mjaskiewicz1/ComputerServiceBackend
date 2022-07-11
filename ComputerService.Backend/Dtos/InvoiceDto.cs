using System;
using System.Collections.Generic;
using Data.Enums;

namespace ComputerService.Backend.Dtos;

public class InvoiceDto
{
    public InvoiceDto(string employeeObjectId, string rma, string? number, decimal total, DateTime invoiceDate,
        DateTime saleDate, DateTime invoicePayment, PaymentStatus paymentStatus, PaymentsMethod paymentsMethod,
        string? name, string? surname, string? nameCompany, string? nip, string city, string street, string postcode, List<InvoiceItemDto> items)
    {
        EmployeeObjectId = employeeObjectId;
        Rma = rma;
        Number = number;
        Total = total;
        InvoiceDate = invoiceDate;
        SaleDate = saleDate;
        InvoicePayment = invoicePayment;
        PaymentStatus = paymentStatus;
        PaymentsMethod = paymentsMethod;
        Name = name;
        Surname = surname;
        NameCompany = nameCompany;
        Nip = nip;
        City = city;
        Street = street;
        Postcode = postcode;
        Items = items;
    }

    public InvoiceDto()
    {
    }

    public string EmployeeObjectId { get; set; }
    public string Rma { get; set; }
    public string? Number { get; set; }
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
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Postcode { get; set; } = null!;
    public List<InvoiceItemDto> Items { get; set; }
}