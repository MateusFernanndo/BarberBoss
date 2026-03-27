using BarberBoss.Comunication.Response;

namespace BarberBoss.Application.Usecase.Billing.GetById;

public interface IGetBillingByIdUseCase
{
    public Task<ResponseBillingJson> Execute(long id);
}
