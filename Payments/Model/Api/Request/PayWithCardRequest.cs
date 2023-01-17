namespace Payments.Model.Api.Request;

public class PayWithCardRequest
{
    public string CardNumber { get; set; }

    public decimal Amount { get; set; }
}