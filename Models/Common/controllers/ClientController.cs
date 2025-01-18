using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public ActionResult<List<Client>> GetClients()
    {
        var clients = _clientService.GetClients();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public ActionResult<Client> GetClient(int id)
    {
        var client = _clientService.GetClient(id);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }

    [HttpPost]
    public IActionResult AddClient([FromBody] Client client)
    {
        _clientService.AddClient(client);
        return CreatedAtAction(nameof(GetClient), new { id = client.id }, client);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateClient(int id, [FromBody] Client client)
    {
        var existingClient = _clientService.GetClient(id);
        if (existingClient == null)
        {
            return NotFound();
        }

        _clientService.UpdateClient(id, client);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveClient(int id)
    {
        var existingClient = _clientService.GetClient(id);
        if (existingClient == null)
        {
            return NotFound();
        }

        _clientService.RemoveClient(id);
        return NoContent();
    }
}
