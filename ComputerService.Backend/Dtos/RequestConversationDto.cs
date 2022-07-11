using System;

namespace ComputerService.Backend.Dtos;

public class RequestConversationDto
{
    public string Message { get; set; } = null!;
    public string? EmployeeObjectId { get; set; }
    public DateTime CreatedDateTime { get; set; }
}