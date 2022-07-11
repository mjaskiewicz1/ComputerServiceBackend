using System;
using System.Collections.Generic;
using Data.Enums;
using RequestAF.Dtos;

namespace ComputerService.Backend.Dtos;

public class RequestDto
{
    public RequestDto()
    {
    }

    public RequestDto(string email, ICollection<RequestConversationDto>? requestConversations, string? rma,
        string? employeeObjectId, RequestStatus requestStatus)
    {
        Email = email;
        RequestConversations = requestConversations;
        Rma = rma;
        EmployeeObjectId = employeeObjectId;
        RequestStatus = requestStatus;
    }

    public string? Rma { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? EmployeeObjectId { get; set; }

    public Guid? Url { get; set; }

    //Dane Klienta
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public RequestStatus RequestStatus { get; set; }
    public decimal? Estimate { get; set; }

    //dane o urzadzeniu 
    public string Brand { get; set; }
    public string Model { get; set; }
    public string? SerialNumber { get; set; }

    public string? Details { get; set; } //np haslo do laptopa
    public string FailureDescription { get; set; }
    public RequestInvoiceDetailDto? RequestInvoiceDetail { get; set; }
    public RequestShipmentDto? RequestShipment { get; set; }
    public ICollection<RequestConversationDto>? RequestConversations { get; set; }
}