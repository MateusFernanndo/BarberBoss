using AutoMapper;
using BarberBoss.Comunication.Request;
using BarberBoss.Comunication.Response;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestBillingJson, Billing>();
        CreateMap<RequestUpdateBillingJson, Billing>();
    }

    private void EntityToResponse()
    {
        CreateMap<Billing, ResponseRegisterBillingJson>();
        CreateMap<Billing, ResponseShortBillingJson>();
        CreateMap<Billing, ResponseBillingJson>();
    }
}
