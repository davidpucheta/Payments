using Payments.Services.Abstractions;

namespace Payments.Services.Implementations;

public class FeeService : IFeeService
{
    private decimal _lastFee;
    private DateTime _lastTimeUpdated;

    public FeeService()
    {
        _lastFee = GetRandomDecimal();
        _lastTimeUpdated = new DateTime(
            DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
            DateTime.Now.Hour, 0, 0, 0);
    }
    
    public decimal Calculate()
    {
        var hourDifference = Math.Floor((DateTime.Now - _lastTimeUpdated).TotalHours);
        if (!(hourDifference >= 1)) 
            return _lastFee;
        
        _lastTimeUpdated = _lastTimeUpdated.AddHours(hourDifference);
        _lastFee = GetRandomDecimal() * _lastFee;

        return _lastFee;
    }

    private static decimal GetRandomDecimal()
    {
        var random = new Random();
        var n = random.Next(0, 200);

        return (decimal)n / 100;
    }
}