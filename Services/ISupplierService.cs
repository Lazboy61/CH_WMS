using System.Collections.Generic;

public interface ISupplierService
{
    IEnumerable<Supplier> GetAllSuppliers();
    Supplier GetSupplierById(int id);
    IEnumerable<Item> GetItemsForSupplier(int supplierId);
    void AddSupplier(Supplier supplier);
}
