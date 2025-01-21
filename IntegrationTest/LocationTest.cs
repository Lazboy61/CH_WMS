// using System.Net.Http;
// using System.Net;
// using System.Text;
// using System.Text.Json;
// using System.Threading.Tasks;
// using Xunit;
// using Microsoft.AspNetCore.Mvc.Testing;

// public class LocationControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
// {
//     private readonly WebApplicationFactory<Program> _factory;

//     public LocationControllerIntegrationTests(WebApplicationFactory<Program> factory)
//     {
//         _factory = factory;
//     }

//     [Fact]
//     public async Task CreateLocation_ReturnsCreated()
//     {
//         // Arrange
//         var client = _factory.CreateClient();
//         var location = new
//         {
//             warehouse_id = 1, // Ensure this ID exists in the database
//             code = "A.3.0", // Unique and valid code
//             name = "Row: A, Rack: 3, Shelf: 0", // Ensure this is unique if required
//             created_at = "2025-01-21T12:44:24.687Z",
//             updated_at = "2025-01-21T12:44:24.687Z",
//             inventories = new[]
//             {
//             new
//             {
//                 item_id = "ITEM001",
//                 description = "Test Inventory Item",
//                 item_reference = "REF001",
//                 locations = new[] { "A.3.0" },
//                 total_on_hand = 10,
//                 total_expected = 5,
//                 total_ordered = 3,
//                 total_allocated = 2,
//                 total_available = 8
//             }
//         }
//         };

//         var jsonContent = new StringContent(JsonSerializer.Serialize(location), Encoding.UTF8, "application/json");

//         // Act
//         var response = await client.PostAsync("/api/Locations", jsonContent);
//         var responseContent = await response.Content.ReadAsStringAsync();

//         // Debugging: Log the response for troubleshooting
//         Console.WriteLine($"Response status: {response.StatusCode}");
//         Console.WriteLine($"Response content: {responseContent}");

//         // Assert
//         Assert.Equal(HttpStatusCode.Created, response.StatusCode);
//         Assert.False(string.IsNullOrEmpty(responseContent), "Response body is empty.");
//     }

// }
