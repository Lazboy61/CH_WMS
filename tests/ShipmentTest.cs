using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

public class ShipmentsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ShipmentsControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAllShipments_ReturnsListOfShipments()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/Shipments");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var shipments = JsonSerializer.Deserialize<IEnumerable<Shipment>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.NotNull(shipments);
        Assert.NotEmpty(shipments);
    }
    [Fact]
    public async Task GetShipmentById_ReturnsShipmentDetails()
    {
        // Arrange
        var client = _factory.CreateClient();

        var shipmentId = 1;

        // Act
        var response = await client.GetAsync($"/api/Shipments/{shipmentId}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var shipment = JsonSerializer.Deserialize<Shipment>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.NotNull(shipment);
        Assert.Equal(shipmentId, shipment.id);
    }
    [Fact]
    public async Task GetOrdersInShipment_ReturnsOrders()
    {
        // Arrange
        var client = _factory.CreateClient();

        var shipmentId = 1;

        // Act
        var response = await client.GetAsync($"/api/Shipments/{shipmentId}/orders");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var orders = JsonSerializer.Deserialize<IEnumerable<Order>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.NotNull(orders);
        Assert.NotEmpty(orders);
    }
    [Fact]
    public async Task GetItemsInShipment_ReturnsItems()
    {
        // Arrange
        var client = _factory.CreateClient();

        var shipmentId = 1;

        // Act
        var response = await client.GetAsync($"/api/Shipments/{shipmentId}/items");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var items = JsonSerializer.Deserialize<IEnumerable<ShipmentItem>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.NotNull(items);
        Assert.NotEmpty(items);
    }

}
