using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payments.Data;
using Payments.Model.Api.Request;
using Payments.Model.Api.Response;
using Payments.Model.Data;
using Payments.Services.Abstractions;

namespace Payments.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IFeeService _feeService;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCardRequest> _validator;

    public CardController(ApplicationDbContext applicationDbContext, 
        IMapper mapper, IValidator<CreateCardRequest> validator, IFeeService feeService)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
        _validator = validator;
        _feeService = feeService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Post(CreateCardRequest createCardRequest)
    {
        var result = await _validator.ValidateAsync(createCardRequest);
        if (!result.IsValid) 
        {
            result.AddToModelState(this.ModelState);
            return BadRequest(ModelState);
        }
        
        var card = _mapper.Map<Card>(createCardRequest);
        var savedCard = await _applicationDbContext.Cards.AddAsync(card);
        await _applicationDbContext.SaveChangesAsync();

        return Ok(_mapper.Map<CreateCardResponse>(savedCard.Entity));
    }

    [HttpGet("GetBalance")]
    public async Task<IActionResult> Get(string cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return BadRequest(cardNumber);

        var card = await _applicationDbContext.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        
        return Ok(card?.Balance ?? 0);
    }

    [HttpPost("Pay")]
    public async Task<IActionResult> PayWithCard(PayWithCardRequest payWithCardRequest)
    {
        if (string.IsNullOrWhiteSpace(payWithCardRequest.CardNumber))
            return BadRequest($"Unknown card number: {payWithCardRequest.CardNumber}");

        var card = await _applicationDbContext.Cards
            .FirstOrDefaultAsync(c => c.CardNumber == payWithCardRequest.CardNumber);

        if (card == null)
            return BadRequest("Invalid card.");

        var fee = _feeService.Calculate();
        var amountPlusFee = payWithCardRequest.Amount + fee;
        
        if (amountPlusFee > card.Balance)
            return BadRequest($"Insufficient funds.");

        card.Balance -= amountPlusFee;
        
        var updatedCard = _applicationDbContext.Update(card);
        await _applicationDbContext.SaveChangesAsync();

        return Ok(updatedCard.Entity);
    }
}