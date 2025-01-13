using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILocationService
{
    Task<Location> GetLocationByIdAsync(int id);
    Task<IEnumerable<Location>> GetLocationsByWarehouseIdAsync(int warehouseId);
    Task<bool> UpdateLocationAsync(int id, Location location);
    Task<Location> CreateLocationAsync(Location location);
}
