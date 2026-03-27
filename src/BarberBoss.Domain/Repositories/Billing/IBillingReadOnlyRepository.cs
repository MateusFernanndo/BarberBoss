using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Billing;

public interface IBillingReadOnlyRepository
{
    Task<List<Entities.Billing>> GetAll();
    Task<Entities.Billing?> GetById(long id);
    Task<List<Entities.Billing>> FilterByWeek(DateOnly date);
}
