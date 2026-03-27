using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAcess;

internal class BarberBossDBContext : DbContext
{ 
    public BarberBossDBContext(DbContextOptions options) : base(options) { }

    public DbSet<Billing> Billings {  get; set; }
}
