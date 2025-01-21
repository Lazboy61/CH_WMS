using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

public class SuppliersControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public SuppliersControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAllSuppliers_ReturnsListOfSuppliers()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/Suppliers");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(responseBody);
    }

    [Fact]
    public async Task GetSupplierById_ReturnsSupplierDetails()
    {
        // Arrange
        var client = _factory.CreateClient();
        var supplierId = 1; // Ensure this ID exists in your test database.

        // Act
        var response = await client.GetAsync($"/api/Suppliers/{supplierId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"id\":1", responseBody); // Adjust as needed for your schema.
    }

    // [Fact]
    // public async Task CreateSupplier_CreatesAndReturnsSupplier()
    // {
    //     // Arrange
    //     var client = _factory.CreateClient();
    //     var newSupplier = new
    //     {
    //         id = 0, // Let the DB auto-generate this if applicable.
    //         code = "SUP001",
    //         name = "Test Supplier",
    //         address = "123 Test Street",
    //         address_extra = "Suite 456",
    //         city = "Test City",
    //         zip_code = "12345",
    //         province = "Test Province",
    //         country = "Test Country",
    //         contact_name = "Test Contact",
    //         phonenumber = "123-456-7890",
    //         reference = "Test Reference"
    //     };
    //     var jsonContent = new StringContent(JsonSerializer.Serialize(newSupplier), Encoding.UTF8, "application/json");

    //     // Act
    //     var response = await client.PostAsync("/api/Suppliers", jsonContent);

    //     // Assert
    //     Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    //     var responseBody = await response.Content.ReadAsStringAsync();
    //     Assert.Contains("\"name\":\"Test Supplier\"", responseBody); // Verify returned data.
    // }

    [Fact]
    public async Task GetItemsForSupplier_ReturnsItems()
    {
        // Arrange
        var client = _factory.CreateClient();
        var supplierId = 1; // Ensure this ID exists and has items.

        // Act
        var response = await client.GetAsync($"/api/Suppliers/{supplierId}/items");

        // Assert
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Assert.True(false, $"Supplier with ID {supplierId} has no items.");
        }
        else
        {
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseBody);
        }
    }
}
