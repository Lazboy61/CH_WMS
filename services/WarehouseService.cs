// import json

// from models.base import Base

// WAREHOUSES = []


// class Warehouses(Base):
//     def __init__(self, root_path, is_debug=False):
//         self.data_path = root_path + "warehouses.json"
//         self.load(is_debug)

//     def get_warehouses(self):
//         return self.data

//     def get_warehouse(self, warehouse_id):
//         for x in self.data:
//             if x["id"] == warehouse_id:
//                 return x
//         return None

//     def add_warehouse(self, warehouse):
//         warehouse["created_at"] = self.get_timestamp()
//         warehouse["updated_at"] = self.get_timestamp()
//         self.data.append(warehouse)

//     def update_warehouse(self, warehouse_id, warehouse):
//         warehouse["updated_at"] = self.get_timestamp()
//         for i in range(len(self.data)):
//             if self.data[i]["id"] == warehouse_id:
//                 self.data[i] = warehouse
//                 break

//     def remove_warehouse(self, warehouse_id):
//         for x in self.data:
//             if x["id"] == warehouse_id:
//                 self.data.remove(x)

//     def load(self, is_debug):
//         if is_debug:
//             self.data = WAREHOUSES
//         else:
//             f = open(self.data_path, "r")
//             self.data = json.load(f)
//             f.close()

//     def save(self):
//         f = open(self.data_path, "w")
//         json.dump(self.data, f)
//         f.close()
using System;
using System.Collections.Generic;
using System.Linq;

public class WarehouseService
{
    private List<Warehouse> data;

    public WarehouseService(List<Warehouse> warehouses)
    {
        data = warehouses;
    }

    public List<Warehouse> GetWarehouses()
    {
        return data;
    }

    public Warehouse GetWarehouse(int warehouseId)
    {
        return data.FirstOrDefault(x => x.id == warehouseId);
    }

    public void AddWarehouse(Warehouse warehouse)
    {
        warehouse.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        warehouse.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        data.Add(warehouse);
    }

    public void UpdateWarehouse(int warehouseId, Warehouse updatedWarehouse)
    {
        updatedWarehouse.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].id == warehouseId)
            {
                data[i] = updatedWarehouse;
                break;
            }
        }
    }

    public void RemoveWarehouse(int warehouseId)
    {
        var warehouseToRemove = data.FirstOrDefault(x => x.id == warehouseId);
        if (warehouseToRemove != null)
        {
            data.Remove(warehouseToRemove);
        }
    }
}
