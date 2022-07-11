using System;
using System.Collections.Generic;
using Data.Enums;

namespace ComputerService.Backend.Dtos;

public class RequestFilterDto
{
    public string? Email { get; set; }
    public List<RequestStatus>? RequestStatus { get; set; }
    public DateTime? CreateDateTimeFrom { get; set; }
    public DateTime? CreateDateTimeTo { get; set; }
}