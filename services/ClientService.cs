using System;
using System.Collections.Generic;
using System.Linq;

public class ClientService
{
    private readonly ModelContext _context;

    public ClientService(ModelContext context)
    {
        _context = context;
    }

    public List<Client> GetClients()
    {
        return _context.Clients.ToList();
    }

    public Client GetClient(int clientId)
    {
        return _context.Clients.FirstOrDefault(client => client.id == clientId);
    }

    public void AddClient(Client client)
    {
        _context.Clients.Add(client);
        _context.SaveChanges();
    }

    public void UpdateClient(int clientId, Client client)
    {
        var existingClient = _context.Clients.FirstOrDefault(c => c.id == clientId);
        if (existingClient != null)
        {
            _context.Entry(existingClient).CurrentValues.SetValues(client);
            _context.SaveChanges();
        }
    }

    public void RemoveClient(int clientId)
    {
        var clientToRemove = _context.Clients.FirstOrDefault(c => c.id == clientId);
        if (clientToRemove != null)
        {
            _context.Clients.Remove(clientToRemove);
            _context.SaveChanges();
        }
    }
}
