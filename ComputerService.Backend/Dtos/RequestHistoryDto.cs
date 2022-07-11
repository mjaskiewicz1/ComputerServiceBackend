using System;

namespace ComputerService.Backend.Dtos;

public class RequestHistoryDto
{
    public DateTime PeriodStart { get; set; }
    public string RequestStatus { get; set; }
    public string EmployeeObjectId { get; set; }
}