namespace TamagotchiData.Models;

public interface ITamagotchiRepository
{
    Task<Dragon?> GetDragonAsync(Guid dragonId);

    Task AddDragonAsync(Dragon dragon);
    
    Task SaveAllChangesAsync();

    void SaveAllChanges();

    IEnumerable<Dragon> Dragons { get; }
}