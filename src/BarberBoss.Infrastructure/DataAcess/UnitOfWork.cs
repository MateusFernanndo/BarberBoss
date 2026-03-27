using BarberBoss.Domain.Repositories;

namespace BarberBoss.Infrastructure.DataAcess;

internal class UnitOfWork : IUnitOfWork
{

    private readonly BarberBossDBContext _dbContext;
    public UnitOfWork(BarberBossDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
