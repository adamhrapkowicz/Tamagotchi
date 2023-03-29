using Microsoft.EntityFrameworkCore;

namespace TamagotchiData.Models;

public class TamagotchiRepository : ITamagotchiRepository
{
    private readonly TamagotchiDbContext _context;
    private readonly DbSet<Dragon>? _dragons;

    public TamagotchiRepository(TamagotchiDbContext context)
    {
        _context = context;
        _dragons = context.Dragons;
    }

    public async Task<Dragon?> GetDragonAsync(Guid dragonId)
    {
        return await _dragons!.FindAsync(dragonId);
    }

    public async Task AddDragonAsync(Dragon dragon)
    {
        await _dragons!.AddAsync(dragon);
    }

    public async Task SaveAllChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void SaveAllChanges()
    {
        _context.SaveChanges();
    }

    public IEnumerable<Dragon>? Dragons => _dragons;
}