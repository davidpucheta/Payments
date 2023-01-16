using FluentValidation;
using Payments.Model.Api.Request;

namespace Payments.Validators;

public class CardValidator : AbstractValidator<CreateCardRequest>
{
    public CardValidator()
    {
        RuleFor(c => c.Balance).GreaterThanOrEqualTo(0);
        RuleFor(c => c.CardNumber)
            .NotEmpty()
            .Length(15)
            .Must(IsValidCardNumber).WithMessage("'{PropertyName}' should be all numbers");
    }
    
    private bool IsValidCardNumber(string cardNumber)
    {
        return cardNumber.All(char.IsNumber);
    }
}