using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Billing;

public interface IBillingsUpdateOnlyRepository
{
    Task<Entities.Billing?> GetById(long id);
    void Update(Entities.Billing billing);
}
