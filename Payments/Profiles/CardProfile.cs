using AutoMapper;
using Model.Model.Api.Request;
using Model.Model.Api.Response;
using Model.Model.Data;

namespace Payments.Profiles;

public class CardProfile : Profile
{
    public CardProfile()
    {
        CreateMap<CreateCardRequest, Card>();
        CreateMap<Card, CreateCardResponse>();
    }
}