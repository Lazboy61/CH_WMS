using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;


public class OrderControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public OrderControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetOrderById_ReturnsOrderDetails_AfterCreation()
    {
        // Arrange
        var client = _factory.CreateClient();

        var newOrder = new
        {
            source_id = 0, // Using placeholder value as per Swagger
            order_date = DateTime.UtcNow.ToString("o"), // ISO 8601 format
            request_date = DateTime.UtcNow.AddDays(1).ToString("o"),
            reference = "string",
            reference_extra = "string",
            order_status = "string",
            notes = "string",
            shipping_notes = "string",
            picking_note = "string",
            warehouse_id = 0,
            ship_to = 0,
            bill_to = 0,
            shipment_id = 0,
            total_amount = 0.0M,
            total_discount = 0.0M,
            total_tax = 0.0M,
            total_surcharge = 0.0M,
            items = new[]
            {
            new { id = 0, item_id = "string", amount = 0, orderId = 0 }
        }
        };

        var createOrderContent = new StringContent(
            JsonSerializer.Serialize(newOrder),
            Encoding.UTF8,
            "application/json"
        );

        // Act - Create the order
        var createResponse = await client.PostAsync("/api/Orders", createOrderContent);
        var createResponseContent = await createResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Create Response: {createResponseContent}");

        createResponse.EnsureSuccessStatusCode(); // Expecting 201 Created

        // Extract the created Order's ID
        var createdOrder = JsonSerializer.Deserialize<Order>(createResponseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        Assert.NotNull(createdOrder);
        int createdOrderId = createdOrder.id;

        // Act - Retrieve the Order by ID
        var getResponse = await client.GetAsync($"/api/Orders/{createdOrderId}");
        var getOrderContent = await getResponse.Content.ReadAsStringAsync();

        // Assert - Validate the retrieved Order
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var retrievedOrder = JsonSerializer.Deserialize<Order>(getOrderContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        Assert.NotNull(retrievedOrder);
        Assert.Equal(createdOrderId, retrievedOrder.id);
        Assert.Equal(newOrder.reference, retrievedOrder.reference);

        // Validate items
        Assert.NotNull(retrievedOrder.items);
        Assert.Single(retrievedOrder.items); // Expecting 1 item
        Assert.Equal("string", retrievedOrder.items[0].order_item_id); // Use `order_item_id`

        Assert.Equal(0, retrievedOrder.items[0].amount);
    }

    // [Fact]
    // public async Task UpdateOrder_UpdatesOrderDetails()
    // {
    //     // Arrange
    //     var client = _factory.CreateClient();

    //     var newOrder = new
    //     {
    //         source_id = 16,
    //         order_date = DateTime.UtcNow.ToString("o"),
    //         request_date = DateTime.UtcNow.AddDays(1).ToString("o"),
    //         reference = "ORD_TEST",
    //         reference_extra = "Initial Order Extra Info",
    //         order_status = "Pending",
    //         notes = "Initial notes",
    //         shipping_notes = "Initial shipping notes",
    //         picking_note = "Initial picking note",
    //         warehouse_id = 1,
    //         shipment_id = 99,
    //         total_amount = 1000.50M,
    //         total_discount = 50.25M,
    //         total_tax = 80.00M,
    //         total_surcharge = 20.00M,
    //         items = new[]
    //         {
    //         new { id = 0, order_item_id = "ITEM001", amount = 10, orderId = 0 }
    //     }
    //     };

    //     var createOrderContent = new StringContent(
    //         JsonSerializer.Serialize(newOrder),
    //         Encoding.UTF8,
    //         "application/json"
    //     );

    //     // Act - Create the Order
    //     var createResponse = await client.PostAsync("/api/Orders", createOrderContent);
    //     createResponse.EnsureSuccessStatusCode();
    //     var createResponseContent = await createResponse.Content.ReadAsStringAsync();
    //     var createdOrder = JsonSerializer.Deserialize<Order>(createResponseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    //     Assert.NotNull(createdOrder);

    //     // Prepare updated order payload
    //     var updatedOrder = new
    //     {
    //         id = createdOrder.id,
    //         source_id = 16,
    //         order_date = createdOrder.order_date,
    //         request_date = createdOrder.request_date,
    //         reference = "ORD_UPDATED",
    //         reference_extra = "Updated Order Extra Info",
    //         order_status = "Shipped",
    //         notes = "Updated notes",
    //         shipping_notes = "Updated shipping notes",
    //         picking_note = "Updated picking note",
    //         warehouse_id = createdOrder.warehouse_id,
    //         shipment_id = createdOrder.shipment_id,
    //         total_amount = 2000.00M,
    //         total_discount = 100.00M,
    //         total_tax = 160.00M,
    //         total_surcharge = 40.00M,
    //         items = new[]
    //         {
    //         new { id = createdOrder.items[0].id, order_item_id = "ITEM001", amount = 15, orderId = createdOrder.id }
    //     }
    //     };

    //     var updateOrderContent = new StringContent(
    //         JsonSerializer.Serialize(updatedOrder),
    //         Encoding.UTF8,
    //         "application/json"
    //     );

    //     // Act - Update the Order
    //     var updateResponse = await client.PutAsync($"/api/Orders/{createdOrder.id}", updateOrderContent);
    //     updateResponse.EnsureSuccessStatusCode();
    // }



    // [Fact]
    // public async Task DeleteOrder_RemovesOrder()
    // {
    //     // Arrange
    //     var client = _factory.CreateClient();

    //     var newOrder = new
    //     {
    //         source_id = 16,
    //         order_date = DateTime.UtcNow.ToString("o"),
    //         request_date = DateTime.UtcNow.AddDays(1).ToString("o"),
    //         reference = "ORD_TO_DELETE",
    //         reference_extra = "Order to be deleted",
    //         order_status = "Pending",
    //         notes = "Notes for deletion",
    //         shipping_notes = "Shipping notes for deletion",
    //         picking_note = "Initial picking note",
    //         warehouse_id = 1,
    //         shipment_id = 99,
    //         total_amount = 500.00M,
    //         total_discount = 20.00M,
    //         total_tax = 40.00M,
    //         total_surcharge = 10.00M,
    //         items = new[]
    //         {
    //         new { id = 0, order_item_id = "ITEM002", amount = 5, orderId = 0 }
    //     }
    //     };

    //     var createOrderContent = new StringContent(
    //         JsonSerializer.Serialize(newOrder),
    //         Encoding.UTF8,
    //         "application/json"
    //     );

    //     // Act - Create the Order
    //     var createResponse = await client.PostAsync("/api/Orders", createOrderContent);
    //     createResponse.EnsureSuccessStatusCode();
    //     var createResponseContent = await createResponse.Content.ReadAsStringAsync();
    //     var createdOrder = JsonSerializer.Deserialize<Order>(createResponseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    //     Assert.NotNull(createdOrder);

    //     // Act - Delete the Order
    //     var deleteResponse = await client.DeleteAsync($"/api/Orders/{createdOrder.id}");
    //     deleteResponse.EnsureSuccessStatusCode();
    // }





}
