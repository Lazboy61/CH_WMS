using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LocationService : ILocationService
{
    private readonly ModelContext _dbContext;

    public LocationService(ModelContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Haal een specifieke locatie op via ID
    public async Task<Location> GetLocationByIdAsync(int id)
    {
        return await _dbContext.Locations.FirstOrDefaultAsync(l => l.id == id);
    }

    // Haal alle locaties binnen een specifiek warehouse op
    public async Task<IEnumerable<Location>> GetLocationsByWarehouseIdAsync(int warehouseId)
    {
        return await _dbContext.Locations.Where(l => l.warehouse_id == warehouseId).ToListAsync();
    }

    // Update een bestaande locatie
    public async Task<bool> UpdateLocationAsync(int id, Location location)
    {
        var existingLocation = await _dbContext.Locations.FirstOrDefaultAsync(l => l.id == id);
        if (existingLocation == null)
        {
            return false;
        }

        existingLocation.code = location.code;
        existingLocation.name = location.name;
        existingLocation.updated_at = DateTime.UtcNow;

        _dbContext.Locations.Update(existingLocation);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    // Maak een nieuwe locatie
    public async Task<Location> CreateLocationAsync(Location location)
    {
        location.created_at = DateTime.UtcNow;
        location.updated_at = DateTime.UtcNow;

        _dbContext.Locations.Add(location);
        await _dbContext.SaveChangesAsync();
        return location;
    }
}
