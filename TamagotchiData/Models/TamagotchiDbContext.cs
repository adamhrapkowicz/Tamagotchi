using Microsoft.EntityFrameworkCore;

namespace TamagotchiData.Models;

public class TamagotchiDbContext : DbContext

{
    public TamagotchiDbContext(DbContextOptions<TamagotchiDbContext> options) : base(options)
    {
    }

    public DbSet<Dragon>? Dragons { get; set; }
}