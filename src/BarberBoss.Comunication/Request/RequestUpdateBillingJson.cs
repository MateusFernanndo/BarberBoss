using BarberBoss.Comunication.Enums;

namespace BarberBoss.Comunication.Request;

public class RequestUpdateBillingJson
{
    public string BarberName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public Status Status { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime UptadedAt { get; set; }
}
