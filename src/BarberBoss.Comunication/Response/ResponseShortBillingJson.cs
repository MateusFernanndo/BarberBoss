namespace BarberBoss.Comunication.Response;

public class ResponseShortBillingJson
{
    public long Id {  get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string BarberName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; }
}
