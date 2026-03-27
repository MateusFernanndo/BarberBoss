using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Billing;

public interface IBillingWriteOnlyRepository
{
    Task Add(Entities.Billing billing);
    Task<bool> Delete(long id);
}
