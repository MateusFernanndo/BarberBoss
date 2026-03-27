using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Billing;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAcess.Repositories;

internal class BillingsRepository : IBillingWriteOnlyRepository, IBillingReadOnlyRepository, IBillingsUpdateOnlyRepository
{
    private readonly BarberBossDBContext _dbContext;
    public BillingsRepository(BarberBossDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Billing billing)
    {
        await _dbContext.Billings.AddAsync(billing);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Billings.FirstOrDefaultAsync(expense => expense.Id == id);
        if(result is null)
        {
            return false;
        }
        _dbContext.Billings.Remove(result);
        return true;
    }

    public async Task<List<Billing>> GetAll()
    {
        return await _dbContext.Billings.AsNoTracking().ToListAsync();
    }

    async Task<Billing?> IBillingsUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Billings.FirstOrDefaultAsync(billing => billing.Id == id);
    }

    async Task<Billing?> IBillingReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Billings.AsNoTracking().FirstOrDefaultAsync(billing => billing.Id == id);
    }
    public void Update(Billing billing)
    {
        _dbContext.Billings.Update(billing);
    }

    public async Task<List<Billing>> FilterByWeek(DateOnly date)
    {
        DayOfWeek firstDaysOfWeek = DayOfWeek.Monday;
        int diff = (7 + (date.DayOfWeek - firstDaysOfWeek)) % 7;
        var startDate = date.AddDays(-1 * diff);
        var endDate = startDate.AddDays(6);
        return await _dbContext
            .Billings
            .AsNoTracking()
            .Where(billing => billing.Date >= startDate && billing.Date <= endDate)
            .OrderBy(billing => billing.Date)
            .ThenBy(billing => billing.ClientName)
            .ToListAsync();
        
    }
}


