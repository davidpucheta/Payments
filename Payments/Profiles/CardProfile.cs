using AutoMapper;
using Payments.Model.Api.Request;
using Payments.Model.Api.Response;
using Payments.Model.Data;

namespace Payments.Profiles;

public class CardProfile : Profile
{
    public CardProfile()
    {
        CreateMap<CreateCardRequest, Card>();
        CreateMap<Card, CreateCardResponse>();
    }
}