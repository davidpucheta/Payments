using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Model.Api.Request;
using Model.Model.Api.Response;
using Model.Model.Data;
using Payments.Services.Abstractions;
using Repositories;

namespace Payments.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CardController : ControllerBase
{
    private readonly IFeeService _feeService;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCardRequest> _validator;
    private readonly IRepository<Card> _cardRepository;

    public CardController(IMapper mapper, IValidator<CreateCardRequest> validator, IFeeService feeService, IRepository<Card> cardRepository)
    {
        _mapper = mapper;
        _validator = validator;
        _feeService = feeService;
        _cardRepository = cardRepository;
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

        var savedCard = await _cardRepository.AddAsync(card);

        return Ok(_mapper.Map<CreateCardResponse>(savedCard));
    }

    [HttpGet("GetBalance")]
    public async Task<IActionResult> Get(string cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return BadRequest(cardNumber);
        
        var cards = await _cardRepository.GetAllAsync();
        var card = cards.FirstOrDefault(c => c.CardNumber == cardNumber);

        return Ok(card?.Balance ?? 0);
    }

    [HttpPost("Pay")]
    public async Task<IActionResult> PayWithCard(PayWithCardRequest payWithCardRequest)
    {
        if (string.IsNullOrWhiteSpace(payWithCardRequest.CardNumber))
            return BadRequest($"Unknown card number: {payWithCardRequest.CardNumber}");
        
        var cards = await _cardRepository.GetAllAsync();
        var card = cards.FirstOrDefault(c => c.CardNumber == payWithCardRequest.CardNumber);

        if (card == null)
            return BadRequest("Invalid card.");

        var fee = _feeService.Calculate();
        var amountPlusFee = payWithCardRequest.Amount + fee;
        
        if (amountPlusFee > card.Balance)
            return BadRequest($"Insufficient funds.");

        card.Balance -= amountPlusFee;

        var updatedCard = await _cardRepository.UpdateAsync(card);

        return Ok(updatedCard);
    }
}