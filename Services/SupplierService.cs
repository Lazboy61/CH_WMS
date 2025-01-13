using System.Collections.Generic;
using System.Linq;

public class SupplierService : ISupplierService
{
    private readonly ModelContext _context;

    public SupplierService(ModelContext context)
    {
        _context = context;
    }

    public IEnumerable<Supplier> GetAllSuppliers()
    {
        return _context.Suppliers.ToList();
    }

    public Supplier GetSupplierById(int id)
    {
        return _context.Suppliers.FirstOrDefault(supplier => supplier.id == id);
    }

    public IEnumerable<Item> GetItemsForSupplier(int supplierId)
    {
        // Ophalen van alle items die gekoppeld zijn aan de opgegeven supplier_id
        return _context.Items.Where(item => item.supplier_id == supplierId).ToList();
    }

    public void AddSupplier(Supplier supplier)
    {
        if (_context.Suppliers.Any(s => s.id == supplier.id))
        {
            throw new InvalidOperationException("Duplicate ID detected");
        }
        _context.Suppliers.Add(supplier);
        _context.SaveChanges();
    }

}
