using Microsoft.EntityFrameworkCore;

namespace TamagotchiData.Models
{
    public class TamagotchiRepository : ITamagotchiRepository
    {
        private readonly TamagotchiDbContext _context;

        private readonly DbSet<Dragon> _dragons;

        public TamagotchiRepository(TamagotchiDbContext context)
        {
            _context = context;
            _dragons = context.Dragons;
        }

        public async Task<Dragon?> GetDragonAsync(Guid dragonId)
        {
            return await _dragons.FindAsync(dragonId);
        }

        public async Task AddDragonAsync(Dragon dragon)
        {
            await _dragons.AddAsync(dragon);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDragonAsync(Guid dragonId)
        {
            var dragon = await GetDragonAsync(dragonId);
            if (dragon != null)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAllChanges()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Dragon> Dragons => _dragons;
    }
}
