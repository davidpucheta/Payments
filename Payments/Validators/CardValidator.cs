using FluentValidation;
using Payments.Data;
using Payments.Model.Api.Request;

namespace Payments.Validators;

public class CardValidator : AbstractValidator<CreateCardRequest>
{
    public CardValidator(ApplicationDbContext dbContext)
    {
        RuleFor(c => c.Balance).GreaterThanOrEqualTo(0);
        RuleFor(c => c.CardNumber)
            .NotEmpty()
            .Length(15)
            .Must(IsValidCardNumber).WithMessage("'{PropertyName}' should be all numbers")
            .Must(n => dbContext.Card.FirstOrDefault(c => c.CardNumber == n) == null)
                .WithMessage("'{PropertyName}' already registered.");
    }
    
    private bool IsValidCardNumber(string cardNumber)
    {
        return cardNumber.All(char.IsNumber);
    }
}