using BarberBoss.Comunication.Request;
using BarberBoss.Comunication.Response;

namespace BarberBoss.Application.Usecase.Billing.Register;

public interface IRegisterBillingUseCase
{
    Task<ResponseRegisterBillingJson> Execute(RequestBillingJson request);
}
