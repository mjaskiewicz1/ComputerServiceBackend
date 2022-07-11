using Data.Models.BaseModel;

namespace Data.Models;

/// <summary>
///     dane do wysyłki dla klienta
/// </summary>
public class RequestShipmentDetail : BaseEntityRma
{
    public Request Request { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Postcode { get; set; } = null!;
}