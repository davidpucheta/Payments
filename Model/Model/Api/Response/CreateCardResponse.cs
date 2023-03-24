namespace Model.Model.Api.Response;

public class CreateCardResponse
{
    public int Id { get; set; }
    
    public string CardNumber { get; set; }

    public decimal Balance { get; set; }
}