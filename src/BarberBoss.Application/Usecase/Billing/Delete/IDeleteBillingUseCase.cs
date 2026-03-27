namespace BarberBoss.Application.Usecase.Billing.Delete;

public interface IDeleteBillingUseCase
{
    public Task Execute(long id);
}
