namespace TamagotchiData.Models;

public interface ITamagotchiRepository
{
    Task<Dragon?> GetDragonAsync(Guid dragonId);

    Task AddDragonAsync(Dragon dragon);
    
    Task UpdateDragonAsync(Guid dragonId);

    Task SaveAllChanges();

    IEnumerable<Dragon> Dragons { get; }
}