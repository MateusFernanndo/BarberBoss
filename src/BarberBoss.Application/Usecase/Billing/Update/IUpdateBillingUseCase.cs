using BarberBoss.Comunication.Request;

namespace BarberBoss.Application.Usecase.Billing.Update;

public interface IUpdateBillingUseCase
{
    public Task Execute(long id, RequestUpdateBillingJson request);
}
