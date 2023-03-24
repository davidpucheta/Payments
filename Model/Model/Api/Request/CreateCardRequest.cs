namespace Model.Model.Api.Request;

public class CreateCardRequest
{
    public string CardNumber { get; set; }

    public decimal Balance { get; set; }
}