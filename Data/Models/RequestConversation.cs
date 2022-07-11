using Data.Models.BaseModel;

namespace Data.Models;

public class RequestConversation : BaseEntityRma
{
    public Request Request { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string? EmployeeObjectId { get; set; }
    public DateTime CreatedDateTime { get; set; }
}