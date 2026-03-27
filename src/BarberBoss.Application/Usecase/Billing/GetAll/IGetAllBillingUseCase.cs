using BarberBoss.Comunication.Response;

namespace BarberBoss.Application.Usecase.Billing.GetAll;

public interface IGetAllBillingUseCase
{
    Task<ResponseAllBillingsJson> Execute();
}
