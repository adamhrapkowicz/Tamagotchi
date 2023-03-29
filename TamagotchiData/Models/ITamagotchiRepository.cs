namespace TamagotchiData.Models;

public interface ITamagotchiRepository
{
    IEnumerable<Dragon>? Dragons { get; }

    Task<Dragon?> GetDragonAsync(Guid dragonId);

    Task AddDragonAsync(Dragon dragon);

    Task SaveAllChangesAsync();

    void SaveAllChanges();
}