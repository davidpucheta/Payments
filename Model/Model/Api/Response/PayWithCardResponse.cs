namespace Model.Model.Api.Response;

public class PayWithCardResponse
{
    public int Id { get; set; }
    
    public string CardNumber { get; set; }

    public decimal Balance { get; set; }
}